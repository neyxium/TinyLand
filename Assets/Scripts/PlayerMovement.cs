using UnityEngine;

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
    Direction direction = Direction.Front;
    bool isMoving = false;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        Vector2 pos = player.transform.position;
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
        else
        {
            isMoving = false;
        }
        player.transform.position = pos;
        animator.SetBool("isMoving", isMoving);
        animator.SetInteger("direction", (int)direction);

    }
}
