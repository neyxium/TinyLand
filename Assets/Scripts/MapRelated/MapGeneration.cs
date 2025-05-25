using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum treeType
{
    tree,
    sapling,
    unhealthyTree
}

public class MapGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    int spawningMultiplier = 10;
    int random = 0;
    List<Vector2> spawnedLocation = new List<Vector2>();
    void Start()
    {
        if (GameData.Instance.trees == -1 || GameData.Instance.plantedSaplings == -1)
        {
            GenerateObjects();
        }
        else
        {
            GenerateObjects(GameData.Instance.trees, GameData.Instance.plantedSaplings);
        }
    }

    private void GenerateObjects(int trees = 50, int saplings = -1)
    {
        Debug.Log("Trees: " + trees + ", Saplings: " + saplings);
        GameData.Instance.trees = 0;
        GameData.Instance.plantedSaplings = 0;

        for (int i = 0; i < trees; i++)
        {
            spawnTree(treeType.tree, Vector2.zero, "Tree_" + i);
        }
        if (saplings != -1 || saplings != 0)
        {
            for (int i = 0; i < saplings; i++)
            {
                Vector2 location = getLocation();
                if (location == Vector2.zero)
                {
                    break;
                }
                spawnTree(treeType.sapling, Vector2.zero, "Sapling_" + i);
            }
        }
    }

    public void spawnTree(treeType objectType, Vector2 location, string name)
    {
        if (location == Vector2.zero)
        {
            location = getLocation();
            if (location == Vector2.zero)
            {
                return;
            }
        }
        GameObject breakableObject = Instantiate(objects[(int)objectType], location, Quaternion.identity);
        breakableObject.name = name;
        spawnedLocation.Add(location);
        switch (objectType)
        {
            case treeType.tree:
                GameData.Instance.trees++;
                break;
            case treeType.sapling:
                GameData.Instance.plantedSaplings++;
                break;
            default:
                Debug.LogWarning("Unknown tree type!");
                break;
        }
        GameData.Instance.SaveData();
    }

    public string GetNextSaplingName()
    {
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int maxIndex = -1;
        foreach (var item in objects)
        {
            if (item.name.StartsWith("Sapling_"))
            {
                string number = item.name.Replace("Sapling_", "");
                if (int.TryParse(number, out int index))
                {
                    if (index > maxIndex)
                        maxIndex = index;
                }
            }
        }
        int nextIndex = maxIndex + 1;
        return "Sapling_" + nextIndex;
    }

    public string GetNextTreeName()
    {
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int maxIndex = -1;
        foreach (var item in objects)
        {
            if (item.name.StartsWith("Tree_"))
            {
                string number = item.name.Replace("Tree_", "");
                if (int.TryParse(number, out int index))
                {
                    if (index > maxIndex)
                        maxIndex = index;
                }
            }
        }
        int nextIndex = maxIndex + 1;
        return "Tree_" + nextIndex;
    }

    private Vector2 getLocation()
    {
        Vector2 objectLocation;
        bool isTooClose;
        int safetyBreak = 0;

        do
        {
            safetyBreak++;
            random = Random.Range(0, objects.Count);

            float radius = 15f;
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distanceFromCenter = Random.Range(0f, radius);
            float x = Mathf.Cos(angle) * distanceFromCenter;
            float y = Mathf.Sin(angle) * distanceFromCenter;
            objectLocation = new Vector2(x, y);

            if (safetyBreak > 10000)
            {
                return Vector2.zero;
            }

            isTooClose = false;
            foreach (Vector2 loc in spawnedLocation)
            {
                float distance = Vector2.Distance(objectLocation, loc);
                if (distance < 3f)
                {
                    isTooClose = true;
                    break;
                }
            }

        } while (isTooClose);

        return objectLocation;
    }

}
