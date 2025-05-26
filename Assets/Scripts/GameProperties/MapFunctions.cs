using UnityEngine;
using UnityEngine.SceneManagement;

public class MapFunctions : MonoBehaviour
{
    public void LoadForest()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void LoadHome()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
