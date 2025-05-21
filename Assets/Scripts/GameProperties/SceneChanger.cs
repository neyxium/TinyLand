using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Game()
    {
        SceneManager.LoadScene(1);
    }

    public void Forest()
    {
        SceneManager.LoadScene(2);
    }

    public void Mountain()
    {
        SceneManager.LoadScene(3);
    }
}
