using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using HFPS.Systems;

public class HandlePlayerJoin : NetworkBehaviour
{
    [SerializeField] ScriptManager scriptManager;

    new void OnStartClient()
    {
        scriptManager.m_GameManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<HFPS_GameManager>();
    }
}
