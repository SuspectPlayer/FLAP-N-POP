using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    private bool audioMuted = false;
    public AudioMixer globalMixer;
    public Sprite audioEnabledImg;
    public Sprite audioMutedImg;
    public Image MuteBtnImageSource;

    private void Start()
    {
       // MuteBtnImageSource
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
        if (!audioMuted)
        {
            globalMixer.SetFloat("Master", -80);
            audioMuted = true;
            MuteBtnImageSource.GetComponent<Image>().sprite = audioMutedImg;
        }
        else
        {
            globalMixer.SetFloat("Master", 0);
            audioMuted = false;
            MuteBtnImageSource.GetComponent<Image>().sprite = audioEnabledImg;
        }
    }
}
