using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PolicyInfo
{
    public string id;
    public string title;
    public string content;
    public string condition;
    public string support;
    public string department;
}

[System.Serializable]
public class PolicyList
{
    public PolicyInfo[] list;
}

[System.Serializable]
public class PolicyUIManager : MonoBehaviour
{
    [SerializeField] int selectedIndex = 0;
    [SerializeField] GameObject selectedItem;
    [SerializeField] List<GameObject> items;
    public GameObject contentPanel;
    public GameObject detailPanel;
    public GameObject itemPrefab;
    public string json;
    [SerializeField] PolicyList data;

    Color32 colorPanel = new Color32(0, 0, 0, 120);
    Color32 colorBlue = new Color32(12, 69, 192, 255);

    // Start is called before the first frame update
    void Start()
    {
        // Get all items via ajax
        data = JsonUtility.FromJson<PolicyList>(json);
        // assume that we have 10 entries
        for (int i = 0; i < data.list.Length; i++)
        {
            GameObject obj = Instantiate(itemPrefab, contentPanel.transform);
            // Debug.Log(obj.transform.Find("Title"));
            obj.transform.Find("Title").GetComponent<TMPro.TMP_Text>().text = data.list[i].title;
            items.Add(obj);
        }

        selectedItem = items[0];
        SetItemStyle();
        SetDetail();
    }

    public void OnItemSelect(GameObject item)
    {
        selectedItem = item;
        selectedIndex = item.transform.GetSiblingIndex() - 1;
        SetItemStyle();
        SetDetail();
    }

    public void SetDetail()
    {
        PolicyInfo info = data.list[selectedIndex];
        detailPanel.transform.Find("Title").GetComponent<TMPro.TMP_Text>().text = info.title;
        detailPanel.transform.Find("SecContent").Find("Content").GetComponent<TMPro.TMP_Text>().text = info.content;
        detailPanel.transform.Find("SecCondition").Find("Condition").GetComponent<TMPro.TMP_Text>().text = info.condition;
        detailPanel.transform.Find("SecDept").Find("Dept").GetComponent<TMPro.TMP_Text>().text = info.department;
        Canvas.ForceUpdateCanvases();
        detailPanel.GetComponent<VerticalLayoutGroup>().enabled = false;
        detailPanel.GetComponent<VerticalLayoutGroup>().enabled = true;
        Canvas.ForceUpdateCanvases();
        detailPanel.GetComponent<VerticalLayoutGroup>().enabled = false;
        detailPanel.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    private void SetItemStyle()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Transform tag = items[i].transform.Find("Image");
            if (i == selectedIndex)
            {
                items[i].GetComponent<Image>().color = Color.white;
                items[i].transform.Find("Title").GetComponent<TMPro.TMP_Text>().color = Color.black;
                tag.GetComponent<Image>().color = colorBlue;
                tag.Find("TagText").GetComponent<TMPro.TMP_Text>().color = Color.white;
            }
            else
            {
                items[i].GetComponent<Image>().color = new Color32(0, 0, 0, 120);
                items[i].transform.Find("Title").GetComponent<TMPro.TMP_Text>().color = Color.white;
                tag.GetComponent<Image>().color = Color.white;
                tag.Find("TagText").GetComponent<TMPro.TMP_Text>().color = colorBlue;
            }
        }
    }

    public void OpenInBrowser()
    {
        Application.OpenURL("http://www.jigaorencai.com:8088/policy/" + data.list[selectedIndex].id);
    }
}
