using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using HFPS.Systems;
using Mirror;

/**
 * When player enters the trigger, prompt user with following actions:
 * - Press E to active certain UI element;
 * - Press F to open a url in browser, if `url` is specified;
 */
public class NetworkedInteractiveTrigger : NetworkBehaviour
{
    public GameObject panelToShow;
    public GameObject actionHintPanel;
    public string url;

    private bool isInTrigger;
    private PlayerControllerFPS controller;

    /*public override void OnStartAuthority()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerFPS>();
        isInTrigger = false;
    }*/

    private void Start ()
    {
        isInTrigger = false;
    }

    private void Update()
    {
        if (!isInTrigger) return;
        if (Keyboard.current.fKey.wasPressedThisFrame && url.Trim() != "")
        {
            Application.OpenURL(url);
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            ShowPanel();
        }
    }

    private void ShowPanel()
    {
        panelToShow.SetActive(true);
        actionHintPanel.SetActive(false);
        NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerFPS>().LockControl();
    }

    public void HidePanel()
    {
        panelToShow.SetActive(false);
        NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerFPS>().UnlockControl();
        if (isInTrigger)
        {
            actionHintPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player" && other.gameObject == NetworkClient.localPlayer.gameObject)
        {
            actionHintPanel.SetActive(true);
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player" && other.gameObject == NetworkClient.localPlayer.gameObject)
        {
            actionHintPanel.SetActive(false);
            isInTrigger = false;
        }
    }
}