using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    [SerializeField] private float bounceForce = 15f;
    [SerializeField] private Vector2 bounceDirection = Vector2.up;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            playerRb.velocity = bounceDirection.normalized * bounceForce;
        }
    }
}
