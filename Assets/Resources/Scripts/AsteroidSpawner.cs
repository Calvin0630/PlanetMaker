using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
    //for the GUI
    public bool asteroidSpawning;
    public static Vector3 cameraSize;
    GameObject asteroidPrefab;
    public float asteroidSpawnDelay;
    GameObject FramePrefab;
	// Use this for initialization
	void Start () {
        cameraSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        FramePrefab = (GameObject)Resources.Load("Prefabs/Frame");
        asteroidPrefab = (GameObject) Resources.Load("Prefabs/Asteroid");
        StartCoroutine(SpawnAsteroid());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator SpawnAsteroid() {
        if (asteroidSpawning) {
            Vector3 spawnPos;
            Vector3 initialVelocity;
            //spawns on top or bottom
            int randomInt = Random.Range(0, 1000);
            if ( randomInt % 2 == 0) {
                //spawn on top
                if(randomInt % 4 == 0) {
                    spawnPos = new Vector3(Random.Range(-cameraSize.x, cameraSize.x), 1.1f * cameraSize.y, 0);
                    initialVelocity = Vector3.right * Random.Range(-1, 1);
                }
                //spawn on bottom
                else {
                    spawnPos = new Vector3(Random.Range(-cameraSize.x, cameraSize.x), -1.1f * cameraSize.y, 0);
                    initialVelocity = Vector3.right * Random.Range(-1, 1);
                }
            }
            //spawns on left or right
            else {
                //spawns on left
                if ((randomInt - 1) % 4 == 0) {
                    spawnPos = new Vector3(-1.1f * cameraSize.x, Random.Range(-cameraSize.y, cameraSize.y), 0);
                    initialVelocity = Vector3.up * Random.Range(-1, 1);
                }
                //spawns on right
                else {
                    spawnPos = new Vector3(1.1f * cameraSize.x, Random.Range(-cameraSize.y, cameraSize.y), 0);
                    initialVelocity = Vector3.up * Random.Range(-1, 1);
                }
            }
            GameObject asteroidInstance = (GameObject)Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            asteroidInstance.GetComponent<Rigidbody>().velocity = (-spawnPos.normalized + initialVelocity * 10);
        }
        yield return new WaitForSeconds(asteroidSpawnDelay);
        StartCoroutine(SpawnAsteroid());
    }
    
}
