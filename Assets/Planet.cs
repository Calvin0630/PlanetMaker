using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
    public float DistanceFromSun;
	// Use this for initialization
	void Start () {
        DistanceFromSun = (gameObject.transform.position - GameObject.FindWithTag("Sun").transform.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
