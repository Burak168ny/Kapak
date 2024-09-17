using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cleaner : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Objeyi X ekseninde 10 birim sağa taşıyoruz.
            transform.position += new Vector3(50f, 0f, 0f);
            CleanerText.cleanerAmount += 1;
        }
    }
}
