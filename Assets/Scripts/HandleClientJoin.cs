using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using HFPS.Systems;


public class HandleClientJoin : NetworkBehaviour
{
    [SerializeField] NetworkManager networkManager;

    new void OnStartClient()
    {
        GetComponent<HFPS_GameManager>().m_PlayerObj = networkManager.playerPrefab;
    }
}
