using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/**
 * When player enters the trigger, play the video automatically;
 * And pause/stop it when the player is no longer in the trigger.
 */
public class VideoPlayerTrigger : MonoBehaviour
{
    [SerializeField] bool stopWhenExit;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        StopVideo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            PlayVideo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            if (stopWhenExit)
            {
                StopVideo();
            }
            else
            {
                PauseVideo();
            }
        }
    }

    private void PlayVideo()
    {
        videoPlayer.Play();
    }

    private void PauseVideo()
    {
        videoPlayer.Pause();
    }

    private void StopVideo()
    {
        videoPlayer.Stop();
    }
}
