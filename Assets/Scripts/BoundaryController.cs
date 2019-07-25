using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D obj) {
        // If the collider registers a hit with a gameObject, destroy it
        string t = obj.gameObject.tag;
        
        if (t == "Satellite" || t == "Asteroid" || t == "Enemy" || t == "Bullet" || t == "Player") {
            Destroy(obj.gameObject);
        }
    }
}
