using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Collider2D))]
public class RedirecBouncePlatform : MonoBehaviour
{
    [SerializeField] private float bounceForce = 15f;
    [SerializeField] private Vector2[] bounceDirections =
    {
        Vector2.zero
    };

    private int currentDirectionIndex = 0;
    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
                Input.mousePosition
            );

            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            if (hit == GetComponent<Collider2D>())
            {
                currentDirectionIndex++;

                if (currentDirectionIndex >= bounceDirections.Length)
                {
                    currentDirectionIndex = 0;
                }

                Debug.Log("Direction: " + bounceDirections[currentDirectionIndex]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        
        if (currentDirectionIndex == 0)
        {
            return;
        }

        if (playerRb != null)
        {
            playerRb.velocity =
                bounceDirections[currentDirectionIndex].normalized * bounceForce;
        }
    }
}
