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

    public bool autoMoveUp = false;
    




    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform shatterParent;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shatterParent = GameObject.FindGameObjectWithTag("ShatterParent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
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
                print(balloon);
                Vector3 direction = (balloon.transform.position.normalized - transform.position.normalized).normalized;
                float distanceModifer = Mathf.Clamp(
                    (Vector3.Distance(balloon.transform.position, transform.position) * distanceMultiplier)/10,0,1) * explosionForce;

                Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * (explosionForce + Random.Range(-randomExplosionModifier, randomExplosionModifier) - distanceModifer ) , ForceMode2D.Impulse) ;
            }

        }

  



    }
}
