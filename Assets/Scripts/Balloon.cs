using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

    public GameObject balloonShatter;


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
        GameObject shatteredBallon = Instantiate(balloonShatter, transform.position, transform.rotation, shatterParent);
        foreach (Transform piece in shatteredBallon.transform) {
            // add an explosion force to it, were not using 2d rb since that doesnt have the explostion force helper
            //piece.GetComponent<Rigidbody>().AddForce(Vector3.left * 100, ForceMode.Impulse);
            piece.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            piece.GetComponent<SpriteRenderer>().color = spriteRenderer.color;

            Destroy(piece.gameObject, 5);// destroy the object in 5 seconds
        }



    }
}
