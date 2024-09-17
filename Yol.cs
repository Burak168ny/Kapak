using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yol : MonoBehaviour
{
    public DeadAndScore deadAndScore; // DeadAndScore referansını ekleyin

    void Start()
    {
        // Yol parçalarını taşımak
        roads[0].localPosition += new Vector3(-1598f, 0f, 0f);
        hasMoved = true;

        // Yol parçalarını tersine çevirme
        Array.Reverse(roads);

        // Sıradaki listeyi yeniden konumlandırma
        if (currentList == 0)
        {
            StartCoroutine(ResetHasObjectsMoved());
            StartCoroutine(RepositionAndToggleKinematic(objectsToReposition1));
            currentList = 1;
        }
        else
        {
            StartCoroutine(ResetHasObjectsMoved());
            StartCoroutine(RepositionAndToggleKinematic(objectsToReposition2));
            currentList = 0;
        }

        // Reset hasMoved
        StartCoroutine(ResetHasMoved());
    }
    
    public Transform[] roads;
    public GameObject[] objectsToReposition1; // İlk liste
    public GameObject[] objectsToReposition2; // İkinci liste
    public Vector3 spawnAreaSize; // Cisimlerin oluşturulacağı alanın boyutu

    private bool hasMoved = false;
    private int currentList = 0; // Hangi listenin sırada olduğunu takip etmek için

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "roadSpawnPoint" && !hasMoved)
        {
            // Yol parçalarını taşımak
            roads[0].localPosition += new Vector3(-1598f, 0f, 0f);
            hasMoved = true;

            // Yol parçalarını tersine çevirme
            Array.Reverse(roads);

            // Sıradaki listeyi yeniden konumlandırma
            if (currentList == 0)
            {
                StartCoroutine(ResetHasObjectsMoved());
                StartCoroutine(RepositionAndToggleKinematic(objectsToReposition1));
                currentList = 1;
            }
            else
            {
                StartCoroutine(ResetHasObjectsMoved());
                StartCoroutine(RepositionAndToggleKinematic(objectsToReposition2));
                currentList = 0;
            }

            // Reset hasMoved
            StartCoroutine(ResetHasMoved());
        }
    }

    private IEnumerator ResetHasMoved()
    {
        // Belirli bir süre bekleyin (örneğin 0.1 saniye)
        yield return new WaitForSeconds(0.1f);
        hasMoved = false;
    }

    private IEnumerator ResetHasObjectsMoved()
    {
        // Belirli bir süre bekleyin (örneğin 0.1 saniye)
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator RepositionAndToggleKinematic(GameObject[] objectsToReposition)
    {
        if (roads.Length > 1) // roads dizisinin en az iki eleman içerdiğini kontrol edin
        {
            // roads[1] pozisyonunu almak
            Vector3 roadPosition = roads[1].position;

            // Belirli bir alanda rastgele pozisyonlar oluşturma
            Vector3 spawnCenter = roadPosition; // Spawn merkezini roads[1] pozisyonuna ayarlayın

            // `isKinematic` özelliğini devre dışı bırak
            foreach (GameObject obj in objectsToReposition)
            {
                if (obj != null)
                {
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                }
            }

            int maxObjects = GetMaxObjectsBasedOnScore(); // Skora göre objelerin sayısını al

            for (int i = 0; i < Mathf.Min(objectsToReposition.Length, maxObjects); i++)
            {
                GameObject obj = objectsToReposition[i];
                if (obj != null)
                {
                    // Rastgele bir pozisyon
                    Vector3 randomPosition = new Vector3(
                        UnityEngine.Random.Range(spawnCenter.x - 7f, spawnCenter.x + spawnAreaSize.x),
                        UnityEngine.Random.Range(spawnCenter.y + 4, spawnCenter.y + spawnAreaSize.y), // Y ekseni için rastgele değer
                        UnityEngine.Random.Range(spawnCenter.z, spawnCenter.z + spawnAreaSize.z)
                    );

                    // Objeyi yeni pozisyona taşıma
                    obj.transform.position = randomPosition;
                }
            }

            // 1 saniye bekle
            yield return new WaitForSeconds(1f);

            // `isKinematic` özelliğini tekrar etkinleştir
            foreach (GameObject obj in objectsToReposition)
            {
                if (obj != null)
                {
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                    }
                }
            }
        }
    }

    private int GetMaxObjectsBasedOnScore()
    {
        float score = deadAndScore.GetScore(); // DeadAndScore'dan skoru al

        if (score > 3500)
            return 60;
        else if (score > 2000)
            return 60;
        else if (score > 1000)
            return 45;
        else if (score > 300)
            return 30;
        else
            return 15;
    }
}
