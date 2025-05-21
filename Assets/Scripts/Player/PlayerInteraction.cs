using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Poskusi pridobiti WorldItem komponento
        WorldItem worldItem = collision.gameObject.GetComponent<WorldItem>();
        if (worldItem == null) return;

        // Preveri prostor v nahrbtniku
        if (GameData.Instance.backpackMaxSpace >= GameData.Instance.backpackSpaceFilled + worldItem.itemData.size)
        {
            // Dodaj item
            GameData.Instance.AddToBackpack(worldItem.itemData.itemName, worldItem.quantity);
            GameData.Instance.backpackSpaceFilled += worldItem.itemData.size * worldItem.quantity;

            GameData.Instance.SaveData();
            GameData.Instance.SaveBackpack();

            // Izpiši vsebino nahrbtnika
            Debug.Log("Backpack:");
            foreach (var item in GameData.Instance.backpack)
            {
                Debug.Log(item.itemName + ": " + item.quantity);
            }

            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Not enough space in backpack!");
        }
    }
}
