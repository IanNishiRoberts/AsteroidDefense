using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private static string playerName = "PlayerShip(Clone)";

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();

       rb2d.AddTorque(5.0f, ForceMode2D.Impulse);
       rb2d.AddForce(new Vector2(0.0f, -1.0f), ForceMode2D.Impulse);

       player = GameObject.Find(playerName);
    }

    void FixedUpdate() {
        if (player != null) {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist <= 2) { // If within 2 units of player, try to go to them
                Vector3 v = player.transform.position - transform.position;
                rb2d.AddForce(v.normalized * (1.0f - dist / 2) * 50);
            }
        } else {
            player = GameObject.Find(playerName);
        }
    }

    void OnTriggerEnter2D(Collider2D obj) {
        string t = obj.gameObject.tag;
        
        if (t == "Player") {
            Destroy(gameObject);
            GameController.instance.AddToScore("Star");
        }
    }

    void OnBecameInvisible() =>
        Destroy(gameObject);
}
