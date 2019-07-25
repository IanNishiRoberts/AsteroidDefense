using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    // Movement related variables
    public float maxGravDist = 10.0f;
    public float maxGravity = 15.0f;
    public float accelerationForce = 10f;
    public float rotationForce = 1f;
    public float maximumVelocity = 5.0f;

    // Shooting realted variables    
    public GameObject projectile;
    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    // Gravity related variables
    private GameObject[] planets;
    private Renderer[] renderers;
    private Rigidbody2D rb2d; 

    // Start is called before the first frame update
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it
        rb2d = GetComponent<Rigidbody2D>();

        // Find all gameObjects tagged as "Planet"
        planets = GameObject.FindGameObjectsWithTag("Planet");

        // Find all this objects renderers
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fire control, limit to 1 new bullet every 0.5f
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            
            Instantiate(projectile, transform.TransformPoint(Vector3.forward * 1.5f), transform.rotation);

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }

    // Do physics here
    void FixedUpdate() {
        // GetAxis returns values in the range -1 to 1, so it's easy to apply force simply by multiplying values
        float rotation = Input.GetAxis("Horizontal");
        float acceleration = Input.GetAxis("Vertical");

        // ForceMode2D.Force applies force over time, .Impulse applies force instantly
        // Only use forward acceleration, in order to stop down input being used to brake
        if (acceleration == 1) {
            rb2d.AddForce(transform.up * acceleration * accelerationForce, ForceMode2D.Force);
            if (rb2d.velocity.magnitude > maximumVelocity) {
                rb2d.velocity = rb2d.velocity.normalized * maximumVelocity;
            }
        }
        
        // Move rotation of rb2d manually, rather than relying on AddTorque, to stop spinning wildly        
        rb2d.MoveRotation(rb2d.rotation + ((-rotation * rotationForce) * Time.fixedDeltaTime));

        // Apply simple gravity towards any planet gameObjects
        /*
        if (planets != null) {
            foreach(GameObject planet in planets) {
                float dist = Vector3.Distance(planet.transform.position, transform.position);
                if (dist <= maxGravDist) {
                    Vector3 v = planet.transform.position - transform.position;
                    rb2d.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
                }
            }
        }
        */
    }
}
