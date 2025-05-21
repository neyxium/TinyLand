using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    int spawningMultiplier = 10;
    int random = 0;
    List<Vector2> spawnedLocation = new List<Vector2>();
    void Start()
    {
        GenerateObjects();
    }

    private void GenerateObjects()
    {
        for (int i = 0; i < 5 * spawningMultiplier; i++)
        {
            Vector2 location = getLocation();
            if (location == Vector2.zero)
            {
                break;
            }
            Random.Range(0, objects.Count);
            Instantiate(objects[random], location, Quaternion.identity);
            spawnedLocation.Add(location);
        }
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
