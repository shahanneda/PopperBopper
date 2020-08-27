using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public float balloonSpawnDelay = 0.5f;

    private GameObject balloon = null;
    private bool triggeredSpawn = false;

    private Color padColor;
    // Start is called before the first frame update
    void Start()
    {
        padColor = new Color(
              Random.Range(0, 1.0f),
              Random.Range(0, 1.0f),
              Random.Range(0, 1.0f));

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = padColor;
        StartCoroutine(balloonSpawnCoroutine());
        triggeredSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (balloon == null && !triggeredSpawn)
        { // triggeredSpawn check since sometimes in the period waiting for the couroutine it would start it 100 times
            triggeredSpawn = true;
            StartCoroutine(balloonSpawnCoroutine());
        }

    }

    IEnumerator balloonSpawnCoroutine()
    {
        yield return new WaitForSeconds(balloonSpawnDelay);
        SpawnBalloon();
    }
    void SpawnBalloon()
    {
        balloon = Instantiate(balloonPrefab, transform.position, Quaternion.identity, transform);
        balloon.transform.localScale = Vector2.zero;
        balloon.GetComponent<Balloon>().shouldGrowToFull = true;
        balloon.GetComponent<SpriteRenderer>().color = padColor;
        triggeredSpawn = false; // since corutine is done we can reset it now;
    }
}
