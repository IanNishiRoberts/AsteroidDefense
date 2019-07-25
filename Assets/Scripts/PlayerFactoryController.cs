using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactoryController : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject player;

    public void SpawnPlayerShip(bool announceRespawn) {
        if (player == null) {
            player = Instantiate(playerPrefab, new Vector3(0,-2.0f, 0), Quaternion.identity);
            if (announceRespawn) {
                GameController.instance.AnnounceRespawn();
            }
        }
    }
}
