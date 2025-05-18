using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb2d;
    WallCheck wallCheck;
    PlayerInput playerInput;
    bool isCharacterLeft = false;

    public float MoveSpeed = 15f;
    public float MaxMoveSpeed = 15f;
    public float moveMultiplier = 1f;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        wallCheck = GetComponent<WallCheck>();
        playerInput = InputManager.Instance.input;
    }

    private void FixedUpdate()
    {
        wallCheck.Check();
        OnMove();
    }

    void OnMove() 
    {
        Vector2 inputValue = playerInput.Ingame.Direction.ReadValue<Vector2>();
        float horizontalInput = inputValue.x;

        if (horizontalInput != 0)
        {
            if (Mathf.Abs(rb2d.linearVelocityX) <= MaxMoveSpeed*moveMultiplier || (Mathf.Sign(rb2d.linearVelocityX) != Mathf.Sign(horizontalInput)))
            {
                rb2d.linearVelocityX = horizontalInput * MoveSpeed * moveMultiplier;
                isCharacterLeft = horizontalInput < 0;
            }

            if (wallCheck.Left)
            {
                rb2d.linearVelocityX = Mathf.Clamp(rb2d.linearVelocityX, 0, float.MaxValue);
            }
            if (wallCheck.Right)
            {
                rb2d.linearVelocityX = Mathf.Clamp(rb2d.linearVelocityX, float.MinValue, 0);
            }
        }
        else
        {
            rb2d.linearVelocityX *= .9f;
            if(Mathf.Abs(rb2d.linearVelocityX) < 0.01f)
            {
                rb2d.linearVelocityX = 0;
            }
        }
    }
}
