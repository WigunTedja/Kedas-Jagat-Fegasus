using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public MobileJoystick mobileJoystick;

    //private bool isFacingRight = true;
    public float MoveSpeed = 5f;
    private Vector2 Movement;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = 0f;
        float MoveY = 0f;

        if (mobileJoystick != null && mobileJoystick.gameObject.activeInHierarchy)
        {
            MoveX = mobileJoystick.InputVector.x;
            MoveY = mobileJoystick.InputVector.y;
        }

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

        bool isMoving = (MoveX != 0f || MoveY != 0f);
        _animator.SetBool("isRunning", isMoving);
        //if(Keyboard.current.eKey.isPressed)
        //{
        //    _animator.SetTrigger("isInteracting");
        //    //_animator.ResetTrigger("isInteracting");
        //}

        if (MoveX > 0f)
        {
            sr.flipX = false;
        }
        else if (MoveX < 0f)
        {
            sr.flipX = true;
        }
        //rb.linearVelocity = Movement;
        //rb.MovePosition(Movement);// = Movement;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = Movement * MoveSpeed;

    }

    private void Flip()
    {

        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        //isFacingRight = false;
    }
}
