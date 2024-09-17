using UnityEngine;

public class NotRoad : MonoBehaviour
{
    public float massIncrease = 100f; // Kütle artış miktarı
    public float dragIncrease = 10f; // Sürtünme artış miktarı

    private bool actionDone = false; // Ağırlık ekleme ve sürtünme değişikliğinin yapılıp yapılmadığını takip eden bayrak

    void OnCollisionEnter(Collision collision)
    {
        // Karakter objeye değdiğinde işaretle
        if (collision.collider.CompareTag("Player") && !actionDone) // Ağırlık ekleme ve sürtünme değişikliği henüz yapılmadıysa
        {
            Rigidbody playerRigidbody = collision.collider.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Karakterin Rigidbody'sinin kütlesini ve sürtünmesini artır
                IncreaseMassAndDrag(playerRigidbody);
                actionDone = true; // Ağırlık ekleme ve sürtünme değişikliğinin yapıldığını işaretle
            }
        }
    }

    void IncreaseMassAndDrag(Rigidbody rb)
    {
        if (rb != null)
        {
            rb.mass += massIncrease; // Kütleyi artır
            rb.drag += dragIncrease; // Sürtünme kuvvetini artır
        }
    }
}
