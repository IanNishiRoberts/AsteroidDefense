using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactoryController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;
    public float minSpeed = 1.0f;
    public float maxSpeed = 5.0f;
    public float maxRotation = 5.0f;

    private GameObject[] satellites;
    private float yAxisSpawnPoint;
    private float xAxisSpawnPoint;
    private Sprite[] asteroidSprites;

    // Start is called before the first frame update
    void Start()
    {
        // Load sprites for asteroids
        asteroidSprites = Resources.LoadAll<Sprite>("asteroid");

        // Set asteroid spawn system based on play field dimensions
        Camera mainCamera = Camera.main;
        xAxisSpawnPoint = mainCamera.aspect * mainCamera.orthographicSize;
        yAxisSpawnPoint = mainCamera.orthographicSize + 1;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnAsteroid() {
        // Generate list of satellites every spawn in case any have been destroyed
        satellites = GameObject.FindGameObjectsWithTag("Satellite");
        if (satellites.Length == 0)
            return;

        GameObject g = Instantiate(asteroidPrefab, new Vector3(Random.Range(-xAxisSpawnPoint, xAxisSpawnPoint), yAxisSpawnPoint, 0), Quaternion.identity);
        GameObject targetSatellite = satellites[Random.Range(0, satellites.Length - 1)];
        Rigidbody2D rb2d = g.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = g.GetComponent<SpriteRenderer>();

        sr.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length - 1)];

        rb2d.AddForce((targetSatellite.transform.position - g.transform.position) * Random.Range(minSpeed, maxSpeed), ForceMode2D.Force);
        rb2d.AddTorque(Random.Range(-maxRotation, maxRotation));

        Invoke("SpawnAsteroid", Random.Range(minSpawnTime, maxSpawnTime));
    }

    public void Activate() {
        Invoke("SpawnAsteroid", 0.0f);
    }
}
