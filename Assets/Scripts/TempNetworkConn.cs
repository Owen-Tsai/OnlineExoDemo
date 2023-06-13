using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class TempNetworkConn : MonoBehaviour
{
    private NetworkManager manager;
    private bool joined = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if (joined) return;
        Keyboard kb = Keyboard.current;
        if (kb.xKey.wasPressedThisFrame)
        {
            manager.StartHost();
            joined = true;
        }
        if (kb.zKey.wasPressedThisFrame)
        {
            manager.StartClient();
            joined = true;
        }
    }
}
