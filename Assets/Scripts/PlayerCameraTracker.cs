using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTracker : MonoBehaviour
{

    private Transform player;

    public float followTreshhold = 0.5f;
    public float panSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform; 
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if( distanceFromPlayer > 0.01f) {
            Vector2 newPos = Vector2.Lerp(transform.position, player.position, Time.deltaTime * panSpeed * distanceFromPlayer );
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);


        }
    }
}
