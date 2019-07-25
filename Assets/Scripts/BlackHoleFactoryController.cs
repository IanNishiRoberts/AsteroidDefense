using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleFactoryController : MonoBehaviour
{
    public GameObject blackHole;

    private float xAxisSpawnPoint;
    private float yAxisSpawnPoint;
    private GameObject currentHole;

    void Start()
    {
        Camera mainCamera = Camera.main;
        xAxisSpawnPoint = mainCamera.aspect * mainCamera.orthographicSize;
        yAxisSpawnPoint = mainCamera.orthographicSize - 1;
    }

    void SpawnBlackHole() {
        currentHole = Instantiate(blackHole, new Vector3(Random.Range(-xAxisSpawnPoint, xAxisSpawnPoint), Random.Range(2, yAxisSpawnPoint), 0), Quaternion.identity);
        Invoke("DestroyBlackHole", Random.Range(10.0f, 15.0f));
    }

    void DestroyBlackHole() {
        Destroy(currentHole);
        Invoke("SpawnBlackHole", Random.Range(15.0f, 30.0f));
    }

    public void Activate() {
        Invoke("SpawnBlackHole", Random.Range(15.0f, 30.0f));        
    }
}
