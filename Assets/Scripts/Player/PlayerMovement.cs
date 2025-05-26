using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Direction
{
    Front,
    Right,
    Back,
    Left
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 200f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject tool;

    public Direction direction = Direction.Front;
    private bool isMoving = false;
    private Animator playerAnimator;
    private Animator toolAnimator;
    private SpriteRenderer toolRenderer;
    private GameObject treeInRange = null;
    private GameObject stoneInRange = null;
    [SerializeField] GameObject sapling;
    ToolBehaviour toolBehaviour;
    GameObject mapMenu;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        toolAnimator = tool.GetComponent<Animator>();
        toolRenderer = tool.GetComponent<SpriteRenderer>();
        toolBehaviour = tool.GetComponent<ToolBehaviour>();

        foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.name == "Map" && obj.scene.IsValid())
            {
                mapMenu = obj;
                break;
            }
        }

        if (!GameData.Instance.firstTime)
        {
            GameObject axe = GameObject.Find("Axe");
            if (axe != null)
            {
                Destroy(axe);
            }
        }

        if (GameData.Instance.firstTime)
        {
            playerAnimator.SetBool("firstTime", true);
        }

    }

    void Update()
    {
        if (!GameData.Instance.firstTime)
        {
            HandleInput();
            HandleMapInput();
            HandleToolDirection();
        }
    }

    private void HandleToolDirection()
    {
        switch (toolBehaviour.equipedTool)
        {
            case "Axe":
            case "Sword":
            case "Pickaxe":
                UpdateToolDirection(direction);
                break;
            case "Bow":
                UpdateBowDirection(direction);
                break;
        }
    }

    private void HandleInput()
    {
        Vector2 pos = player.transform.position;
        isMoving = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += MovementSpeed * Time.deltaTime;
            direction = Direction.Back;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= MovementSpeed * Time.deltaTime;
            direction = Direction.Front;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= MovementSpeed * Time.deltaTime;
            direction = Direction.Left;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += MovementSpeed * Time.deltaTime;
            direction = Direction.Right;
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameData.Instance.AddToBackpack("wood", 10);
            GameData.Instance.SaveBackpack();
        }

        if (Input.GetMouseButton(0))
        {
            if (tool.activeSelf && (toolBehaviour.equipedTool == "Axe" || toolBehaviour.equipedTool == "Pickaxe"))
            {
                toolAnimator.enabled = true;
                toolAnimator.SetInteger("Direction", (int)direction);
                toolAnimator.SetBool("Active", true);
            }
        }
        else
        {
            toolAnimator.enabled = false;
            toolAnimator.SetBool("Active", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            float radius = 2f;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

            bool foundTree = false;

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Tree") || hit.CompareTag("Sapling"))
                {
                    foundTree = true;
                    break;
                }
            }

            if (!foundTree && GameData.Instance.HasItem("Sapling"))
            {
                MapGeneration mapGen = GameObject.FindAnyObjectByType<MapGeneration>();
                if (mapGen != null)
                {
                    mapGen.spawnTree(treeType.sapling, gameObject.transform.position, mapGen.GetNextSaplingName());
                    GameData.Instance.TakeAwayItem("Sapling");
                    GameData.Instance.SaveData();
                    GameData.Instance.SaveBackpack();
                }

            }
            else if (foundTree)
            {
                Debug.Log("Too close to another tree!");
            }
        }

        player.transform.position = pos;
        playerAnimator.SetBool("isMoving", isMoving);
        playerAnimator.SetInteger("direction", (int)direction);
    }

    public void HandleMapInput()
    {
        if (Input.GetKeyDown(KeyCode.M) && !mapMenu.activeSelf)
        {
            OpenMap();
        }
        else if ((Input.GetKeyDown(KeyCode.M) && mapMenu.activeSelf) || (Input.GetKeyDown(KeyCode.Escape) && mapMenu.activeSelf))
        {
            CloseMap();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }


    public void OnAxeSwingEnd()
    {
        if (treeInRange != null)
        {
            BreakableEnviroment treeScript = treeInRange.GetComponent<BreakableEnviroment>();
            if (treeScript != null && toolBehaviour.equipedTool == "Axe")
            {
                bool destroyed = treeScript.DamageObject();
            }
        }
        else if (stoneInRange != null && toolBehaviour.equipedTool == "Pickaxe")
        {
            BreakableEnviroment stoneScript = stoneInRange.GetComponent<BreakableEnviroment>();
            if (stoneScript != null)
            {
                bool destroyed = stoneScript.DamageObject();
            }
        }
    }

    private void UpdateToolDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                toolRenderer.flipX = true;
                toolRenderer.flipY = false;
                tool.transform.localPosition = new Vector3(-0.015f, 0, 0);
                tool.transform.localRotation = Quaternion.Euler(0, 0, -30);
                break;

            case Direction.Right:
                toolRenderer.flipX = false;
                toolRenderer.flipY = false;
                tool.transform.localPosition = new Vector3(0.015f, 0, 0);
                tool.transform.localRotation = Quaternion.Euler(0, 0, 30);
                break;

            case Direction.Front:
            case Direction.Back:
                toolRenderer.flipX = false;
                toolRenderer.flipY = false;
                tool.transform.localPosition = new Vector3(0.0f, 0, 0);
                tool.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    private void UpdateBowDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                toolRenderer.flipX = false;
                toolRenderer.flipY = true;
                tool.transform.localPosition = new Vector3(0f, 0, 0);
                tool.transform.localRotation = Quaternion.Euler(0, 0, -45);
                break;

            case Direction.Right:
                toolRenderer.flipX = true;
                toolRenderer.flipY = true;
                tool.transform.localPosition = new Vector3(0.0f, 0, 0);
                tool.transform.localRotation = Quaternion.Euler(0, 0, 45);
                break;

            case Direction.Front:
            case Direction.Back:
                toolRenderer.flipX = false;
                toolRenderer.flipY = false;
                tool.transform.localPosition = new Vector3(0.0f, 0, 0);
                tool.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    public void SetTreeInRange(GameObject tree)
    {
        treeInRange = tree;
    }

    public void SetStoneInRange(GameObject stone)
    {
        stoneInRange = stone;
    }

    public void SetEndFirstTimeAnimaiton()
    {
        GameData.Instance.firstTime = false;
        GameData.Instance.SaveData();
        playerAnimator.SetBool("firstTime", false);
    }

    public void OpenMap()
    {
        mapMenu.SetActive(true);
    }

    public void CloseMap()
    {
        mapMenu.SetActive(false);
    }

}
