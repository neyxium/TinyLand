using System.Collections;
using Unity.VisualScripting;
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
    bool changed = false;

    void Start()
    {
        questProgress = GameData.Instance.questProgress;
        if (questProgress == 1 || changed == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = campFire;
            changed = false;
        }
        else if (questProgress == 2 || changed == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = woodenHouse;
            changed = false;
        }
        else if (questProgress == 3 || changed == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stoneHouse;
            changed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        questProgress = GameData.Instance.questProgress;
        if (collision.gameObject.tag == "Player")
        {
            if (questProgress == 0)
            {
                StartCoroutine(FirstQuestText());
            }
            else if (questProgress == 1)
            {
                StartCoroutine(SecondQuestText());
            }
            else if (questProgress == 2)
            {
                StartCoroutine(ThirdQuestText());
            }
            else if (questProgress == 3)
            {
                StartCoroutine(FourthQuestText());
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
        changed = true;
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
        changed = true;
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
        changed = true;
        yield return new WaitForSeconds(1f);
        bubble10.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(bubble10);
    }




}
