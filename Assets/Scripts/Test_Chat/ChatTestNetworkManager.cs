using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

namespace Cai.Lobby
{

    public struct CreatePlayerMessage : NetworkMessage
    {
        public string displayName;
    }

    public class ChatTestNetworkManager : NetworkManager
    {
        public Transform spawnPos;

        public override void Start()
        {
            base.Start();
            Debug.Log(PlayerAuthData.DisplayName + ", " + PlayerAuthData.SessionId);

            if (PlayerAuthData.SessionId == "admin")
            {
                Debug.Log("Start Host");
                StartHost();
            }
            else
            {
                Debug.Log("Start Client");
                StartClient();
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
        }

        private void OnCreatePlayer(NetworkConnectionToClient conn, CreatePlayerMessage message)
        {
            Debug.Log("on create player");
            GameObject obj = Instantiate(playerPrefab, spawnPos);
            obj.GetComponent<ChatTestController>().displayName = message.displayName;

            NetworkServer.AddPlayerForConnection(conn, obj);
        }


    }
}

