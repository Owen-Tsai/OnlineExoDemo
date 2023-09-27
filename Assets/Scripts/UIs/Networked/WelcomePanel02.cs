using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HFPS.Systems;
using Mirror;
using Unity.VisualScripting;
using HFPS.Player;
using Cai.UI.Networked;
public class WelcomePanel02 : MonoBehaviour
{
  [SerializeField] Transform[] transforms;
   private HFPS_GameManager manager;
   GameObject localPlayer;
   public MouseLook mouseLook;
    private void Start()
    {
         manager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<HFPS_GameManager>();
         localPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    public void Teleport(int i) {
        
        manager.LockPlayerControls(false, false, true, 3, true);

        localPlayer.transform.SetPositionAndRotation(
            transforms[i].position,
            transforms[i].rotation
        );

        // Debug.Log(transforms[i].eulerAngles.y);
        mouseLook.SetRotation(new Vector2(transforms[i].eulerAngles.y, 0));
        ClosePanel();
    }

    public void ClosePanel() {
        manager.LockPlayerControls(false, false, false, 0, false, true);
        manager.Unpause();
        gameObject.SetActive(false);
    }
}
