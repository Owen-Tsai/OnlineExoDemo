using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

namespace Cai.Lobby
{
    public class ChatTestController : NetworkBehaviour
    {
        Keyboard keyboard;
        public string displayName;
        
        // Start is called before the first frame update
        void Start()
        {
            keyboard = Keyboard.current;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOwned) return;
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                transform.Translate(new Vector3(0, 0.5f, 0));
            }
        }
    }
}
