using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Cai.UI.Networked {
    public class WelcomeTrigger : MonoBehaviour
    {
        // 将会显示的 UI 元素
        [SerializeField] GameObject uiElement;

        private bool hasShown = false;

        void Start()
        {
            hasShown = false;
            uiElement.SetActive(false);
        }

        private bool IsOwned(GameObject obj) {
            return obj.GetComponent<NetworkIdentity>().isOwned;
        }

        private void OnTriggerEnter(Collider other) {
            if (
                other.gameObject.CompareTag("Player")
                && IsOwned(other.gameObject)
                && !hasShown
            ) {
                hasShown = true;
                uiElement.SetActive(true);
                NetworkClient.localPlayer.GetComponent<PlayerControllerFPS>().LockControl();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("Player") && IsOwned(other.gameObject)) {
                Debug.Log("Exit Trigger");
                uiElement.SetActive(false);
                NetworkClient.localPlayer.GetComponent<PlayerControllerFPS>().UnlockControl();
            }
        }

        public void ClosePanel() {
            uiElement.SetActive(false);
            NetworkClient.localPlayer.GetComponent<PlayerControllerFPS>().UnlockControl();
        }
    }
}
