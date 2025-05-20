using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int wood = 0;
    public int playerHealth = 100;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("wood", wood);
        PlayerPrefs.SetInt("playerHealth", playerHealth);
    }

    public void LoadData()
    {
        wood = PlayerPrefs.GetInt("wood");
        playerHealth = PlayerPrefs.GetInt("playerHealth");
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
