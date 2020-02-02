using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Camera cam;
    private bool following;
    private GameObject smallPlayer;
    private GameObject bigPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponent<Camera>();
        smallPlayer = FindObjectOfType<SmallController>().gameObject;
        bigPlayer = FindObjectOfType<BigController>().gameObject;
        following = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(following)
        {
            float middlePoint = (smallPlayer.transform.position.x + bigPlayer.transform.position.x) / 2;
            float distance = middlePoint - transform.position.x;
            float position = Mathf.Lerp(transform.position.x,middlePoint,Mathf.Min(1-(Mathf.Sqrt(distance)/distance),1));
            transform.position = new Vector3(position, transform.position.y, transform.position.z);
        }
    }
}
