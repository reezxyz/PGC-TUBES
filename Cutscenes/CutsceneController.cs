using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene = "Level1_Orientation";

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
}
