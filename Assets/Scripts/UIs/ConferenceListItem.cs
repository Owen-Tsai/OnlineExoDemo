using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConferenceListItem : MonoBehaviour
{
    public void LoadConferenceScene()
    {
        SceneManager.LoadScene("02ConferenceHall");
    }
}
