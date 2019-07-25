using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    public GameObject explosion;  // Empty gameObject containing a play-once particle system for the satellite getting hit

    private float animDelay = 0.0f;
    private float myTime = 0.0F;
    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        // Setup some random variables so all the satellites rotate to different angles and different speeds
        animDelay = Random.Range(2.5f, 5.0f);
        float randomTorque = Random.Range(-10.0f, 10.0f);
        float randomStartAngle = Random.Range(-20f, 20f);
        rb2d = GetComponent<Rigidbody2D>(); 
        transform.Rotate(0, 0, randomStartAngle);
        rb2d.angularVelocity = randomTorque;

        // Set animation to random frame to start the satellites
        /* Currently no animation on these
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f)); 
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        // If animDelay has passed, reverse the direction of the satellites rotation
        myTime = myTime + Time.deltaTime;

        if (myTime > animDelay)
        {
            rb2d.angularVelocity = -rb2d.angularVelocity;
            myTime = 0.0F;
        }    
    }

    void OnTriggerEnter2D(Collider2D obj) {
        // Asteroid vs Satellite vs Player
        string t = obj.gameObject.tag;
        
        if (t == "Player") {
            Instantiate(explosion, obj.transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(obj.gameObject);
            GameController.instance.AddToScore("Player");
            GameController.instance.AddToScore("Satellite");
        }
    }
}
