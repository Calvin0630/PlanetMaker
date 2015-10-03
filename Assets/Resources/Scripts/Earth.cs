using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {
    Rigidbody rBody;
    GameObject rocketPrefab;
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        //rBody.velocity = new Vector3(-1, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
