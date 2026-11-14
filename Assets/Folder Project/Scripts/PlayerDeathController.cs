using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDeathController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup fadeScreen;
    [SerializeField] private MonoBehaviour playerMovement;

    [Header("Death Settings")]
    [SerializeField] private float fadeDuration = 0.4f;
    [SerializeField] private float blackScreenDuration = 0.5f;

    private Rigidbody2D rb;
    private RigidbodyConstraints2D originalConstraints;
    private RoomCheckpoint currentCheckpoint;
    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalConstraints = rb.constraints;

        if (fadeScreen != null)
            fadeScreen.alpha = 0f;
    }

    public void SetCheckpoint(RoomCheckpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void Die()
    {
        if (isDead || currentCheckpoint == null)
            return;

        isDead = true;
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        // Stop player movement
        if (playerMovement != null)
            playerMovement.enabled = false;

        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // Keep the screen black for a moment
        yield return new WaitForSeconds(blackScreenDuration);

        // Reset the current room's puzzle objects
        if (currentCheckpoint.PuzzleManager != null)
            currentCheckpoint.PuzzleManager.ResetRoom();

        // Move player to this room's checkpoint
        rb.position = currentCheckpoint.SpawnPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Restore physics and movement
        rb.constraints = originalConstraints;

        if (playerMovement != null)
            playerMovement.enabled = true;

        // Fade back in
        yield return StartCoroutine(Fade(0f));

        isDead = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeScreen == null)
            yield break;

        float startAlpha = fadeScreen.alpha;
        float elapsed = 0f;

        fadeScreen.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadeScreen.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            yield return null;
        }

        fadeScreen.alpha = targetAlpha;
        fadeScreen.blocksRaycasts = targetAlpha > 0f;
    }
}