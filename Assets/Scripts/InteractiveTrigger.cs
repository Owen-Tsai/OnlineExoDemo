using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using HFPS.Systems;

/**
 * When player enters the trigger, prompt user with following actions:
 * - Press E to active certain UI element;
 * - Press F to open a url in browser, if `url` is specified;
 */
public class InteractiveTrigger : MonoBehaviour
{
    public GameObject panelToShow;
    public GameObject actionHintPanel;
    public string url;

    private bool isInTrigger;
    private HFPS_GameManager manager;

    private void Start()
    {
        isInTrigger = false;
        url = "";
        manager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<HFPS_GameManager>();
    }

    private void Update()
    {
        if (!isInTrigger) return;
        if (Keyboard.current.fKey.wasPressedThisFrame && url.Trim() != "")
        {
            Debug.Log("F key pressed");
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
        manager.LockPlayerControls(false, false, true, 3, true);
        manager.isPaused = true;
    }

    public void HidePanel()
    {
        panelToShow.SetActive(false);
        manager.LockPlayerControls(false, false, false, 0, false, true);
        manager.Unpause();
        if (isInTrigger)
        {
            actionHintPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            actionHintPanel.SetActive(true);
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            actionHintPanel.SetActive(false);
            isInTrigger = false;
        }
    }
}