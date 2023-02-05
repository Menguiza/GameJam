using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseFirstButton;
    private void Start()
    {
        Time.timeScale = 1;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
    public void PlayGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene") - 1);


    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
