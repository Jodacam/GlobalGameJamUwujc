using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSwap : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool iAmNumberOne;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void swapSprite()
    {
        if(iAmNumberOne)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
        }
        iAmNumberOne = !iAmNumberOne;
        spriteRenderer.enabled = true;
        StartCoroutine(hideSprite());
    }

    IEnumerator hideSprite()
    {
        yield return new WaitForSeconds(1);
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        /*
        if(GetComponentInParent<Transform>().localScale.x>0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        */

        transform.localScale = new Vector3(Mathf.Sign(transform.parent.transform.localScale.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    }
}
