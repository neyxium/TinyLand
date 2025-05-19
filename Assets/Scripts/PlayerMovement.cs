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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 pos = gameObject.transform.position;
            pos.y += MovementSpeed * Time.deltaTime;
            gameObject.transform.position = pos;
            direction = Direction.Back;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 pos = gameObject.transform.position;
            pos.y -= MovementSpeed * Time.deltaTime;
            gameObject.transform.position = pos;
            direction = Direction.Front;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 pos = gameObject.transform.position;
            pos.x -= MovementSpeed * Time.deltaTime;
            gameObject.transform.position = pos;
            direction = Direction.Left;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 pos = gameObject.transform.position;
            pos.x += MovementSpeed * Time.deltaTime;
            gameObject.transform.position = pos;
            direction = Direction.Right;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetInteger("direction", (int)direction);

    }
}
