using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class DeadAndScore : MonoBehaviour
{
    public GameObject DeadText;
    public GameObject ScoreText;
    public GameObject ResetButton; // Reset butonunu tanımla
    public bool dead = false;
    float score = 0;
    
    void Start()
    {
        DeadText.SetActive(false);
        ResetButton.SetActive(false); // Başlangıçta reset butonu inaktif
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            score += Time.deltaTime * 50f; // Zaman tabanlı hesaplama
            int displayedScore = Mathf.FloorToInt(score); // Tam sayı olarak göster
            ScoreText.GetComponent<TextMeshProUGUI>().text = displayedScore.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            dead = true;
            DeadText.SetActive(true);
            ResetButton.SetActive(true); // Reset butonunu aktif hale getir
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            dead = true;
            DeadText.SetActive(true);
            ResetButton.SetActive(true); // Reset butonunu aktif hale getir
        }
    }

    // Reset butonuna tıklandığında çağrılacak fonksiyon
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }

    public float GetScore()
    {
        return score;
    }
}
