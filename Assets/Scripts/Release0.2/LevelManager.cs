using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public struct CharacterMessage : NetworkMessage
{
    public string displayName;
    public int prefabIndex;
}

public class LevelManager : NetworkManager
{
    [Header("Cai Custom Fields")]
    public GameObject[] playerPrefabs;
    public Transform spawnPos;

    public override void Start()
    {
        base.Start();
        Debug.Log("Player name: " + PlayerAuthData.DisplayName);
        Debug.Log("Session ID: " + PlayerAuthData.SessionId);

        if (PlayerAuthData.SessionId == "host" || PlayerAuthData.SessionId == "admin")
        {
            Debug.Log("Start Host");
            StartHost();
        }
        else
        {
            StartClient();
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("server started");
        NetworkServer.RegisterHandler<CharacterMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        CharacterMessage msg = new CharacterMessage
        {
            displayName = PlayerAuthData.DisplayName,
            prefabIndex = PlayerAuthData.playerPrefabIndex,
        };

        NetworkClient.Send(msg);
    }

    private void OnCreatePlayer(NetworkConnectionToClient conn, CharacterMessage message)
    {
        playerPrefab = playerPrefabs[message.prefabIndex];
        GameObject obj = Instantiate(playerPrefabs[message.prefabIndex], spawnPos.position, spawnPos.rotation);
        obj.GetComponent<ChatController>().displayName = message.displayName;

        NetworkServer.AddPlayerForConnection(conn, obj);
    }
}
