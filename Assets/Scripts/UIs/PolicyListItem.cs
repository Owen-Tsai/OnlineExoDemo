using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyListItem : MonoBehaviour
{
    private PolicyUIManager policyUIManager;

    private void Start()
    {
        policyUIManager = GetComponentInParent<PolicyUIManager>();
    }

    public void OnItemClick()
    {
        policyUIManager.OnItemSelect(gameObject);
    }
}
