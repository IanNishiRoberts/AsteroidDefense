using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTextController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Graphic g = gameObject.GetComponent<Graphic>();
        g.GetComponent<CanvasRenderer>().SetAlpha(1f);
        g.CrossFadeAlpha(0f, 3.0f, false);

        Invoke("DestroySpawnText", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) {
            float y = Time.deltaTime * 1;
            Vector3 v = transform.localScale; 
            v.x -= 0.001F ;
            v.y -= 0.001F;
         
            transform.Translate(0, -y, 0);
            transform.localScale = v;
        }
    }

    void DestroySpawnText() {
        Destroy(gameObject);
    }
}
