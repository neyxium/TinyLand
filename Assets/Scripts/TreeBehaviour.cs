using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    int treeHealth = 10;

    public bool DamageTree()
    {
        treeHealth--;

        if (treeHealth <= 0)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
