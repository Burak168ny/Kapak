using UnityEngine;
using UnityEngine.UI;

public class ApplyForce : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody bileşeni
    public float forceAmount = 100f; // Uygulanacak kuvvetin miktarı
    public float forceDirection = 100f;
    public float forceSDirection = 20f;
    public float maxSpeed = 30f; // Maksimum hız limitini belirle
    public Button upButton; // Yukarı butonu
    public Button downButton; // Aşağı butonu

    private Vector3 direction; // Global direction değişkeni
    private DeadAndScore deadAndScore; // DeadAndScore nesnesi

    void Start()
    {
        // Rigidbody bileşenini al
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // DeadAndScore bileşenini bul
        deadAndScore = FindObjectOfType<DeadAndScore>();

        // Butonlara tıklama olaylarını bağla
        upButton.onClick.AddListener(ApplyUpwardForce);
        downButton.onClick.AddListener(ApplyDownwardForce);

        // İlk direction değerini ayarla
        Quaternion rotation = Quaternion.Euler(0, -90, 30); // X ekseninde açı
        direction = rotation * Vector3.forward; // İleri yönlü vektörü bu rotasyonla döndür
    }

    void FixedUpdate()
    {
        // Skora göre hız limitini ve forceSDirection değerini güncelle
        UpdateForceAndSpeedBasedOnScore();

        // Update metodunda direction değişkenini kullanarak kuvvet uygula
        rb.AddForce(direction * forceSDirection, ForceMode.Impulse);

        // Hızı sınırlama
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void ApplyUpwardForce()
    {
        // Karakterin yerel koordinat sistemine göre yukarı doğru kuvvet uygula
        rb.AddForce(transform.up * forceAmount, ForceMode.Impulse);
        rb.AddForce(direction * forceDirection, ForceMode.Impulse);
    }

    void ApplyDownwardForce()
    {
        // Karakterin yerel koordinat sistemine göre aşağı doğru kuvvet uygula
        rb.AddForce(-transform.up * forceAmount, ForceMode.Impulse);
        rb.AddForce(direction * forceDirection, ForceMode.Impulse);
    }

    void UpdateForceAndSpeedBasedOnScore()
    {
        if (deadAndScore != null)
        {
            float score = deadAndScore.GetScore();

            if (score >= 10000)
            {
                maxSpeed = 35f;
                forceSDirection = 35f;
            }
            else if (score >= 7500)
            {
                maxSpeed = 31f;
                forceSDirection = 31f;
            }
            else if (score >= 5000)
            {
                maxSpeed = 27f;
                forceSDirection = 28f;
            }
            else if (score >= 2500)
            {
                maxSpeed = 23f;
                forceSDirection = 25f;
            }
            else
            {
                maxSpeed = 20f;
                forceSDirection = 23f;
            }
        }
    }
}
