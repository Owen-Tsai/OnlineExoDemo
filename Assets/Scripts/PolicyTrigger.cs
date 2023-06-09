using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HFPS.Systems;

public class PolicyTrigger : MonoBehaviour
{
    [SerializeField] GameObject actionHintPanel;
    [SerializeField] GameObject policyListPanel;
    [SerializeField] string url;
    private bool isInTrigger;
    private HFPS_GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        isInTrigger = false;
        gameManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<HFPS_GameManager>();
    }

    void Update()
    {
        if (!isInTrigger) return;
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Application.OpenURL(url);
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            ShowPolicyListPanel();
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

    public void ShowPolicyListPanel()
    {
        policyListPanel.SetActive(true);
        actionHintPanel.SetActive(false);
        gameManager.LockPlayerControls(true, true, true, 2, true, true);
    }

    public void HidePolicyListPanel()
    {
        policyListPanel.SetActive(false);
        if (isInTrigger)
        {
            actionHintPanel.SetActive(true);
        }
        gameManager.LockPlayerControls(false, false, false, 0, false, true);
        gameManager.Unpause();
    }
}
