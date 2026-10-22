
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallingObject : MonoBehaviour
{
    private Collider2D objectCollider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool activated;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Color originalColor;

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Remember the original state.
        startPosition = transform.position;
        startRotation = transform.rotation;

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        ResetObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.CompareTag("Player"))
            return;

        activated = true;

        // Become solid and fall.
        objectCollider.isTrigger = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;

        if (spriteRenderer != null)
        {
            spriteRenderer.color =
                new Color(1f, 0.75f, 0.2f, 1f);
        }
    }

    public void ResetObject()
    {
        activated = false;

        // Stop all movement before restoring the position.
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 1f;

        transform.SetPositionAndRotation(
            startPosition,
            startRotation
        );

        objectCollider.enabled = true;
        objectCollider.isTrigger = true;

        if (spriteRenderer != null)
        {
            Color ghostColor = originalColor;
            ghostColor.a = 0.4f;
            spriteRenderer.color = ghostColor;
        }
    }
}
