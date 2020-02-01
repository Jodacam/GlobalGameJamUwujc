using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderObjectCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BigController controller;
    private GameObject itemInFocus;
    private bool focusing;
    void Start()
    {
        focusing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject != controller.gameObject && collision.CompareTag("Player")) || collision.CompareTag("CanBeGrabbed"))
        {
            itemInFocus = collision.gameObject;
            controller.SetItemInFocus(itemInFocus);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == itemInFocus)
            controller.SetItemInFocus(null);
    }
}
