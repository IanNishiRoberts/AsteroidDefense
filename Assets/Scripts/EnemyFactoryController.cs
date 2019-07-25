using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryController : MonoBehaviour
{
    
    public GameObject enemyPrefab;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;
    
    private float yAxisSpawnPoint;
    private float xAxisSpawnPoint;
    private GameObject player;
    private static string playerName = "PlayerShip(Clone)";
    private Sprite[] shipSprites;
    private GameObject[] satellites;
    private bool spawnIncreased = false;
    void Start()
    {
        // Load sprites for ships
        shipSprites = Resources.LoadAll<Sprite>("enemyships");

        // Set enemy spawn system based on play field dimensions
        Camera mainCamera = Camera.main;
        xAxisSpawnPoint = mainCamera.aspect * mainCamera.orthographicSize;
        yAxisSpawnPoint = mainCamera.orthographicSize + 1;

        FindPlayer();
    }

    void SpawnEnemy() {
        
        // If no satellites remain, increase spawn time
        if (!spawnIncreased) {
            satellites = GameObject.FindGameObjectsWithTag("Satellite");
            if (satellites.Length == 0) {
                minSpawnTime = minSpawnTime / 2;
                maxSpawnTime = maxSpawnTime / 2;
                spawnIncreased = true;
            }
        }

        if (player != null) {
            GameObject g = Instantiate(enemyPrefab, new Vector3(Random.Range(-xAxisSpawnPoint, xAxisSpawnPoint), yAxisSpawnPoint, 0), Quaternion.identity);
            SpriteRenderer sr = g.GetComponent<SpriteRenderer>();

            sr.sprite = shipSprites[Random.Range(0, shipSprites.Length - 1)];
        } else {
            // Player is on a delayed spawn, check again
            FindPlayer();
        }

        Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void FindPlayer() {
        player = GameObject.Find(playerName);
    }

    public void Activate() {
        Invoke("SpawnEnemy", 5.0f);
    }
}
