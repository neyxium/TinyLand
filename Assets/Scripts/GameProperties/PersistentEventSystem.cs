using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentEventSystem : MonoBehaviour
{
    private static PersistentEventSystem instance;

    void Awake()
    {
        EventSystem current = GetComponent<EventSystem>();

        if (instance != null && instance != this)
        {
            // Drug EventSystem že obstaja → izbriši ta podvojeni
            Destroy(gameObject);
            return;
        }

        // Postavi to kot edino instanco
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Poišči in odstrani vse druge EventSysteme
        EventSystem[] allSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        foreach (EventSystem es in allSystems)
        {
            if (es != current)
            {
                Debug.Log("Removing duplicate EventSystem: " + es.gameObject.name);
                Destroy(es.gameObject);
            }
        }
    }
}
