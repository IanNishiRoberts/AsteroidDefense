using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 4.0f;
    public GameObject explosion;  // Empty gameObject containing a play-once particle system for something getting hit
    public GameObject starSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D obj) {
        // If the collider registers a hit with a gameObject, instantiate a new explosion and destroy both objects
        string t = obj.gameObject.tag;
        
        if (t == "Satellite" || t == "Asteroid" || (t == "Enemy" && this.tag != "EnemyBullet")) {
            Instantiate(explosion, obj.transform.position, Quaternion.identity);
            if (Random.Range(0, 100) > 50) {
                Instantiate(starSpawn, obj.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            Destroy(obj.gameObject);

            GameController.instance.AddToScore(t);
        }

        if (this.tag == "EnemyBullet" && t == "Player") {
            Instantiate(explosion, obj.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(obj.gameObject);
        }

        // Two bullets collide, no need for explosion
        if (t == "Bullet") {
            Destroy(gameObject);
            Destroy(obj.gameObject);
        }
    }

    private void OnBecameInvisible() =>
        // Destroy the bullet 
        Destroy(gameObject);
}
