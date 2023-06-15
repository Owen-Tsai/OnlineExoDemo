using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
class IConferenceInfo
{
    public short id;
    public string name;
    public string timeStart;
    public string timeEnd;
    public string count;
    public bool isActive;
}

[System.Serializable]
class IConferenceList
{
    public IConferenceInfo[] list;
}

public class ConferenceUIManager : MonoBehaviour
{
    public GameObject panel;
    public string json;
    public GameObject itemPrefab;

    private IConferenceList data;

    Color32 green = new Color32(22, 154, 11, 255);
    Color32 gray = new Color32(180, 180, 180, 255);
    Color32 grayDarker = new Color32(60, 60, 60, 255);

    void Start()
    {
        // Get all items via ajax
        data = JsonUtility.FromJson<IConferenceList>(json);

        // assume that we have 10 entries
        for (int i = 0; i < data.list.Length; i++)
        {
            var item = data.list[i];
            GameObject obj = Instantiate(itemPrefab, panel.transform);
            // Debug.Log(obj.transform.Find("Title"));
            obj.transform.Find("Name").GetComponent<TMPro.TMP_Text>().text = item.name;
            obj.transform.Find("Time").GetComponent<TMPro.TMP_Text>().text = item.timeStart + " - " + item.timeEnd;
            obj.transform.Find("Count").GetComponent<TMPro.TMP_Text>().text = "参会企业：" + item.count;

            Transform tag = obj.transform.Find("Tag");
            TMPro.TMP_Text tagText = tag.GetComponentInChildren<TMPro.TMP_Text>();
            if (item.isActive)
            {
                tag.GetComponent<Image>().color = green;
                tagText.color = Color.white;
            }
            else
            {
                tag.GetComponent<Image>().color = gray;
                tagText.color = grayDarker;
            }
        }
    }

    public void OnItemClick(GameObject item)
    {
        int index = item.transform.GetSiblingIndex();
        SceneManager.LoadScene("JobConfHall");
    }
}
