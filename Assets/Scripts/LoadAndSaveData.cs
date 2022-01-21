using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plusieurs instance de SaveAndLoad dans la scène");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    public void Start()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();

        /*int currentHealth = PlayerPrefs.GetInt("playerHealth", PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = currentHealth;
        PlayerHealth.instance.healthBar.setHealth(currentHealth);*/

        GameValues.difficulty = (GameValues.Difficulties)PlayerPrefs.GetInt("difficulty", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("difficulty", (int)GameValues.difficulty);
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);
        PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);

        if(CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }
        //PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.currentHealth);
    }
}
