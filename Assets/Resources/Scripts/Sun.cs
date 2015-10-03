using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
    public float gravityStrength;
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
        Vector3 sunToPlanet;
        Vector3 velocity;
        foreach (GameObject obj in planets) {
            //gets the new velocity
            sunToPlanet = obj.transform.position - gameObject.transform.position;
            velocity = orbitSpeed * Get2DNormal(sunToPlanet).normalized / ((sunToPlanet).magnitude);
            obj.GetComponent<Rigidbody>().velocity = velocity;
            //obj.GetComponent<Planet>().prevVelocity = velocity;
            obj.transform.position = gameObject.transform.position + sunToPlanet.normalized * obj.GetComponent<Planet>().distanceFromSun;
        }

        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject obj in asteroids) {
            //float planetForceMag = (obj.GetComponent<Rigidbody>().mass * rBody.mass) / Mathf.Pow(2, Vector3.Distance(obj.transform.position, gameObject.transform.position));
            float planetForceMag = GetGravitationalForce(obj.GetComponent<Rigidbody>().mass, rBody.mass, Vector3.Distance(obj.transform.position, transform.position));
            Vector3 planetForce = planetForceMag * (gameObject.transform.position - obj.transform.position).normalized;
            obj.GetComponent<Rigidbody>().AddForce(planetForce);
        }

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Asteroid") {
            Destroy(col.gameObject);
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

    float GetGravitationalForce(float m1, float m2, float distance) {
        return (m2 * m1) / Mathf.Pow(2, distance);
    }
}
