using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{
    public float speed = 13f; // Objeyi hareket ettirmek için başlangıç hızı
    public float maxSpeed = 40f; // Mesafe 50 birimi aştığında hızın maksimum değeri
    public float minSpeed = 13f; // Mesafe 30 birimi olduğunda hızın eski haline dönmesi için minimum değer
    public float maxDistance = 50f; // Hızın artırılacağı mesafe
    public float minDistance = 30f; // Hızın eski haline döneceği mesafe
    private bool isStopped = false;
    private float initialSpeed;
    private bool actionDone = false; // Ağırlık ekleme ve rotasyon değişikliğinin yapılıp yapılmadığını takip eden bayrak
    private DeadAndScore deadAndScore; // Skoru almak için referans
    private float lastScoreThreshold = 0f; // Son hız artışının yapıldığı puan eşiği

    void Start()
    {
        initialSpeed = speed; // Başlangıç hızını kaydet
        deadAndScore = FindObjectOfType<DeadAndScore>(); // DeadAndScore referansını bul
        StartCoroutine(ChangeScaleCoroutine()); // Coroutine başlat
    }

    void Update()
    {
        if (!isStopped)
        {
            // Objeyi -X ekseni yönünde hareket ettir
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            
            if (deadAndScore != null)
            {
                float score = deadAndScore.GetScore();
                UpdateSpeedBasedOnScore(score);
            }
        }
    }

    // Hızı elle ayarlamak için public bir yöntem
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        // Karakter objenin içine girdiğinde işaretle
        if (other.CompareTag("Player") && !actionDone) // Ağırlık ekleme ve rotasyon değişikliği henüz yapılmadıysa
        {
            ApplyForce applyForce = other.GetComponent<ApplyForce>();
            if (applyForce != null)
            {
                // Karakterin Rigidbody'sinin kütlesini artır
                IncreaseMass(applyForce.rb);
                
                // Karakterin rotasını değiştir
                ChangeCharacterRotation(other.transform);
                
                actionDone = true; // Ağırlık ekleme ve rotasyon değişikliğinin yapıldığını işaretle
            }
        }
    }

    bool IsCharacterInsideObject(Collider character)
    {
        // Karakterin objenin içinde olup olmadığını kontrol et
        Collider objectCollider = GetComponent<Collider>();
        if (objectCollider != null)
        {
            return objectCollider.bounds.Contains(character.transform.position);
        }
        return false;
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Transform playerTransform = player.transform;
                float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);

                if (distance > maxDistance)
                {
                    speed = maxSpeed;
                }
                else if (distance <= minDistance)
                {
                    speed = minSpeed;
                }
                else
                {
                    float t = (distance - minDistance) / (maxDistance - minDistance);
                    speed = Mathf.Lerp(minSpeed, maxSpeed, t);
                }
            }
        }
    }

    void StopObject()
    {
        isStopped = true;
    }

    void IncreaseMass(Rigidbody rb)
    {
        if (rb != null)
        {
            rb.mass += 60; // Kütleyi artırarak objenin hareket etmesini engelle
            rb.drag = 7; // Sürtünme kuvvetini artır
        }
    }

    void ChangeCharacterRotation(Transform characterTransform)
    {
        // Karakterin X ve Y eksenlerinde 50 derece döndür
        characterTransform.Rotate(Vector3.right * -50);
        characterTransform.Rotate(Vector3.up * -50);
    }

    IEnumerator ChangeScaleCoroutine()
    {
        Vector3 originalScale = transform.localScale;

        while (true)
        {
            // X ekseninde boyutu artır, Z ekseninde boyutu azalt
            transform.localScale = new Vector3(transform.localScale.x + 2, transform.localScale.y - 0.05f, transform.localScale.z);
            yield return new WaitForSeconds(0.5f);

            // Orijinal boyuta geri döndür
            transform.localScale = originalScale;
            yield return new WaitForSeconds(0.3f); 
        }
    }

    void UpdateSpeedBasedOnScore(float score)
    {
        // 3000 puanlık aralıklarla hız artışını kontrol et
        float nextThreshold = Mathf.Floor(score / 350) * 350;

        if (nextThreshold > lastScoreThreshold && score >= nextThreshold && score < 20000)
        {
            speed += 1;
            minSpeed += 1;
            lastScoreThreshold = nextThreshold; // Son artış puan eşiğini güncelle
        }
    }
}
