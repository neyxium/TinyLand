using UnityEngine;
using System.Collections.Generic;

public class BreakableEnviroment : MonoBehaviour
{
    int enviromentHealth = 10;
    [SerializeField] List<Item> dropItems;
    [SerializeField] GameObject worldItemPrefab;
    public bool DamageObject()
    {
        enviromentHealth--;

        if (enviromentHealth <= 0)
        {
            foreach (Item item in dropItems)
            {
                int roll = Random.Range(0, 101);
                if (roll <= item.dropChance)
                {
                    Vector2 pos = transform.position;
                    pos.y += Random.Range(-1f, 1.01f);
                    pos.x += Random.Range(-1f, 1.01f);
                    GameObject dropped = Instantiate(worldItemPrefab, pos, Quaternion.identity);
                    dropped.GetComponentInChildren<WorldItem>().itemData = item;    
                }
            }

            Destroy(gameObject);
            return true;
        }

        return false;
    }

}
