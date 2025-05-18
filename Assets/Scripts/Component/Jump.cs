using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpAcceleration = 100f;
    public float jumpUpTime = 0.15f;

    float elapsedTime = 0f;
    bool isJumping = false;
    PlayerInput playerInput;
    WallCheck wallCheck;
    Rigidbody2D rb2d;

    private void Awake()
    {
        playerInput = InputManager.Instance.input;
        playerInput.Ingame.Z.started += OnJumpStart;
        playerInput.Ingame.Z.canceled += JumpCancled;
        wallCheck = GetComponent<WallCheck>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        JumpAdjustment();
    }

    void OnJumpStart(InputAction.CallbackContext ctx)
    {
        elapsedTime = 0;
        wallCheck.Check();
        if (wallCheck.Down)
        {
            isJumping = true;
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void JumpCancled(InputAction.CallbackContext ctx) 
    {
        isJumping = false;
    }

    void JumpAdjustment() 
    {
        if (elapsedTime <= jumpUpTime && isJumping)
        {
            elapsedTime += Time.fixedDeltaTime;
            rb2d.AddForce(Vector2.up * jumpAcceleration);
        }
    }
}
