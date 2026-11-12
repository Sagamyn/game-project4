
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private float moveSpeed = 6f;
    //[SerializeField] private float jumpForce = 12f;
    [SerializeField] private float launchPower = 5f;
    [SerializeField] private float minDragDistance = 0.5f;
    [SerializeField] private float normalGravity = 1f;

    private Rigidbody2D rb;
    private bool grounded;
    private Vector2 dragStartPosition;
    private Vector2 dragCurrentPosition;
    private bool isAiming;
    private LineRenderer aimLine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        aimLine = GetComponent<LineRenderer>();
        aimLine.enabled = false;
    }

    private void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");

        //rb.velocity = new Vector2(
        //    horizontal * moveSpeed,
        //    rb.velocity.y
        //);

        //if (Input.GetButtonDown("Jump") && grounded)
        //{
        //    rb.velocity = new Vector2(
        //        rb.velocity.x,
        //        jumpForce
        //    );

        //    grounded = false;
        //}

        if (Input.GetMouseButtonDown(0) && grounded)
        {
            Vector2 mouseWorldPosition = GetMouseWorldPosition();

            if (GetComponent<Collider2D>().OverlapPoint(mouseWorldPosition))
            {
                isAiming = true;
                dragStartPosition = rb.position;
                rb.velocity = Vector2.zero;
            }
        }

        if (isAiming && Input.GetMouseButton(0))
        {
            dragCurrentPosition = GetMouseWorldPosition();
            aimLine.enabled = true;
            aimLine.SetPosition(0, dragStartPosition);
            aimLine.SetPosition(1, dragCurrentPosition);
        }

        if (isAiming && Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            aimLine.enabled = false;

            Vector2 dragDirection = dragStartPosition - dragCurrentPosition;

            if (dragDirection.magnitude >= minDragDistance)
            {
                Vector2 launchVelocity = Vector2.ClampMagnitude(
                    dragDirection * launchPower,
                    15f
                );

                rb.gravityScale = normalGravity;
                rb.velocity = launchVelocity;
                grounded = false;
            }
        }
    }
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    public void SetGrounded(bool value)
    {
        grounded = value;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                grounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}
