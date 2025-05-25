using System;
using UnityEngine;

public class SaplingBehaviour : MonoBehaviour
{
    private DateTime plantedTime;
    void Start()
    {
        if (PlayerPrefs.HasKey("Sapling_" + gameObject.name))
        {
            long binaryTime = Convert.ToInt64(PlayerPrefs.GetString("Sapling_" + gameObject.name));
            plantedTime = DateTime.FromBinary(binaryTime);
        }
        else
        {
            plantedTime = DateTime.Now;
            PlayerPrefs.SetString("Sapling_" + gameObject.name, plantedTime.ToBinary().ToString());
        }
    }

    void Update()
    {

    }
}
