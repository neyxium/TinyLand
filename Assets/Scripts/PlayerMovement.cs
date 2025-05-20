using UnityEngine;
using UnityEngine.InputSystem;

enum Direction
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
    [SerializeField] GameObject axe;

    private Direction direction = Direction.Front;
    private bool isMoving = false;
    private Animator playerAnimator;
    private Animator axeAnimator;
    private SpriteRenderer axeRenderer;
    private GameObject treeInRange = null;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        axeAnimator = axe.GetComponent<Animator>();
        axeRenderer = axe.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInput();
        UpdateAxeDirection(direction);
    }

    private void HandleInput()
    {
        Vector2 pos = player.transform.position;
        isMoving = false;

        // Premikanje in določanje smeri
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
            if (axe.activeSelf)
            {
                axeAnimator.enabled = true;
                axeAnimator.SetInteger("Direction", (int)direction);
                axeAnimator.SetBool("Active", true);
            }
        }
        else
        {
            axeAnimator.enabled = false;
            axeAnimator.SetBool("Active", false);
        }

        player.transform.position = pos;
        playerAnimator.SetBool("isMoving", isMoving);
        playerAnimator.SetInteger("direction", (int)direction);
    }

    public void OnAxeSwingEnd()
    {
        if (treeInRange != null)
        {
            TreeBehaviour treeScript = treeInRange.GetComponent<TreeBehaviour>();
            if (treeScript != null)
            {
                bool destroyed = treeScript.DamageTree();
                if (destroyed)
                {
                    GameData.Instance.wood++;
                    Debug.Log("Score: " + GameData.Instance.wood);
                }
            }
        }
    }

    private void UpdateAxeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                axeRenderer.flipX = true;
                axe.transform.localPosition = new Vector3(-0.015f, 0, 0);
                axe.transform.localRotation = Quaternion.Euler(0, 0, -30);
                break;

            case Direction.Right:
                axeRenderer.flipX = false;
                axe.transform.localPosition = new Vector3(0.015f, 0, 0);
                axe.transform.localRotation = Quaternion.Euler(0, 0, 30);
                break;

            case Direction.Front:
            case Direction.Back:
                axeRenderer.flipX = false;
                axe.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    public void SetTreeInRange(GameObject tree)
    {
        treeInRange = tree;
    }

}
