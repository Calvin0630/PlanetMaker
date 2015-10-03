using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {
    Rigidbody rBody;
    GameObject rocketPrefab;
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        rocketPrefab = (GameObject)Resources.Load("Prefabs/Rocket");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && false) {

        }
	
	}
}
