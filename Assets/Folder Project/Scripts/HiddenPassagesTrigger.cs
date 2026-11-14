
using UnityEngine;

public class HiddenPassageTrigger : MonoBehaviour
{
    [SerializeField] private HiddenPassages passage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        passage.Reveal();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        passage.Hide();
    }
}
