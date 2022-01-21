using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public Collider2D player;
    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }
        instance = this;
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        if(GameValues.difficulty == GameValues.Difficulties.Easy)
        {
            player.gameObject.transform.position = CurrentSceneManager.instance.respawnPoint;
        }
        else
        {
            Inventory.instance.removeCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
        PlayerHealth.instance.Respawn();
        gameOverUI.SetActive(false);
    }

    public void MainButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        // quitter
        Application.Quit();
    }
}
