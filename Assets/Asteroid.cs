using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.childCount > 0) {
            transform.GetChild(0).GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        }
	}
}
