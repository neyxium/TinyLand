using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public List<InventoryItem> backpack = new List<InventoryItem>();
    private string backpackSavePath => Application.persistentDataPath + "/backpack.json";
    public int backpackMaxSpace = 100;
    public int backpackSpaceFilled = 0;
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
        PlayerPrefs.SetInt("backpackMaxSpace", backpackMaxSpace);
        PlayerPrefs.SetInt("backpackSpaceFilled", backpackSpaceFilled);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        wood = PlayerPrefs.GetInt("wood");
        playerHealth = PlayerPrefs.GetInt("playerHealth");
        backpackMaxSpace = PlayerPrefs.GetInt("backpackMaxSpace");
        backpackSpaceFilled = PlayerPrefs.GetInt("backpackSpaceFilled");
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    // BACKPACK STUFF

    public void SaveBackpack()
    {
        string json = JsonUtility.ToJson(new BackpackWrapper(backpack));
        System.IO.File.WriteAllText(backpackSavePath, json);
        Debug.Log("Backpack saved to: " + backpackSavePath);
    }

    public void LoadBackpack()
    {
        if (System.IO.File.Exists(backpackSavePath))
        {
            string json = System.IO.File.ReadAllText(backpackSavePath);
            BackpackWrapper wrapper = JsonUtility.FromJson<BackpackWrapper>(json);
            backpack = wrapper.items;
            Debug.Log("Backpack loaded.");
        }
        else
        {
            Debug.Log("No backpack save file found.");
            backpack = new List<InventoryItem>();
        }
    }

    public void AddToBackpack(string itemName, int quantity)
    {
        var existing = backpack.Find(item => item.itemName == itemName);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            backpack.Add(new InventoryItem(itemName, quantity));
        }
    }


    // Wrapper class for List<InventoryItem> (JsonUtility doesn't serialize lists directly)
    [System.Serializable]
    private class BackpackWrapper
    {
        public List<InventoryItem> items;

        public BackpackWrapper(List<InventoryItem> items)
        {
            this.items = items;
        }
    }

}
