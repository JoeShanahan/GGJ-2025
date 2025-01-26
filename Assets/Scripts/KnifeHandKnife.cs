using UnityEngine;

public class KnifeHandKnife : MonoBehaviour
{
    [Header("Child Objects")]
    public GameObject knife;
    public GameObject hand;

    private bool isDragging = false;

    private void Start()
    {
        if (knife == null || hand == null)
        {
            Debug.LogError("Knife or Hand GameObject reference is missing.");
            return;
        }

        knife.SetActive(true);
        hand.SetActive(false);
    }

    private void OnMouseDown()
    {
        isDragging = true;
        knife.SetActive(false);
        hand.SetActive(true);
        Debug.Log("Dragging started: Knife disabled, Hand enabled.");
    }

    private void OnMouseUp()
    {
        isDragging = false;
        knife.SetActive(true);
        hand.SetActive(false);
        Debug.Log("Dragging ended: Knife enabled, Hand disabled.");
    }
}