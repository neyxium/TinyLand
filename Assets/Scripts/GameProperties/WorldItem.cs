using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item itemData;
    [HideInInspector] public int quantity;

    void Start()
    {
        if (itemData != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemData.icon;
            quantity = itemData.size;
        }
    }
}
