using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private GameManager gameManager;

     void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().reachedLevelEnd(transform.position);
            gameManager.LevelEndReached();
        }
    }
}
