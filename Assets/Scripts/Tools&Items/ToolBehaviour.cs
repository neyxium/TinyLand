using UnityEngine;
using System.Collections.Generic;

public class ToolBehaviour : MonoBehaviour
{
    public PlayerMovement playerMovement;
    [SerializeField] List<Sprite> sword;
    [SerializeField] List<Sprite> pickaxe;
    [SerializeField] List<Sprite> axe;
    [SerializeField] List<Sprite> bow;
    public string equipedTool;

    void Update()
    {
        HandleInput();
        if (string.IsNullOrEmpty(equipedTool))
        {
            equipedTool = GetTool(0); // Prvi tool v inventarju
            SelectTool(equipedTool);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipedTool = GetTool(0);
            Debug.Log(equipedTool);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equipedTool = GetTool(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipedTool = GetTool(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equipedTool = GetTool(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            equipedTool = GetTool(4);
        }

        SelectTool(equipedTool);
    }

    void SelectTool(string equipedTool)
    {
        switch (equipedTool)
        {
            case "Sword":
                gameObject.GetComponent<SpriteRenderer>().sprite = sword[0];
                break;
            case "Pickaxe":
                gameObject.GetComponent<SpriteRenderer>().sprite = pickaxe[0];
                break;
            case "Axe":
                gameObject.GetComponent<SpriteRenderer>().sprite = axe[GameData.Instance.houseProgress];
                break;
            case "Bow":
                gameObject.GetComponent<SpriteRenderer>().sprite = bow[0];
                break;
            case "":
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            playerMovement.SetTreeInRange(other.gameObject);
        }
        else if (other.CompareTag("Stone"))
        {
            Debug.Log("In range");
            playerMovement.SetStoneInRange(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            playerMovement.SetTreeInRange(null);
        }
        else if (other.CompareTag("Stone"))
        {
            Debug.Log("NOT In range");
            playerMovement.SetStoneInRange(null);
        }
    }
    private string GetTool(int targetIndex)
    {
        int index = 0;
        foreach (InventoryItem item in GameData.Instance.backpack)
        {
            Item itemData = GameData.Instance.GetItemByName(item.itemName);
            if (itemData == null) continue;

            if (itemData.isTool)
            {
                if (index == targetIndex)
                {
                    string returnedItem = itemData.itemName;
                    return returnedItem.Split('_')[0];
                }
                index++;
            }
        }
        return "";
    }

    public void OnAxeSwingEnd()
    {
        playerMovement.OnAxeSwingEnd();
    }
}
