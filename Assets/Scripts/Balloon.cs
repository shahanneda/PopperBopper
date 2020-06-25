using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

    public GameObject balloonShatter;
    public GameObject balloonExplodeParticleSystem;


    public float explosionForce = 1f;
    public float explosionRadius = 1f;



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

        rb.isKinematic = true;
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



        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.up);

        foreach(RaycastHit2D hit in hits) {
            Balloon balloon = hit.collider.gameObject.GetComponent<Balloon>();
            if(balloon != null) {
                Vector3 direction = balloon.transform.position.normalized - transform.position.normalized;
                Rigidbody2D rb = balloon.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * (explosionForce + Random.Range(-1,1)), ForceMode2D.Impulse);
            }

        }

        //GameObject shatteredBallon = Instantiate(balloonShatter, transform.position, transform.rotation, shatterParent);
        //foreach (Transform piece in shatteredBallon.transform) {
        //    // add an explosion force to it, were not using 2d rb since that doesnt have the explostion force helper
        //    //piece.GetComponent<Rigidbody>().AddForce(Vector3.left * 100, ForceMode.Impulse);
        //    piece.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
        //    piece.GetComponent<SpriteRenderer>().color = spriteRenderer.color;

        //    Destroy(piece.gameObject, 5);// destroy the object in 5 seconds
        //}



    }
}
