using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
    public float orbitSpeed;
    Rigidbody rBody;
    GameObject[] planets;
    GameObject[] asteroids;
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject obj in planets) {
            obj.GetComponent<Rigidbody>().velocity = orbitSpeed * Get2DNormal(obj.transform.position - gameObject.transform.position).normalized/ ((obj.transform.position - gameObject.transform.position).magnitude);
            print((obj.transform.position - gameObject.transform.position).magnitude);
        }

        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject obj in asteroids) {
            float planetForceMag = (obj.GetComponent<Rigidbody>().mass * rBody.mass) / Mathf.Pow(2, Vector3.Distance(obj.transform.position, gameObject.transform.position));
            Vector3 planetForce = planetForceMag * (gameObject.transform.position - obj.transform.position).normalized;
            obj.GetComponent<Rigidbody>().AddForce(planetForce);
        }

    }
    //returns an angle between 0 and pi/2
    //v1 is sun, v2 is the asteroid
    float GetAngleBetween(Vector3 a, Vector3 b) {
        Vector3 v = b - a;
        return Mathf.Atan(v.x/v.y);
    }

    Vector3 Get2DNormal(Vector3 v) {
        return new Vector3(v.y, -v.x, 0);
    }
}
