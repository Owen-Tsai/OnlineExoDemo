using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Mirror;
using System;

public class ChatController : NetworkBehaviour
{
    [SerializeField] GameObject chatPanel;
    [SerializeField] TMPro.TMP_InputField chatInput;
    [SerializeField] TMPro.TMP_Text chatText;
    [SerializeField] ScrollRect scrollViewContent;
    Keyboard kb;
    public string displayName;

    bool isChatPanelActivated = false;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        kb = Keyboard.current;
        chatPanel = GameObject.Find("Panel_Chat");
        chatInput = chatPanel.transform.Find("ChatInput").GetComponent<TMPro.TMP_InputField>();
        chatText = GameObject.Find("ChatText").GetComponent<TMPro.TMP_Text>();
        scrollViewContent = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

        chatPanel.SetActive(false);
        chatInput.onEndEdit.AddListener(delegate { Send(chatInput.text); });

        OnMessage += HandleMessage;
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!isOwned) return;
        OnMessage -= HandleMessage;
    }

    private void HandleMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }
        CmdSendMessage(chatInput.text);
        chatInput.text = string.Empty;
        chatInput.Select();
        chatInput.Select();
    }

    [Command]
    public void CmdSendMessage(string message)
    {
        RpcHandleMessage($"[{displayName}]: {message}");
    }

    [ClientRpc]
    public void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
        Canvas.ForceUpdateCanvases();
        scrollViewContent.verticalNormalizedPosition = 0;
        Canvas.ForceUpdateCanvases();
    }


    void Update()
    {
        if (!isOwned) return;
        if (kb.slashKey.wasPressedThisFrame && !isChatPanelActivated)
        {
            chatPanel.SetActive(true);
            isChatPanelActivated = true;
            NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerFPS>().LockControl();
            chatInput.Select();
        }

        if (kb.escapeKey.wasPressedThisFrame && isChatPanelActivated) 
        {
            chatPanel.SetActive(false);
            isChatPanelActivated = false;
            NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerFPS>().UnlockControl();
        }
    }
}
