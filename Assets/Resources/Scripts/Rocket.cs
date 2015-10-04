using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    public float rocketForce;
	// Use this for initialization
	void Start () {
        rocketForce = 10;
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObject.transform.parent != null) {
            Vector3 forceDir = transform.parent.position - transform.position;
            transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(forceDir.normalized * rocketForce);
        }
	}

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Asteroid") {
            gameObject.transform.SetParent(col.gameObject.transform);
        }
    }
}
