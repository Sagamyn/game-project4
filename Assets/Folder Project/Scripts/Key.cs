using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyType
    {
        Sun,
        Moon
    }

    [SerializeField] private KeyType keyType;

    private bool collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        if (LevelProgressManager.Instance == null)
            return;

        if (keyType == KeyType.Sun)
            LevelProgressManager.Instance.CollectSunKey();
        else
            LevelProgressManager.Instance.CollectMoonKey();

        collected = true;
        Destroy(gameObject);
    }
}
