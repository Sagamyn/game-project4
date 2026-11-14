
using System.Collections;
using UnityEngine;

public class HiddenPassages : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] wallVisuals;
    [SerializeField] private Collider2D wallCollider;
    [SerializeField] private float fadeDuration = 0.3f;

    private Coroutine fadeCoroutine;
    private float[] originalAlphas;

    private void Awake()
    {
        originalAlphas = new float[wallVisuals.Length];

        for (int i = 0; i < wallVisuals.Length; i++)
        {
            originalAlphas[i] = wallVisuals[i].color.a;
        }
    }

    public void Reveal()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        wallCollider.enabled = false;
        fadeCoroutine = StartCoroutine(FadeWall(false));
    }

    public void Hide()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeWall(true));
    }

    private IEnumerator FadeWall(bool hide)
    {
        float elapsed = 0f;
        float[] startAlphas = new float[wallVisuals.Length];

        for (int i = 0; i < wallVisuals.Length; i++)
        {
            startAlphas[i] = wallVisuals[i].color.a;
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            for (int i = 0; i < wallVisuals.Length; i++)
            {
                float targetAlpha = hide ? originalAlphas[i] : 0f;

                Color color = wallVisuals[i].color;
                color.a = Mathf.Lerp(startAlphas[i], targetAlpha, t);
                wallVisuals[i].color = color;
            }

            yield return null;
        }

        for (int i = 0; i < wallVisuals.Length; i++)
        {
            Color color = wallVisuals[i].color;
            color.a = hide ? originalAlphas[i] : 0f;
            wallVisuals[i].color = color;
        }

        if (hide)
            wallCollider.enabled = true;

        fadeCoroutine = null;
    }
}
