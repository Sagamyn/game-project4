
using System.Collections;
using UnityEngine;

public class LastGateManager : MonoBehaviour
{
    [Header("Gate State")]
    [SerializeField] private bool sunKeyInserted;
    [SerializeField] private bool moonKeyInserted;
    [SerializeField] private bool gateOpened;

    [Header("Ancient Wall")]
    [SerializeField] private Transform ancientWall;
    [SerializeField] private Collider2D wallCollider;
    [SerializeField] private float openDistance = 5f;
    [SerializeField] private float openSpeed = 2f;

    private Vector3 closedPosition;

    private void Awake()
    {
        if (ancientWall != null)
            closedPosition = ancientWall.position;
    }

    public bool InsertSunKey()
    {
        if (sunKeyInserted || gateOpened)
            return false;

        if (LevelProgressManager.Instance == null ||
            !LevelProgressManager.Instance.UseSunKey())
            return false;

        sunKeyInserted = true;
        CheckGate();
        Debug.Log("Sun Key inserted!");
        return true;
    }

    public bool InsertMoonKey()
    {
        if (moonKeyInserted || gateOpened)
            return false;

        if (LevelProgressManager.Instance == null ||
            !LevelProgressManager.Instance.UseMoonKey())
            return false;

        moonKeyInserted = true;
        CheckGate();
        Debug.Log("Moon Key inserted!");
        return true;
    }

    private void CheckGate()
    {
        if (sunKeyInserted && moonKeyInserted)
            OpenGate();
    }

    private void OpenGate()
    {
        if (gateOpened || ancientWall == null)
            return;

        gateOpened = true;
        StartCoroutine(RaiseWall());
    }

    private IEnumerator RaiseWall()
    {
        Vector3 targetPosition =
            closedPosition + Vector3.up * openDistance;

        if (wallCollider != null)
            wallCollider.enabled = false;

        while (Vector3.Distance(ancientWall.position, targetPosition) > 0.01f)
        {
            ancientWall.position = Vector3.MoveTowards(
                ancientWall.position,
                targetPosition,
                openSpeed * Time.deltaTime
            );

            yield return null;
        }

        ancientWall.position = targetPosition;

        Debug.Log("ANCIENT GATE OPENED!");
    }
}
