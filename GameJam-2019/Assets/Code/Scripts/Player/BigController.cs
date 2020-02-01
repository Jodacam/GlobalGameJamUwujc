using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigController : PlayerController
{

    [SerializeField] private GameObject itemInFocus;
    [SerializeField] private GameObject holderObject;

    [SerializeField] private Vector2 throwForce;

    private bool isGrabbing;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isGrabbing = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        UpdateHolderObjectPosition();
        if(actionButton.down)
        {
            if(isGrabbing)
            {
                Throw();
            }
            else
            {
                Grab();
            }
        }
    }

    private void Grab()
    {
        if(itemInFocus != null)
        {
            isGrabbing = true;
            itemInFocus.transform.SetParent(holderObject.transform);
            itemInFocus.transform.localPosition = Vector2.zero;
            itemInFocus.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (itemInFocus.CompareTag("Player"))
                itemInFocus.GetComponent<SmallController>().isGrabbed = true;
        }
    }

    private void Throw()
    {
        itemInFocus.transform.SetParent(null);
        itemInFocus.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwForce.x * direction, throwForce.y), ForceMode2D.Impulse);
        itemInFocus.GetComponent<Rigidbody2D>().gravityScale = 1;
        if (itemInFocus.CompareTag("Player"))
            itemInFocus.GetComponent<SmallController>().isGrabbed = false;
        isGrabbing = false;
        itemInFocus = null;
    }

    private void UpdateHolderObjectPosition()
    {
        holderObject.transform.localPosition = new Vector2(holderObject.transform.localPosition.x, holderObject.transform.localPosition.y);
        if (isGrabbing)
            itemInFocus.transform.localPosition = Vector2.zero;
    }

    public void SetItemInFocus(GameObject focusObject)
    {
        itemInFocus = focusObject;
    }
}
