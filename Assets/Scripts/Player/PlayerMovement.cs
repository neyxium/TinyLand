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
    [SerializeField] float MovementSpeed = 100f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject tool;

    public Direction direction = Direction.Front;
    private bool isMoving = false;
    private Animator playerAnimator;
    private Animator toolAnimator;
    private SpriteRenderer toolRenderer;
    private GameObject treeInRange = null;
    private GameObject stoneInRange = null;
    ToolBehaviour toolBehaviour;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        toolAnimator = tool.GetComponent<Animator>();
        toolRenderer = tool.GetComponent<SpriteRenderer>();
        toolBehaviour = tool.GetComponent<ToolBehaviour>();
    }

    void Update()
    {
        HandleInput();
        HandleToolDirection();
    }

    private void HandleToolDirection()
    {
        switch (toolBehaviour.equipedTool)
        {
            case "axe":
            case "sword":
            case "pickaxe":
                UpdateToolDirection(direction);
                break;
            case "bow":
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

        if (Input.GetMouseButton(0))
        {
            if (tool.activeSelf && (toolBehaviour.equipedTool == "axe" || toolBehaviour.equipedTool == "pickaxe"))
            {
                toolAnimator.enabled = true;
                toolAnimator.SetInteger("Direction", (int)direction);
                toolAnimator.SetBool("Active", true);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            toolAnimator.enabled = false;
            toolAnimator.SetBool("Active", false);
        }

        player.transform.position = pos;
        playerAnimator.SetBool("isMoving", isMoving);
        playerAnimator.SetInteger("direction", (int)direction);
    }

    public void OnAxeSwingEnd()
    {
        if (treeInRange != null)
        {
            BreakableEnviroment treeScript = treeInRange.GetComponent<BreakableEnviroment>();
            if (treeScript != null && toolBehaviour.equipedTool == "axe")
            {
                bool destroyed = treeScript.DamageObject();
            }
        }
        else if (stoneInRange != null && toolBehaviour.equipedTool == "pickaxe")
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

}
