using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
    public float distanceFromSun;
	// Use this for initialization
	void Start () {
        distanceFromSun = (gameObject.transform.position - GameObject.FindWithTag("Sun").transform.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
