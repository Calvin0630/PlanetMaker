using UnityEngine;
using System.Collections;

public class Satelite : MonoBehaviour {
    public float orbitSpeed;
    GameObject earth;
    float initialDistance;
    Rigidbody rBody;
    GameObject rocketPrefab;
    public float rocketSpeed;
	// Use this for initialization
	void Start () {
        earth = GameObject.Find("Earth");
        rBody = GetComponent<Rigidbody>();
        initialDistance = (gameObject.transform.position - earth.transform.position).magnitude;
        rocketPrefab = (GameObject)Resources.Load("Prefabs/Rocket");
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 rocketDirection = mousePosition - transform.position;
            GameObject rocketInstance = (GameObject)Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            rocketInstance.GetComponent<Rigidbody>().velocity = rocketDirection.normalized * rocketSpeed;
        }

	}

    void FixedUpdate() {
        Vector3 angularVelocity = Get2DNormal(gameObject.transform.position - earth.transform.position).normalized;
        rBody.velocity = Input.GetAxis("Horizontal") * orbitSpeed * angularVelocity;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.position = earth.transform.position + (gameObject.transform.position - earth.transform.position).normalized * initialDistance;
    }

    Vector3 Get2DNormal(Vector3 v) {
        return new Vector3(v.y, -v.x, 0);
    }
}
