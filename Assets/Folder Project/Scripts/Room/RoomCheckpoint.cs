using UnityEngine;

public class RoomCheckpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private RoomPuzzleManager puzzleManager;

    public Vector3 SpawnPosition => spawnPoint.position;
    public RoomPuzzleManager PuzzleManager => puzzleManager;

    private void Awake()
    {
        puzzleManager = GetComponent<RoomPuzzleManager>();
    }
}