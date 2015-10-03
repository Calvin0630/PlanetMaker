using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
    public float gravityStrength;
    public float orbitSpeed;
    Rigidbody rBody;
    GameObject[] planets;
    GameObject[] asteroids;
    float maxVelocity = 30;
    public float idealMaxVelocity;
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
        Vector3 planetForce = Vector3.zero;
        foreach (GameObject obj in asteroids) {
            //float planetForceMag = (obj.GetComponent<Rigidbody>().mass * rBody.mass) / Mathf.Pow(2, Vector3.Distance(obj.transform.position, gameObject.transform.position));
            float sunForceMag = GetGravitationalForce(obj.GetComponent<Rigidbody>().mass, rBody.mass, Vector3.Distance(obj.transform.position, transform.position));
            Vector3 sunForce = sunForceMag * (gameObject.transform.position - obj.transform.position).normalized;
            foreach (GameObject planet in planets) {
                float planetForceMagnitude = GetGravitationalForce(planet.GetComponent<Rigidbody>().mass, obj.GetComponent<Rigidbody>().mass, Vector3.Distance(obj.transform.position, planet.transform.position));
                planetForce += planetForceMagnitude * (planet.transform.position - obj.transform.position).normalized;
            }
            obj.GetComponent<Rigidbody>().AddForce(((sunForce + planetForce)/maxVelocity) * idealMaxVelocity);
            if (obj.GetComponent<Rigidbody>().velocity.magnitude > maxVelocity) maxVelocity = obj.GetComponent<Rigidbody>().velocity.magnitude;
        }
        print(maxVelocity);
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
        return 10 + gravityStrength * (m2 * m1) / (Mathf.Pow(2, distance)- 1);
    }
}
