using System.Collections;
using UnityEngine;

public class RuinQuest : MonoBehaviour
{
    [SerializeField] GameObject bubble0;
    [SerializeField] GameObject bubble1;
    [SerializeField] GameObject bubble2;
    [SerializeField] GameObject bubble3;
    [SerializeField] GameObject bubble4;
    [SerializeField] GameObject bubble5;
    [SerializeField] GameObject bubble6;
    [SerializeField] GameObject bubble7;
    [SerializeField] GameObject bubble8;
    [SerializeField] GameObject bubble9;
    [SerializeField] GameObject bubble10;
    [SerializeField] Sprite campFire;
    [SerializeField] Sprite woodenHouse;
    [SerializeField] Sprite stoneHouse;

    int questProgress;

    void Start()
    {
        int houseProgress = GameData.Instance.houseProgress;

        if (houseProgress == 1)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = campFire;
        }
        else if (houseProgress == 2)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = woodenHouse;
        }
        else if (houseProgress == 3)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = stoneHouse;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        questProgress = GameData.Instance.questProgress;

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (questProgress)
            {
                case 0:
                    StartCoroutine(FirstQuestText());
                    break;

                case 1:
                    if (GameData.Instance.GetItemQuantity("Wood") >= 10)
                    {
                        GameData.Instance.TakeAwayItem("Wood", 10);
                        StartCoroutine(SecondQuestText());
                    }
                    else
                    {
                        Debug.Log("Not enough wood for second quest (needs 10).");
                    }
                    break;

                case 2:
                    if (GameData.Instance.GetItemQuantity("Wood") >= 25)
                    {
                        GameData.Instance.TakeAwayItem("Wood", 25);
                        StartCoroutine(ThirdQuestText());
                    }
                    else
                    {
                        Debug.Log("Not enough wood for third quest (needs 25).");
                    }
                    break;

                case 3:
                    if (GameData.Instance.GetItemQuantity("Wood") >= 50)
                    {
                        GameData.Instance.TakeAwayItem("Wood", 50);
                        StartCoroutine(FourthQuestText());
                    }
                    else
                    {
                        Debug.Log("Not enough wood for final quest (needs 50).");
                    }
                    break;
            }
        }
    }

    IEnumerator FirstQuestText()
    {
        GameData.Instance.questProgress++;
        Destroy(bubble0);
        bubble1.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble1);
        bubble2.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble2);
    }

    IEnumerator SecondQuestText()
    {
        GameData.Instance.questProgress++;
        bubble3.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble3);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().sprite = campFire;
        GameData.Instance.houseProgress++;
        GameData.Instance.SaveData();
        yield return new WaitForSeconds(1f);
        bubble4.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble4);
        bubble5.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble5);
    }

    IEnumerator ThirdQuestText()
    {
        GameData.Instance.questProgress++;
        bubble6.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble6);
        gameObject.GetComponent<SpriteRenderer>().sprite = woodenHouse;
        GameData.Instance.houseProgress++;
        GameData.Instance.SaveData();
        yield return new WaitForSeconds(1f);
        bubble7.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble7);
        bubble8.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble8);
    }

    IEnumerator FourthQuestText()
    {
        GameData.Instance.questProgress++;
        bubble9.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble9);
        gameObject.GetComponent<SpriteRenderer>().sprite = stoneHouse;
        GameData.Instance.houseProgress++;
        GameData.Instance.SaveData();
        yield return new WaitForSeconds(1f);
        bubble10.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble10);
    }
}
