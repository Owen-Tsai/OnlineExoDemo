using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using HFPS.Systems;

namespace Cai.UI.Networked {
    
    public class WelcomeTrigger02 : MonoBehaviour
    {
        // 将会显示的 UI 元素
        [SerializeField] GameObject uiElement;

        private bool hasShown = false;
        private HFPS_GameManager manager;
        void Start()
        {
          Debug.Log(hasShown);
            hasShown = false;
            uiElement.SetActive(false);
            manager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<HFPS_GameManager>();
        }

        // private bool IsOwned(GameObject obj) {
        //     return obj.GetComponent<NetworkIdentity>().isOwned;
        // }

        private void OnTriggerEnter(Collider other) {
            if (
                other.gameObject.CompareTag("Player")
                // && IsOwned(other.gameObject)
                && !hasShown
            ) {
                
                // hasShown = true;
                uiElement.SetActive(true);
                manager.LockPlayerControls(false, false, true, 3, true);
                manager.isPaused = true;
                // NetworkClient.localPlayer.GetComponent<HFPS_GameManager>().LockPlayerControls(false, false, true, 3, true);
            }
        }

        public void ClosePanel() {
            uiElement.SetActive(false);
            manager.LockPlayerControls(false, false, false, 0, false, true);
            // NetworkClient.localPlayer.GetComponent<HFPS_GameManager>().LockPlayerControls(false, false, false, 0, false, true);
        }
    }
}