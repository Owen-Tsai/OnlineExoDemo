using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HFPS.Systems;
using UnityEngine.InputSystem;

public class PolicySearchTrigger : MonoBehaviour
{
    [SerializeField] GameObject actionHintPanel;
    [SerializeField] GameObject policySearchPanel;
    [SerializeField] string url;
    private bool isInTrigger;
    private HFPS_GameManager gameManager;

    // Start is called before the first frame update
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
            ShowPolicySearchPanel();
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

    public void ShowPolicySearchPanel()
    {
        policySearchPanel.SetActive(true);
        actionHintPanel.SetActive(false);
        gameManager.LockPlayerControls(true, true, true, 2, true, true);
    }

    public void HidePolicySearchPanel()
    {
        policySearchPanel.SetActive(false);
        if (isInTrigger)
        {
            actionHintPanel.SetActive(true);
        }
        gameManager.LockPlayerControls(false, false, false, 0, false, true);
        gameManager.Unpause();
    }
}
