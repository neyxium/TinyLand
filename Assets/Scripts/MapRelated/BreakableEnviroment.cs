using UnityEngine;

public class BreakableEnviroment : MonoBehaviour
{
    int enviromentHealth = 10;
    [SerializeField] GameObject droppedItem;

    public bool DamageObject()
    {
        enviromentHealth--;
        if (enviromentHealth <= 0)
        {
            Instantiate(droppedItem, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
