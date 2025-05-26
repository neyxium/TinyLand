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
    public int trees;
    public int saplings;
    public int plantedSaplings;
    public bool firstTime = true;
    public int questProgress = 0;
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
    private void Start()
    {
        LoadData();
        LoadBackpack();
        //ResetData();
        //ClearBackpack();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("wood", wood);
        PlayerPrefs.SetInt("playerHealth", playerHealth);
        PlayerPrefs.SetInt("backpackMaxSpace", backpackMaxSpace);
        PlayerPrefs.SetInt("backpackSpaceFilled", backpackSpaceFilled);
        PlayerPrefs.SetInt("trees", trees);
        PlayerPrefs.SetInt("saplings", saplings);
        PlayerPrefs.SetInt("plantedSaplings", plantedSaplings);
        PlayerPrefs.SetInt("firstTime", firstTime ? 1 : 0);
        PlayerPrefs.SetInt("questProgress", questProgress);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        wood = PlayerPrefs.GetInt("wood", 0);
        playerHealth = PlayerPrefs.GetInt("playerHealth", 100);
        backpackMaxSpace = PlayerPrefs.GetInt("backpackMaxSpace", 100);
        backpackSpaceFilled = PlayerPrefs.GetInt("backpackSpaceFilled", 0);
        trees = PlayerPrefs.GetInt("trees", -1);
        saplings = PlayerPrefs.GetInt("saplings", -1);
        plantedSaplings = PlayerPrefs.GetInt("plantedSaplings", -1);
        firstTime = PlayerPrefs.GetInt("firstTime", 1) == 1;
        questProgress = PlayerPrefs.GetInt("questProgress", 0);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
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

    public void ClearBackpack()
    {
        backpack.Clear();
        SaveBackpack();
    }

    public event System.Action OnInventoryChanged; // Creates an event for inventory changes

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

        OnInventoryChanged?.Invoke(); // Invokes event and tells all listeners that the inventory has changed (InventoryUI)
    }

    public Item GetItemByName(string name)
    {
        Item[] allItems = Resources.LoadAll<Item>("Items");
        foreach (Item item in allItems)
        {
            if (item.itemName == name)
                return item;
        }
        Debug.LogWarning("Item not found: " + name);
        return null;
    }

    public bool HasItem(string itemName)
    {
        foreach (InventoryItem item in backpack)
        {
            if (item.itemName == itemName && item.quantity > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void TakeAwayItem(string itemName, int ammount = 1)
    {
        foreach (InventoryItem item in backpack)
        {
            if (item.itemName == itemName && item.quantity > 0)
            {
                item.quantity -= ammount;
            }
        }
        backpackSpaceFilled--;
        OnInventoryChanged?.Invoke();
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
