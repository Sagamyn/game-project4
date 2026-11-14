using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerDeathController death =
            other.GetComponent<PlayerDeathController>();

         if (death != null)
            death.Die();
    }
}
