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
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipedTool = "sword";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equipedTool = "pickaxe";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipedTool = "axe";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equipedTool = "bow";
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            equipedTool = "none";
        }

        SelectTool(equipedTool);
    }

    void SelectTool(string equipedTool)
    {
        switch (equipedTool)
        {
            case "sword":
                gameObject.GetComponent<SpriteRenderer>().sprite = sword[0];
                break;
            case "pickaxe":
                gameObject.GetComponent<SpriteRenderer>().sprite = pickaxe[0];
                break;
            case "axe":
                gameObject.GetComponent<SpriteRenderer>().sprite = axe[0];
                break;
            case "bow":
                gameObject.GetComponent<SpriteRenderer>().sprite = bow[0];
                break;
            case "none":
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

    public void OnAxeSwingEnd()
    {
        playerMovement.OnAxeSwingEnd();
    }
}
