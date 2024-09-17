using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private GameObject[] characterList;
    private int selectedIndex;

    private void Awake()
    {
        // PlayerPrefs'den seçilen karakter indexini al
        selectedIndex = PlayerPrefs.GetInt("CharacterSelected", 0);

        // Transform child'larını karakterList array'ine aktar
        characterList = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // Tüm karakterleri deaktif yap
        foreach(GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // Seçilen karakteri aktif yap
        if(characterList.Length > 0 && selectedIndex >= 0 && selectedIndex < characterList.Length)
        {
            characterList[selectedIndex].SetActive(true);
        }
    }
}
