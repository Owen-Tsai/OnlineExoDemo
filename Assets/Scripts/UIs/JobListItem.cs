using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobListItem : MonoBehaviour
{
    private JobUIManager jobUIManager;

    private void Start()
    {
        jobUIManager = GetComponentInParent<JobUIManager>();
    }

    public void OnItemClick()
    {
        jobUIManager.OnItemSelect(gameObject);
    }
}
