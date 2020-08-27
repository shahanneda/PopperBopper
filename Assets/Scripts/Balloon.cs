using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

    public GameObject balloonShatter;
    public GameObject balloonExplodeParticleSystem;


    public float explosionForce = 1f;
    public float explosionRadius = 1f;
    public float distanceMultiplier = 1f;
    public float randomExplosionModifier = 1f;
    public float moveSpeed = 1f;
    public float growSpeed = 10.0f;

    public bool autoMoveUp = false;
    public bool shouldGrowToFull = false;
    




    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform shatterParent;

    private MultipleBalloonpoppedPowerup balloonpoppedPowerup;

    public static int balloonPoppedCounter = 0;
    public float explosionIncreaseMultiplier = 1.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shatterParent = GameObject.FindGameObjectWithTag("ShatterParent").transform;
        balloonpoppedPowerup = FindObjectOfType<MultipleBalloonpoppedPowerup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldGrowToFull && Vector3.Distance(Vector3.one, transform.localScale) < 0.01f) {
            shouldGrowToFull = false;
        }
        if (shouldGrowToFull) {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * growSpeed);
        }
        
    }

    private void OnMouseDown()
    {
    }
    private void FixedUpdate()
    {
        if (autoMoveUp) { 
          rb.MovePosition(new Vector2( transform.position.x, transform.position.y + moveSpeed * Time.deltaTime));
        }
    }

    public void BalloonClicked() { 
        //rb.isKinematic = true;
        GetComponent<Collider2D>().isTrigger = true;
        ExplodeBalloon();
        Destroy(gameObject);
    }


    private void ExplodeBalloon() {
        ParticleSystem particleSystem = Instantiate(balloonExplodeParticleSystem, transform.position, transform.rotation, shatterParent).GetComponent<ParticleSystem>();


        ParticleSystem.MainModule main = particleSystem.main;
        main.startSpeed = 100;

        ParticleSystemRenderer particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        particleSystemRenderer.material.color = spriteRenderer.color;

        //main.startColor = spriteRenderer.color;
        particleSystem.Play();
        Destroy(particleSystem.gameObject, 2);


        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.down,1f);

        foreach(RaycastHit2D hit in hits) {
            GameObject balloon = hit.collider.gameObject;
            
            if(balloon != null && balloon.gameObject.tag == "Player") {

                Vector3 direction = (balloon.transform.position - transform.position).normalized;
                float distanceModifer = Mathf.Clamp(
                    (Vector3.Distance(balloon.transform.position, transform.position) * distanceMultiplier)/10,0,1) * explosionForce;

                bool shouldBeStronger = balloonpoppedPowerup.BalloonPopped(spriteRenderer.color, direction);
                Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * (explosionForce  
                    
                     + Random.Range(-randomExplosionModifier, randomExplosionModifier) - distanceModifer), ForceMode2D.Impulse);
                //rb.AddForce(direction * 10, ForceMode2D.Impulse);
            }



        }
        //print(balloonPoppedCounter);



    }
}
