using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;
    public CameraFollow cameraFollow; // CameraFollow script'ine referans

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected", 0); // Varsayılan olarak 0, eğer hiç seçim yapılmamışsa

        characterList = new GameObject[transform.childCount];

        for (int j = 0; j < transform.childCount; j++)
            characterList[j] = transform.GetChild(j).gameObject;

        foreach (GameObject go in characterList)
            go.SetActive(false);

        if (characterList.Length > 0 && index >= 0 && index < characterList.Length)
            characterList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;

        characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        characterList[index].SetActive(false);

        index++;
        if (index >= characterList.Length)
            index = 0;

        characterList[index].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        PlayerPrefs.Save(); // Verilerin kaydedilmesini sağla
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(index); // Kameranın target'ını seçilen karakter yap
        }
        SceneManager.LoadScene("Kapak");
    }
}
