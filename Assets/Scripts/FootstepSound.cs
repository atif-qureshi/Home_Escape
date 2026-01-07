using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;           // Audio source to play footsteps
    public AudioClip[] footstepClips;         // Footstep sounds
    public float movementThreshold = 0.1f;    // Minimal movement to trigger steps
    public float groundCheckDistance = 1.2f;  // Raycast distance to check ground

    private bool isMoving;

    void Update()
    {
        Vector2 input = GetMovementInput();
        bool isGrounded = IsGrounded();
        isMoving = input.magnitude > movementThreshold && isGrounded;

        HandleFootsteps();
    }

    Vector2 GetMovementInput()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            Vector2 input = Vector2.zero;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y -= 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1;
            return input.normalized;
        }
        return Vector2.zero;
#else
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#endif
    }

    void HandleFootsteps()
    {
        if (isMoving && footstepClips.Length > 0 && audioSource != null)
        {
            // If not already playing, start looping footsteps
            if (!audioSource.isPlaying)
            {
                audioSource.clip = footstepClips[Random.Range(0, footstepClips.Length)];
                audioSource.loop = true;
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.Play();
            }
        }
        else
        {
            // Stop footsteps immediately when player stops
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
