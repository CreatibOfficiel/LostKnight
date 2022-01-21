using UnityEngine;
using UnityEngine.SceneManagement;

public class GameValues : MonoBehaviour
{
    public enum Difficulties { Easy, Medium, Hard };

    public static Difficulties difficulty = Difficulties.Easy;

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsWindow;
    public GameObject winWindow;

    public static GameValues instance;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Probleme instance GameValues");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused("");
            }
        }
    }

    public void Paused(string str)
    {
        PlayerMovement.instance.enabled = false;
        if (str == "")
        {
            pauseMenuUI.SetActive(true);
        }
        else if(str == "Win")
        {
            winWindow.SetActive(true);
        }
        
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        if(gameIsPaused)
        {
            pauseMenuUI.SetActive(false);
        }else 
            winWindow.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCredits()
    {
        Resume();
        SceneManager.LoadScene("Credits");
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

}
