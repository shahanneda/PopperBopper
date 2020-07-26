using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) { 
            foreach(Touch touch in Input.touches) {
                handleClickOn(touch.position);
            }
        }

        if (Input.GetMouseButton(0)) {
            handleClickOn(Input.mousePosition);
        }
    }


    void handleClickOn(Vector3 pos) { // doing this manually instead of the built in OnClick because of some bugs with the unity systemk
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        worldPos.z = 0;
        //GameObject.FindGameObjectWithTag("Player").transform.position = worldPos;
        RaycastHit2D  hit = Physics2D.CircleCast(worldPos, 0.5f, Vector2.up, 0.1f);
        if(hit.collider != null) {
            Balloon balloon = hit.collider.GetComponent<Balloon>();
            if(balloon != null) { // check if object is actually a balloon
                balloon.BalloonClicked();
            }
        }
    
    }
}
