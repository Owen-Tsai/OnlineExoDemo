using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cai.Lobby
{
    public class PlayerLogin : MonoBehaviour
    {
        [SerializeField] Sprite[] avatarList;
        [SerializeField] Image avatarPreviewImage;
        private int avatarIdx = 0;

        [SerializeField] TMPro.TMP_InputField nameInput;
        [SerializeField] TMPro.TMP_InputField sessionInput;
        [SerializeField] Button continueButton;

        public static string DisplayName { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            avatarPreviewImage.sprite = avatarList[0];
        }

        public void PrevAvatar()
        {
            avatarIdx--;
            if (avatarIdx <= 0)
            {
                avatarIdx = avatarList.Length - 1;
            }
            avatarPreviewImage.sprite = avatarList[avatarIdx];
            PlayerAuthData.playerPrefabIndex = avatarIdx;
        }

        public void NextAvatar()
        {
            avatarIdx++;
            if (avatarIdx >= avatarList.Length)
            {
                avatarIdx = 0;
            }
            avatarPreviewImage.sprite = avatarList[avatarIdx];
            PlayerAuthData.playerPrefabIndex = avatarIdx;
        }

        private bool IsFormValid()
        {
            return nameInput.GetComponent<TMPro.TMP_InputField>().text.Trim() != ""
                   && sessionInput.GetComponent<TMPro.TMP_InputField>().text.Trim() != "";
        }

        public void OnInputValueChanged()
        {
            continueButton.interactable = IsFormValid();
            PlayerAuthData.DisplayName = nameInput.GetComponent<TMPro.TMP_InputField>().text;
            PlayerAuthData.SessionId = sessionInput.GetComponent<TMPro.TMP_InputField>().text;
        }

        public void StartGame()
        {
            SceneManager.LoadScene("01ServiceHall");
            //SceneManager.LoadScene("Test_Chat");
        }
    }
}
