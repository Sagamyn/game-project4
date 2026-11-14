
using System.Collections;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Laser References")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private SpriteRenderer laserVisual;
    [SerializeField] private Collider2D laserCollider;

    [Header("Laser Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float waitBeforeMove = 1f;
    [SerializeField] private float disappearDuration = 1f;
    [SerializeField] private float respawnDelay = 2f;

    private void Start()
    {
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        while (true)
        {
            // Spawn at starting position
            transform.position = startPoint.position;
            SetLaserActive(true);

            // Wait before moving
            yield return new WaitForSeconds(waitBeforeMove);

            // Move towards destination
            while (Vector2.Distance(transform.position, endPoint.position) > 0.05f)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    endPoint.position,
                    moveSpeed * Time.deltaTime
                );

                yield return null;
            }

            transform.position = endPoint.position;

            // Disappear
            SetLaserActive(false);

            yield return new WaitForSeconds(disappearDuration);

            // Wait before spawning again
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void SetLaserActive(bool active)
    {
        laserVisual.enabled = active;
        laserCollider.enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerDeathController death =
            other.GetComponent<PlayerDeathController>();

        if (death != null)
            death.Die();
    }
}
