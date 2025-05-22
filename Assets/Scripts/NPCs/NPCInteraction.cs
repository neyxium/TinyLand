using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    bool canInteract = true;
    bool interactable;
    public GameObject item;
    public GameObject chatBubble;

    public void TheAxeGuyDialog()
    {
        if (canInteract)
        {
            canInteract = false;
            Instantiate(chatBubble, new Vector2(-3f, -28.5f), quaternion.identity);
            StartCoroutine(giveAxe());
        }

        IEnumerator giveAxe()
        {
            yield return new WaitForSeconds(3f);
            Instantiate(item, transform.position, quaternion.identity);
        }
    }
    

}
