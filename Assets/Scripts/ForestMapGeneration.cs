using System.Collections.Generic;
using UnityEngine;

public class ForestMapGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> trees;
    float maxHeight = 0;
    float minHeight = -30;
    float maxLeft = -15;
    float maxRight = 15;
    int spawningMultiplier = 10;
    int randomTree = 0;
    int randomX = 0;
    int randomY = 0;
    List<Vector2> spawnedLocation = new List<Vector2>();
    void Start()
    {
        GenerateTrees();
    }

    private void GenerateTrees()
    {
        for (int i = 0; i < 5 * spawningMultiplier; i++)
        {
            Vector2 treeLocation = getTreeLocation();
            if (treeLocation == Vector2.zero)
            {
                break;
            }
            Instantiate(trees[randomTree], treeLocation, Quaternion.identity);
            spawnedLocation.Add(treeLocation);
        }
    }

    private Vector2 getTreeLocation()
    {
        Vector2 treeLocation;
        bool isTooClose;
        int safetyBreak = 0;

        do
        {
            safetyBreak++;
            randomTree = Random.Range(0, trees.Count);

            float radius = 15f;
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distanceFromCenter = Random.Range(0f, radius);
            float x = Mathf.Cos(angle) * distanceFromCenter;
            float y = Mathf.Sin(angle) * distanceFromCenter;
            treeLocation = new Vector2(x, y);

            if (safetyBreak > 10000)
            {
                return Vector2.zero;
            }

            isTooClose = false;
            foreach (Vector2 loc in spawnedLocation)
            {
                float distance = Vector2.Distance(treeLocation, loc);
                if (distance < 3f)
                {
                    isTooClose = true;
                    break;
                }
            }

        } while (isTooClose);

        return treeLocation;
    }

}
