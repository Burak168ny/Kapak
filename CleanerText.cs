using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro namespace'i

public class CleanerText : MonoBehaviour
{
    TextMeshProUGUI text;  // TextMeshProUGUI nesnesi
    public static int cleanerAmount;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();  // TextMeshProUGUI bileşenini al
        cleanerAmount = 0;  // cleanerAmount değerini sıfırla
    }

    // Update is called once per frame
    void Update()
    {
        text.text = cleanerAmount.ToString();  // TextMeshProUGUI ile metni güncelle
    }
}
