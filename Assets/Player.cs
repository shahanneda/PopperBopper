using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 targetPos;
    private bool shouldAutoMoveToTarget = false;

    public float levelEndSpeed = 10.0f;
    private Rigidbody2D rb;

    public float shrinkSpeed = 10.0f;
    private bool shouldShrink;

        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(targetPos, transform.position) < 0.1f) {
            shouldAutoMoveToTarget = false;
        }

        if (Vector3.Distance(transform.localScale, Vector3.zero) < 0.01f) {
            shouldShrink = false;
        }

        if (shouldAutoMoveToTarget) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, levelEndSpeed * Time.deltaTime);
        }

        if (shouldShrink) {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed) ;
        }
        
    }

    public void reachedLevelEnd(Vector3 endPos) {
        shouldAutoMoveToTarget = true;
        shouldShrink = true;
        targetPos = endPos;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    
    }
}
