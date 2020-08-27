using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLocationArrow : MonoBehaviour
{

    private Transform playerTransform;
    private Transform levelEndTransform;

    void Start()
    {
        levelEndTransform = GameObject.FindObjectOfType<LevelEnd>().transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 levelEndLocation = Camera.main.WorldToScreenPoint(levelEndTransform.position);
        //transform.LookAt(levelEndLocation);
        transform.right = levelEndLocation - transform.position;
        //Vector3 direction = (playerTransform.transform.position.normalized - levelEndTransform.transform.position.normalized).normalized;
        //transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, direction);

        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);


    }
}
