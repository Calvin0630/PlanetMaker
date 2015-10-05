using UnityEngine;
using System.Collections;

public class Satelite : MonoBehaviour {
    public float orbitSpeed;
    GameObject earth;
    float initialDistance;
    Rigidbody2D rBody;
    GameObject rocketPrefab;
    public float rocketSpeed;
    float shootingFromLeftOrRight = 1;
	// Use this for initialization
	void Start () {
        earth = GameObject.Find("Earth");
        rBody = GetComponent<Rigidbody2D>();
        initialDistance = (gameObject.transform.position - earth.transform.position).magnitude;
        rocketPrefab = (GameObject)Resources.Load("Prefabs/Rocket");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 rocketDirection = mousePosition - transform.position;
        float angle = Vector3.Angle(new Vector3(10, 0, 0), rocketDirection);
        if (rocketDirection.y < 0) {
            angle *= -1;
        }
        gameObject.transform.eulerAngles = new Vector3(0,0,angle-90);


        if (Input.GetMouseButtonDown(0)) {
            print(rocketDirection.normalized * 0.8f * transform.localScale.y + 0.95f * shootingFromLeftOrRight * transform.localScale.x * Get2DNormal(rocketDirection));
            Vector3 spawnLoc = transform.position + rocketDirection.normalized * 0.8f * transform.localScale.y + 0.95f * shootingFromLeftOrRight * transform.localScale.x * Get2DNormal(rocketDirection).normalized;
            shootingFromLeftOrRight *= -1;
            GameObject rocketInstance = (GameObject)Instantiate(rocketPrefab, spawnLoc, Quaternion.identity);
            rocketInstance.transform.Rotate(0,0, angle + 90, Space.Self);
            rocketInstance.GetComponent<Rigidbody2D>().velocity = rocketDirection.normalized * rocketSpeed;
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
