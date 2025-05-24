using Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int size;
    public bool isTool;
    [Header("Drop Chance")]
    [Range(0, 100)] public int dropChance = 100;
}
