using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private Sprite[] sprites;
    private bool stayActive;

    [System.Serializable]
    public class Action : UnityEvent { };
    public Sound pushButton;
    public Sound releaseButton;
    public Action positiveTrigger;
    public Action negativeTrigger;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BigController>() || collision.CompareTag("Block"))
        {
            active = true;
            positiveTrigger.Invoke();
            pushButton.Play(transform);
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BigController>() || collision.CompareTag("Block"))
        {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BigController>() || collision.CompareTag("Block"))
        {
            active = false;
            negativeTrigger.Invoke();
            releaseButton.Play(transform);
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

    public void DebugTestingActive()
    {
        Debug.Log("HOLA");
    }

    public void DebugTestingInactive()
    {
        Debug.Log("ADIOS");
    }


}
