using System;
using UnityEngine;

public class SaplingBehaviour : MonoBehaviour
{
    private float growthDuration = 30f;
    private DateTime plantedTime;
    MapGeneration mapGen;
    [SerializeField] GameObject grownTreePrefab;
    void Start()
    {
        mapGen = GameObject.FindAnyObjectByType<MapGeneration>();
        if (PlayerPrefs.HasKey(gameObject.name))
        {
            long binaryTime = Convert.ToInt64(PlayerPrefs.GetString(gameObject.name));
            plantedTime = DateTime.FromBinary(binaryTime);
        }
        else
        {
            plantedTime = DateTime.Now;
            PlayerPrefs.SetString(gameObject.name, plantedTime.ToBinary().ToString());
        }
    }

    void Update()
    {
        TimeSpan elapsed = DateTime.Now - plantedTime;
        if (elapsed.TotalSeconds >= growthDuration)
        {
            GrowIntoTree();
        }
    }
    
    void GrowIntoTree()
    {
        mapGen.spawnTree(treeType.tree, gameObject.transform.position, mapGen.GetNextTreeName());
        GameData.Instance.plantedSaplings--;
        PlayerPrefs.DeleteKey(gameObject.name);
        Destroy(gameObject);
    }
}
