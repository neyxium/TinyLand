using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    int treeHealth = 10;
    [SerializeField] GameObject woodItem;

    public bool DamageTree()
    {
        treeHealth--;

        if (treeHealth <= 0)
        {
            GameObject wood = Instantiate(woodItem, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
