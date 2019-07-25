using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject explosion;  // Empty gameObject containing a play-once particle system for the asteroid getting hit

    void OnTriggerEnter2D(Collider2D obj) {
        // Asteroid vs Satellite vs Player
        string t = obj.gameObject.tag;
        
        if (t == "Satellite" || t == "Player") {
            Instantiate(explosion, obj.transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(obj.gameObject);
            GameController.instance.AddToScore(t);
        }
    }
        
}
