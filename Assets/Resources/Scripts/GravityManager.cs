using UnityEngine;
using System.Collections;

public class GravityManager : MonoBehaviour {
    //should be 1
    public float gravityStrength;
    GameObject[] planets;
    GameObject[] asteroids;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        Vector3 planetForce = Vector3.zero;
        foreach (GameObject obj in asteroids) {
            foreach (GameObject planet in planets) {
                float planetForceMagnitude = GetGravitationalForce(planet.GetComponent<Rigidbody>().mass, obj.GetComponent<Rigidbody>().mass, Vector3.Distance(obj.transform.position, planet.transform.position));
                planetForce += gravityStrength * planetForceMagnitude * (planet.transform.position - obj.transform.position).normalized;
            }
            obj.GetComponent<Rigidbody>().AddForce(planetForce);
        }
    }

    float GetGravitationalForce(float m1, float m2, float distance) {
        return 10 + gravityStrength * (m2 * m1) / (Mathf.Pow(2, distance) - 1);
    }


    Vector3 Get2DNormal(Vector3 v) {
        return new Vector3(v.y, -v.x, 0);
    }
}
