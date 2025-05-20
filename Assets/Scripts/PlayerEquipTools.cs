using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipTools : MonoBehaviour
{
    [SerializeField] List<GameObject> sword;
    [SerializeField] List<GameObject> pickaxe;
    [SerializeField] List<GameObject> axe;    
    [SerializeField] List<GameObject> bow;
    [SerializeField] string equipedTool;

    void Start()
    {
        
    }

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

        SelectTool();
    }

    private void SelectTool()
    {
        if (equipedTool == "sword")
        {
            sword[0].SetActive(true);
            axe[0].SetActive(false);
            pickaxe[0].SetActive(false);
            bow[0].SetActive(false);
        }
        if (equipedTool == "axe")
        {
            sword[0].SetActive(false);
            axe[0].SetActive(true);
            pickaxe[0].SetActive(false);
            bow[0].SetActive(false);
        }
        if (equipedTool == "pickaxe")
        {
            sword[0].SetActive(false);
            axe[0].SetActive(false);
            pickaxe[0].SetActive(true);
            bow[0].SetActive(false);
        }
        if (equipedTool == "bow")
        {
            sword[0].SetActive(false);
            axe[0].SetActive(false);
            pickaxe[0].SetActive(false);
            bow[0].SetActive(true);
        }
    }
}
