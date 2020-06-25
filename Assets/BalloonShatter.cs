using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonShatter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // A sphere that fully encloses the bounding box.
        Vector3 center = GetComponent<SpriteRenderer>().bounds.center;
        float radius = GetComponent<SpriteRenderer>().bounds.extents.magnitude;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center, radius);
    }
}
