using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSimulator : MonoBehaviour
{

    public bool TriggerWind;
    private List<GameObject> balloonsInWind = new List<GameObject>();

    public float normalWindMultiplier = 0.5f;
    public float angularWindMultiplier = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerWind) {
            TriggerWind = false;
            SimulateWind();
        }
        
    }

    void SimulateWind() { 
        foreach (GameObject balloonObject in balloonsInWind) {
            Rigidbody2D rb = balloonObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * normalWindMultiplier, ForceMode2D.Impulse);
            rb.AddTorque(angularWindMultiplier,ForceMode2D.Impulse);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!balloonsInWind.Contains(collision.gameObject)){  // consider removing this for preformance
            balloonsInWind.Add(collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (balloonsInWind.Contains(collision.gameObject)) {
            balloonsInWind.Remove(collision.gameObject);
        }
        
    }
}
