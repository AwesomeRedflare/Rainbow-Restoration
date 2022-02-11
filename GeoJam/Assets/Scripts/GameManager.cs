using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject transition;
    public GameObject levelCompletePanel;
    public GameObject pauseScreen;

    private void Start()
    {
        transition.SetActive(true);

        FindObjectOfType<AudioManager>().Destroy();
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
    }

    public void PauseScreen()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        pauseScreen.SetActive(false);
        FindObjectOfType<GoodPlatformerController>().Unpause();
    }

    void CloseTransition()
    {
        transition.GetComponent<Animator>().SetTrigger("Close");
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Death()
    {
        CloseTransition();

        Invoke("ResetScene", .5f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Win()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        CloseTransition();

        Invoke("NextLevel", .5f);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        CloseTransition();

        Invoke("GoToMainMenu", .5f);
    }
}
