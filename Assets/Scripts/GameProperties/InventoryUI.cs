using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] List<Image> sideInventoryItems;
    //add tmpro text
    [SerializeField] List<TextMeshProUGUI> sideInventoryItemsText;
    [SerializeField] List<Image> bottomInventoryTools;
    void Start()
    {
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        foreach (var img in sideInventoryItems)
            img.enabled = false;

        foreach (var img in bottomInventoryTools)
            img.enabled = false;

        foreach (var txt in sideInventoryItemsText)
        {
            txt.enabled = false;
        }

        int sideIndex = 0;
        int bottomIndex = 0;
        foreach (InventoryItem item in GameData.Instance.backpack)
        {
            Item itemData = GameData.Instance.GetItemByName(item.itemName);
            if (itemData == null) continue;

            if (itemData.isTool)
            {
                bottomInventoryTools[bottomIndex].sprite = itemData.icon;
                bottomInventoryTools[bottomIndex].enabled = true;
                bottomIndex++;
            }
            else
            {
                sideInventoryItems[sideIndex].sprite = itemData.icon;
                sideInventoryItemsText[sideIndex].text = item.quantity.ToString();
                sideInventoryItems[sideIndex].enabled = true;
                sideInventoryItemsText[sideIndex].enabled = true;
                sideIndex++;
            }
        }
    }

    void OnEnable()
    {
        GameData.Instance.OnInventoryChanged += UpdateInventoryUI; // Subscribes to the event and gets called when the inventory changes
    }

    void OnDisable()
    {
        GameData.Instance.OnInventoryChanged -= UpdateInventoryUI; // Unsubscribes from the event
    }

}
