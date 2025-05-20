using UnityEngine;

public class AxeBehaviour : MonoBehaviour
{
    public PlayerMovement playerMovement;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree in range: " + other.name);
            playerMovement.SetTreeInRange(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            playerMovement.SetTreeInRange(null);
        }
    }

    // Animation event pokliče to
    public void OnAxeSwingEnd()
    {
        playerMovement.OnAxeSwingEnd();
    }
}
