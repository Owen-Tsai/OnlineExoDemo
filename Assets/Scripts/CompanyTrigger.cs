using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CompanyTrigger : MonoBehaviour
{

    public GameObject hintPanel;
    private bool isInTrigger;
    public string hintText;
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        isInTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Application.OpenURL(url);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            hintPanel.SetActive(true);
            hintPanel.GetComponentInChildren<Text>().text = hintText != "" ? hintText : "按 E 键从浏览器查看详情";
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            hintPanel.SetActive(false);
            isInTrigger = false;
        }
    }
}
