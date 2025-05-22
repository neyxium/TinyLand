using Unity.VisualScripting;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item itemData;
    public int quantity = 1;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = itemData.icon;
    }
}
