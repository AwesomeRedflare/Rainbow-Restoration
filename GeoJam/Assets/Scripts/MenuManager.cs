using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject transition;
    public GameObject levelSelectPanel;
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    void CloseTransition()
    {
        transition.GetComponent<Animator>().SetTrigger("Close");
    }

    void OpenTransition()
    {
        transition.GetComponent<Animator>().SetTrigger("Open");
    }

    void GoToMainMenu()
    {
        controlsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        OpenTransition();
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        CloseTransition();

        Invoke("GoToMainMenu", .5f);
    }

    void GoToControls()
    {
        controlsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        OpenTransition();
    }

    public void Controls()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        CloseTransition();

        Invoke("GoToControls", .5f);
    }

    void GoToLevelSelect()
    {
        levelSelectPanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        OpenTransition();
    }

    public void LevelSelect()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        CloseTransition();

        Invoke("GoToLevelSelect", .5f);
    }

    public void LevelButton(int l)
    {
        StartCoroutine("LevelSelector", l);
    }

    
    IEnumerator LevelSelector(int level)
    {
        FindObjectOfType<AudioManager>().Play("Button");
        CloseTransition();

        yield return new WaitForSeconds(.5f);

        SceneManager.LoadScene(level);
    }
}
