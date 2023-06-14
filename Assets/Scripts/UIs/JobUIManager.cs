using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class JobInfo
{
    public string id;
    public string jobname;
    public string describe;
    public string salary;
    public string age;
    public string address;
}

[System.Serializable]
public class JobList
{
    public JobInfo[] list;
}

public class JobUIManager : MonoBehaviour
{
    [SerializeField] int selectedIndex = 0;
    [SerializeField] GameObject selectedItem;
    [SerializeField] List<GameObject> items;
    public GameObject contentPanel;
    public GameObject detailPanel;
    public GameObject itemPrefab;

    public string json;
    [SerializeField] JobList data;

    Color32 colorPanel = new Color32(0, 0, 0, 120);
    Color32 colorBlue = new Color32(12, 69, 192, 255);
    Color32 colorOrange = new Color32(254, 160, 55, 255);

    // Start is called before the first frame update
    void Start()
    {
        // Get all items via ajax
        data = JsonUtility.FromJson<JobList>(json);

        // assume that we have 10 entries
        for (int i = 0; i < data.list.Length; i++)
        {
            GameObject obj = Instantiate(itemPrefab, contentPanel.transform);
            // Debug.Log(obj.transform.Find("Title"));
            var item = data.list[i];
            obj.transform.Find("Title").GetComponent<TMPro.TMP_Text>().text = item.jobname;
            obj.transform.Find("Salary").GetComponent<TMPro.TMP_Text>().text = item.salary;
            obj.transform.Find("Delivered").GetComponent<TMPro.TMP_Text>().text = Random.Range(1, 100) + "人已投递";
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
        JobInfo info = data.list[selectedIndex];
        detailPanel.transform.Find("Title").GetComponent<TMPro.TMP_Text>().text = info.jobname;
        detailPanel.transform.Find("InfoRow1").Find("Salary").GetComponent<TMPro.TMP_Text>().text = info.salary;
        var row2 = detailPanel.transform.Find("InfoRow2");
        row2.Find("Exp").GetComponent<TMPro.TMP_Text>().text = info.age;
        row2.Find("Degree").GetComponent<TMPro.TMP_Text>().text = "本科及以上学历";
        detailPanel.transform.Find("SecContent").Find("Content").GetComponent<TMPro.TMP_Text>().text = info.describe;
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
            }
            else
            {
                items[i].GetComponent<Image>().color = new Color32(0, 0, 0, 120);
                items[i].transform.Find("Title").GetComponent<TMPro.TMP_Text>().color = Color.white;
            }
        }
    }

    public void OpenInBrowser()
    {
        Application.OpenURL("http://www.jigaorencai.com:8088/employment/job/" + data.list[selectedIndex].id);
    }
}
