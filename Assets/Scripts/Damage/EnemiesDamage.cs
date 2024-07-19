using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}