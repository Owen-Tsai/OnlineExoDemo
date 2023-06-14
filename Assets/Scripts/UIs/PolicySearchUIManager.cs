using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
class IPolicyInfoD
{
    public string title;
    public string region;
    public string subject;
    public string supportType;
}

[System.Serializable]
class IPolicyListD
{
    public IPolicyInfoD[] rows;
    public bool success;
    public int total;
}

public class PolicySearchUIManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject itemPrefab;
    IPolicyListD data;

    [SerializeField] string goal;
    [SerializeField] string nationality;
    [SerializeField] string age;
    [SerializeField] string degree;
    [SerializeField] string type;
    [SerializeField] string title;
    [SerializeField] string page;

    public GameObject[] goalOptions;
    public GameObject[] nationalityOptions;
    public GameObject[] educationOptions;
    public GameObject[] typeOptions;
    public GameObject[] titleOptions;

    private int selectedGoal = 0;
    private int selectedNationality = 0;
    private int selectedDegree = 0;
    private int selectedType = 0;
    private int selectedTitle = 0;

    Color32 colorBlue = new Color32(12, 69, 192, 255);
    Color32 colorBg = new Color32(0, 0, 0, 60);

    void Start()
    {
        StartCoroutine(FetchList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FetchList()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.jigaorencai.com:8088/api?m=PublicWebsiteApp&f=getPoliceMatch&goal=" + goal + "&nationality=" + nationality + "&age=" + age + "&degree=" + degree + "&type=" + type + "&title=" + title + "&page=1");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.error);
            yield break;
        }

        string response = www.downloadHandler.text;
        Debug.Log(response);
        data = JsonUtility.FromJson<IPolicyListD>(response);

        GenerateList();
    }

    void GenerateList()
    {
        IPolicyInfoD[] list = data.rows;
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < list.Length; i++)
        {
            var item = list[i];
            GameObject obj = Instantiate(itemPrefab, panel.transform);
            // Debug.Log(obj.transform.Find("Title"));
            obj.transform.Find("Title").GetComponent<TMPro.TMP_Text>().text = item.title;
            obj.transform.Find("Region").GetComponent<TMPro.TMP_Text>().text = "适用区域：" + item.region;
            obj.transform.Find("Type").GetComponent<TMPro.TMP_Text>().text = "支持类型：" + item.supportType;
            obj.transform.Find("Subject").GetComponent<TMPro.TMP_Text>().text = "申报对象：" + item.subject.Replace("\r\n", "");
        }
    }

    public void OnGoalChange(string str)
    {
        string[] _params = str.Split(",");
        selectedGoal = int.Parse(_params[0]);
        goal = _params[1];
        StartCoroutine(FetchList());

        for (int i = 0; i < goalOptions.Length; i++)
        {
            if (i == selectedGoal)
            {
                // set active style
                SetActiveStyle(goalOptions[i]);
            }
            else
            {
                // set inactive style
                SetInactiveStyle(goalOptions[i]);
            }
        }
    }
    public void OnNationalityChange(string str)
    {
        string[] _params = str.Split(",");
        selectedGoal = int.Parse(_params[0]);
        nationality = _params[1];
        StartCoroutine(FetchList());

        for (int i = 0; i < nationalityOptions.Length; i++)
        {
            if (i == selectedGoal)
            {
                // set active style
                SetActiveStyle(nationalityOptions[i]);
            }
            else
            {
                // set inactive style
                SetInactiveStyle(nationalityOptions[i]);
            }
        }
    }
    public void OnEducationChange(string str)
    {
        string[] _params = str.Split(",");
        selectedGoal = int.Parse(_params[0]);
        nationality = _params[1];
        StartCoroutine(FetchList());

        for (int i = 0; i < educationOptions.Length; i++)
        {
            if (i == selectedGoal)
            {
                // set active style
                SetActiveStyle(educationOptions[i]);
            }
            else
            {
                // set inactive style
                SetInactiveStyle(educationOptions[i]);
            }
        }
    }
    public void OnTypesChange(string str)
    {
        string[] _params = str.Split(",");
        selectedGoal = int.Parse(_params[0]);
        nationality = _params[1];
        StartCoroutine(FetchList());

        for (int i = 0; i < typeOptions.Length; i++)
        {
            if (i == selectedGoal)
            {
                // set active style
                SetActiveStyle(typeOptions[i]);
            }
            else
            {
                // set inactive style
                SetInactiveStyle(typeOptions[i]);
            }
        }
    }
    public void OnTitlesChange(string str)
    {
        string[] _params = str.Split(",");
        selectedGoal = int.Parse(_params[0]);
        nationality = _params[1];
        StartCoroutine(FetchList());

        for (int i = 0; i < titleOptions.Length; i++)
        {
            if (i == selectedGoal)
            {
                // set active style
                SetActiveStyle(titleOptions[i]);
            }
            else
            {
                // set inactive style
                SetInactiveStyle(titleOptions[i]);
            }
        }
    }
    public void OnAgeChange(string val)
    {
        age = val;
        StartCoroutine(FetchList());
    }
    public void OnDegreeChange(string val)
    {
        degree = val;
        StartCoroutine(FetchList());
    }
    public void OnTypeChange(string val)
    {
        type = val;
        StartCoroutine(FetchList());
    }
    public void OnTitleChange(string val)
    {
        title = val;
        StartCoroutine(FetchList());
    }

    private void SetActiveStyle(GameObject option)
    {
        option.GetComponent<Image>().color = Color.white;
        option.GetComponentInChildren<TMPro.TMP_Text>().color = colorBlue;
    }

    private void SetInactiveStyle(GameObject option)
    {
        option.GetComponent<Image>().color = colorBg;
        option.GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
    }
}
