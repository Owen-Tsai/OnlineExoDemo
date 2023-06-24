using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicesUIManager : MonoBehaviour
{
    public void VisitUrl(string url)
    {
        Application.OpenURL(url);
    }
}
