using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakableEnviroment : MonoBehaviour
{
    int enviromentHealth = 10;
    [SerializeField] List<Item> dropItems;
    [SerializeField] GameObject worldItemPrefab;
    public bool DamageObject()
    {
        enviromentHealth -= 1 + GameData.Instance.houseProgress;

        StartCoroutine(FlashWhite());
        if (enviromentHealth <= 0)
        {
            GameData.Instance.trees--;
            GameData.Instance.SaveData();
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

    private IEnumerator FlashWhite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            // set color to FD9595
            sr.color = new Color(1f, 0.58f, 0.58f, 1f);
            yield return new WaitForSeconds(0.05f);
            sr.color = originalColor;
        }
    }


}
