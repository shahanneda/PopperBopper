using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    private float lastSpawnTime = 0;
    public float spawnInterval = 2f;

    public GameObject balloonPrefab;

    void Start()
    {

    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval) {
            SpawnNewBalloon();
            lastSpawnTime = Time.time;
        }

    }


    private void SpawnNewBalloon() {
        Vector2 position = new Vector2(Random.RandomRange(-7, 7), transform.position.y);
        GameObject balloon = Instantiate(balloonPrefab, position, Quaternion.identity, transform);
        balloon.GetComponent<SpriteRenderer>().color = new Color(
            Random.Range(0, 1.0f), 
            Random.Range(0, 1.0f), 
            Random.Range(0, 1.0f));


    
    }

}
