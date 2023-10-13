using System;
using System.Collections;
using UnityEngine;

using UnityEngine.UI;



public class Roll : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public RectTransform content;
    private bool isScrolling = false;
    private ScrollRect rect;
    // Start is called before the first frame update
    void Start(){
        rect = GetComponent<ScrollRect>();
        
        StartCoroutine(ScrollContent());
    }

    IEnumerator ScrollContent()
    {
        while(true)
        {
            content.anchoredPosition += Vector2.up * Time.deltaTime;
            Debug.Log(rect.verticalScrollbar.value);
            if(rect.verticalNormalizedPosition < 0.1) {
                Debug.Log("reached bottom");
                rect.verticalScrollbar.value = 1;
            }
            yield return null;
        }
    }
}

