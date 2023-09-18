using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WelcomePanel : MonoBehaviour
{
    [SerializeField] Transform[] transforms;

    public void Teleport(int i) {
        GameObject localPlayer = NetworkClient.localPlayer.gameObject;
        localPlayer.transform.SetPositionAndRotation(
            transforms[i].position,
            transforms[i].rotation
        );
    }

    public void ClosePanel() {
        gameObject.SetActive(false);
        NetworkClient.localPlayer.GetComponent<PlayerControllerFPS>().UnlockControl();
    }
}
