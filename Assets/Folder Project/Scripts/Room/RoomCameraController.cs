
using System.Collections;
using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    [SerializeField] private float transitionTime = 0.6f;

    public float TransitionTime => transitionTime;

    private Coroutine transitionCoroutine;

    public void MoveToRoom(Vector3 roomPosition)
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        roomPosition.z = transform.position.z;

        transitionCoroutine = StartCoroutine(
            SlideToRoom(roomPosition)
        );
    }

    private IEnumerator SlideToRoom(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / transitionTime);
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(
                startPosition,
                destination,
                t
            );

            yield return null;
        }

        transform.position = destination;
        transitionCoroutine = null;
    }
}
