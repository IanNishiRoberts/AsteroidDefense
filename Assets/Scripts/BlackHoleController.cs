using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public GameObject explosion;  
    
    void Start() {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddTorque(1500);
    }
        
    void OnTriggerEnter2D(Collider2D obj) {
        
        string t = obj.gameObject.tag;
        
        if (t == "Asteroid" || t == "Player" || t == "Enemy") {
            Instantiate(explosion, obj.transform.position, Quaternion.identity);
            Destroy(obj.gameObject);
        }
    }
}
