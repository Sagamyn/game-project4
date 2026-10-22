
using UnityEngine;

public class RoomPuzzleManager : MonoBehaviour
{
    public void ResetRoom()
    {
        // Find all falling obstacles in this room.
        FallingObject[] fallingObjects =
            GetComponentsInChildren<FallingObject>(true);

        foreach (FallingObject obj in fallingObjects)
        {
            obj.ResetObject();
        }

        // Find all touch-to-solid platforms in this room.
        TouchToSolid[] solidObjects =
            GetComponentsInChildren<TouchToSolid>(true);

        foreach (TouchToSolid obj in solidObjects)
        {
            obj.ResetObject();
        }

        Debug.Log("Room reset: " + gameObject.name);
    }
}
