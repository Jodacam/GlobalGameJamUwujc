using UnityEngine;

public class Die : MonoBehaviour {
    public Transform point;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.GetComponent<PlayerController>() != null){
                other.transform.position = point.position;
            }
        }
    }
}