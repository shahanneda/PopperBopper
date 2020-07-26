using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;

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
        SpawnBalloon();
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
        yield return new WaitForSeconds(1);
        SpawnBalloon();
    }
    void SpawnBalloon()
    {
        balloon = Instantiate(balloonPrefab, transform.position, Quaternion.identity, transform);
        balloon.GetComponent<SpriteRenderer>().color = padColor;
        triggeredSpawn = false; // since corutine is done we can reset it now;
    }
}
