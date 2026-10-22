
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomCameraZone : MonoBehaviour
{
    [SerializeField]
    private RoomCameraController cameraController;

    [SerializeField]
    private float resetDelay = 0.1f;

    private RoomPuzzleManager puzzleManager;
    private PlayerRoomTransition playerTransition;
    private Coroutine resetCoroutine;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        puzzleManager = GetComponent<RoomPuzzleManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        // Freeze the player during the room transition.
        if (playerTransition == null)
        {
            playerTransition =
                other.GetComponent<PlayerRoomTransition>();
        }

        if (playerTransition != null)
        {
            playerTransition.FreezePlayer();
        }

        // Slide the camera to this room.
        if (cameraController != null)
        {
            cameraController.MoveToRoom(transform.position);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }

        resetCoroutine = StartCoroutine(ResetAfterTransition());
    }

    private IEnumerator ResetAfterTransition()
    {
        float delay = resetDelay;

        if (cameraController != null)
        {
            delay += cameraController.TransitionTime;
        }

        yield return new WaitForSeconds(delay);

        if (puzzleManager != null)
        {
            puzzleManager.ResetRoom();
        }

        resetCoroutine = null;
    }
}
