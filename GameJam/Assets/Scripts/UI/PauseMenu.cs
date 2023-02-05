using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame

    public GameObject pauseFirstButton;

    private void Start()
    {
        Resume();

    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }


        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;


    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);


    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();

    }
}
