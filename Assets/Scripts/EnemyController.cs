using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosion;  // Empty gameObject containing a play-once particle system for the satellite getting hit
    public float maxSpeed = 3.0f;
    public float chaseDelay = 0.25F; // Lower values will make enemies more aggressive in their chase function
    public GameObject projectile;

    private float computedMaxSpeed;
    private float myTime = 0.0F;
    private float nextChase;
    private Rigidbody2D rb2d;
    private static string playerName = "PlayerShip(Clone)";

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        nextChase = chaseDelay;
        computedMaxSpeed = maxSpeed + Random.Range(0.0f, maxSpeed);
        Invoke("Shoot", Random.Range(5.0f, 10.0f));
    }

    private void FixedUpdate() {

        GameObject player = GameObject.Find(playerName);

        // Readjust enemy to chase player after chaseDelay
        myTime = myTime + Time.deltaTime;

        if (player != null) {
            if (myTime > nextChase) {
                nextChase = myTime + chaseDelay;
                
                Vector3 v = player.transform.position - transform.position;
                rb2d.AddForce(v.normalized);

                if (rb2d.velocity.magnitude > computedMaxSpeed) {
                    rb2d.velocity = rb2d.velocity.normalized * computedMaxSpeed;
                }

                nextChase = nextChase - myTime;
                myTime = 0.0F;
            }

            // Look at player
            Vector3 dir = transform.position - player.transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    void OnTriggerEnter2D(Collider2D obj) {
        
        string t = obj.gameObject.tag;
        
        if (t == "Satellite" || t == "Enemy" || t == "Asteroid" || t == "Player") {
            Instantiate(explosion, obj.transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(obj.gameObject);
        }

        if (t == "Satellite") {
            GameController.instance.AddToScore(t);
        }
    }

    void Shoot() {
        GameObject g = Instantiate(projectile, transform.TransformPoint(Vector3.forward * 1.5f), transform.rotation * Quaternion.Euler(-180, 0, 0));
        g.tag = "EnemyBullet";

        Invoke("Shoot", Random.Range(3.0f, 10.0f));
    }
       
}
