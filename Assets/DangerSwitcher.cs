using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSwitcher : MonoBehaviour
{

    public float SwitchIntervalInSeconds = 2f;
    private float lastSwitchTime;

    public float speedBoostMultiplier = 1.5f;

    private SlowColorChanger colorChanger;
    private bool playerAlreadyDead = false;



    private bool isDanger = false;
    private bool playerIsIn = false;

    void Start()
    {
        lastSwitchTime = Time.time;
        colorChanger = GetComponent<SlowColorChanger>();
        
    }

    void Update()
    {
        if(Time.time > lastSwitchTime + SwitchIntervalInSeconds) {
            lastSwitchTime = Time.time;
            colorChanger.DoCompleteTransition();
            isDanger = !isDanger;

            if(isDanger && playerIsIn) { // player is already in we should manually ill since trigger wont enterok
                KillPlayer(FindObjectOfType<Player>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            playerIsIn = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Player p = collision.gameObject.GetComponent<Player>();
            playerIsIn = true;
            if (isDanger) {
                KillPlayer(p);
            }
            else { 
                p.rb.AddForce(p.rb.velocity * speedBoostMultiplier, ForceMode2D.Impulse);
            }
        }
    }

    private void KillPlayer(Player p ) { 
        print("Player is danger");
        p.Die(transform.position);
        FindObjectOfType<GameManager>().PlayerDead();
    }



}
