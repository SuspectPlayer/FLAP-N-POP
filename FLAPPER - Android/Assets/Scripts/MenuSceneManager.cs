using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    private int audioMuted = 0;
    //1 = Muted && 0 == NotMuted

    [SerializeField] private AudioMixer globalMixer;
    [SerializeField] private Sprite audioEnabledImg;
    [SerializeField] private Sprite audioMutedImg;
    [SerializeField] private Image MuteBtnImageSource;

    private void Start()
    {
        audioMuted = PlayerPrefs.GetInt("MuteStatus");
        if(audioMuted == 0)
        {
            MuteBtnImageSource.sprite = audioEnabledImg;
            globalMixer.SetFloat("Master", 0);
        }
        else
        {
            MuteBtnImageSource.sprite = audioMutedImg;
            globalMixer.SetFloat("Master", -80);
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadNextScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex + 1);
    }

    public void OnAudioMuteBtnClick()
    {
        if (audioMuted == 0)
        {
            globalMixer.SetFloat("Master", -80);
            audioMuted = 1;
            PlayerPrefs.SetInt("MuteStatus", audioMuted);
            MuteBtnImageSource.sprite = audioMutedImg;
        }
        else
        {
            globalMixer.SetFloat("Master", 0);
            audioMuted = 0;
            PlayerPrefs.SetInt("MuteStatus", audioMuted);
            MuteBtnImageSource.sprite = audioEnabledImg;
        }
    }
}
