using UnityEngine;

public class StopAnimating : MonoBehaviour
{
    public void stopAnimating()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
