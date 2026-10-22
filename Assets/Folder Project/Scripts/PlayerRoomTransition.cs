
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRoomTransition : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 0.7f;

    private Rigidbody2D rb;
    private bool isFrozen;
    private Coroutine freezeCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FreezePlayer()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine());
    }

    private IEnumerator FreezeRoutine()
    {
        isFrozen = true;

        // Stop the player and temporarily freeze physics.
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(freezeDuration);

        // Restore normal movement and physics.
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        isFrozen = false;
        freezeCoroutine = null;
    }

    public bool IsFrozen()
    {
        return isFrozen;
    }
}
