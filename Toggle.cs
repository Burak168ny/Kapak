// using UnityEngine;
// using UnityEngine.UI; // Toggle butonunu kullanmak için gerekli

// public class Toggle : MonoBehaviour
// {
//     public Button toggleButton; // Toggle butonunu buraya atayın
//     public Transform character; // Karakterinizin Transform'u

//     private bool isButtonActive = false;

//     void Start()
//     {
//         if (toggleButton != null)
//         {
//             toggleButton.gameObject.SetActive(false); // Başlangıçta buton gizli
//         }
//     }

//     void Update()
//     {
//         Vector3 eulerAngles = character.rotation.eulerAngles;

//         // Y rotasyonu 0 ile 180 arasında değilse
//         if (eulerAngles.y < -20 || eulerAngles.y > 220 || eulerAngles.z < -20 || eulerAngles.z > 220)
//         {
//             if (!isButtonActive)
//             {
//                 if (toggleButton != null)
//                 {
//                     toggleButton.gameObject.SetActive(true);
//                     isButtonActive = true;
//                 }
//             }
//         }
//         else
//         {
//             if (isButtonActive)
//             {
//                 if (toggleButton != null)
//                 {
//                     toggleButton.gameObject.SetActive(false);
//                     isButtonActive = false;
//                 }
//             }
//         }
//     }

//     public void OnToggleButtonClicked()
//     {
//         if (character != null)
//         {
//             // Karakterin rotasını 0, 90, 90 yap
//             character.rotation = Quaternion.Euler(0, 90, 90);

//             // Butonu tekrar gizle
//             if (toggleButton != null)
//             {
//                 toggleButton.gameObject.SetActive(false);
//             }
//         }
//     }
// }
