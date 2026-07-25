using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Vector2 Movement;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.interpolation();
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = 0f;
        float MoveY = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) MoveX += 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) MoveX -= 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) MoveY += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) MoveY -= 1f;
        }

        Movement.x = MoveX * MoveSpeed * Time.deltaTime;
        Movement.y = MoveY * MoveSpeed * Time.deltaTime;

        Movement = new Vector2(Movement.x, Movement.y).normalized;
        //rb.linearVelocity = Movement;
        //rb.MovePosition(Movement);// = Movement;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = Movement * MoveSpeed;
    }
}
