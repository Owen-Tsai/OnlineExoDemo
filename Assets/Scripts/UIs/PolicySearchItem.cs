using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicySearchITem : MonoBehaviour
{
    private PolicySearchUIManager manager;

    private void Start()
    {
        manager = GetComponentInParent<PolicySearchUIManager>();
    }
    public void OnClick()
    {
        manager.OnItemClick(gameObject);
    }
}
