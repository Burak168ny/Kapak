using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targetList; // Takip edilecek nesnelerin dizisi
    public float smoothSpeed = 0.125f; // Takip etme hızı
    public Vector3 offset; // Kameranın nesneye göre konumu

    private Transform target; // Şu anda takip edilen nesne

    private void Start()
    {
        // Başlangıçta PlayerPrefs'den seçilen karakter indexini al ve target olarak ayarla
        int selectedIndex = PlayerPrefs.GetInt("CharacterSelected", 0);
        if (targetList != null && selectedIndex >= 0 && selectedIndex < targetList.Length)
        {
            target = targetList[selectedIndex];
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Kameranın mevcut Z pozisyonunu korumak için mevcut pozisyonu al
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        // Kameranın yumuşak hareketini sağlamak için Lerp kullan
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayı yeni pozisyona taşı
        transform.position = smoothedPosition;
    }

    public void SetTarget(int index)
    {
        if (targetList != null && index >= 0 && index < targetList.Length)
        {
            target = targetList[index];
        }
    }
}
