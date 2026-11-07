using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.gravityScale = 0f;

            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.SetGrounded(true);
            }
        }
    }
}
