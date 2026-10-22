
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class TouchToSolid : MonoBehaviour
{
    private Collider2D objectCollider;
    private SpriteRenderer spriteRenderer;

    private bool activated;
    private Color originalColor;

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;

        ResetObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.CompareTag("Player"))
            return;

        activated = true;
        objectCollider.isTrigger = false;

        spriteRenderer.color =
            new Color(1f, 0.75f, 0.2f, 1f);
    }

    public void ResetObject()
    {
        activated = false;

        objectCollider.enabled = true;
        objectCollider.isTrigger = true;

        Color ghostColor = originalColor;
        ghostColor.a = 0.4f;
        spriteRenderer.color = ghostColor;
    }
}
