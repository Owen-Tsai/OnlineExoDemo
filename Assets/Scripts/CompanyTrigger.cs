using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using HFPS.Systems;

/**
 * When player enters the trigger, prompt user with following actions:
 * - Press E, to check the full list of jobs;
 * - Press F, to open specific URL in browser;
 */
public class CompanyTrigger : MonoBehaviour
{

    [SerializeField] GameObject actionHintPanel;
    [SerializeField] GameObject jobListPanel;
    [SerializeField] string url;
    private bool isInTrigger;
    private HFPS_GameManager gameManager;

    void Start()
    {
        isInTrigger = false;
        gameManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<HFPS_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInTrigger) return;
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Application.OpenURL(url);
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            ShowJobListPabel();
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

    public void ShowJobListPabel()
    {
        jobListPanel.SetActive(true);
        actionHintPanel.SetActive(false);
        gameManager.LockPlayerControls(true, true, true, 2, true, true);
    }

    public void HideJobListPanel()
    {
        jobListPanel.SetActive(false);
        if (isInTrigger)
        {
            actionHintPanel.SetActive(true);
        }
        gameManager.LockPlayerControls(false, false, false, 0, false, true);
        gameManager.Unpause();
    }
}
