
using UnityEngine;

public class KeyholeInteraction : MonoBehaviour
{
    public enum KeyType
    {
        Sun,
        Moon
    }

    [Header("Keyhole Settings")]
    [SerializeField] private KeyType keyType;
    [SerializeField] private LastGateManager gateManager;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject insertedKeyVisual;
    [SerializeField] private GameObject interactionPrompt;

    private bool playerNearby;
    private bool keyInserted;

    private void Start()
    {
        if (insertedKeyVisual != null)
            insertedKeyVisual.SetActive(false);

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        if (!playerNearby || keyInserted)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            InsertKey();
    }

    private void InsertKey()
    {
        if (gateManager == null)
        {
            Debug.LogWarning("Gate Manager is not assigned!");
            return;
        }

        bool success = false;

        if (keyType == KeyType.Sun)
            success = gateManager.InsertSunKey();
        else
            success = gateManager.InsertMoonKey();

        if (!success)
        {
            Debug.Log("You don't have the correct key!");
            return;
        }

        keyInserted = true;

        if (insertedKeyVisual != null)
            insertedKeyVisual.SetActive(true);

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNearby = true;

        if (!keyInserted && interactionPrompt != null)
            interactionPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNearby = false;

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }
}
