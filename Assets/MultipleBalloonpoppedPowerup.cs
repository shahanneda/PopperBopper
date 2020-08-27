using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleBalloonpoppedPowerup : MonoBehaviour
{
    public static int progress = 0;

    public float balloonMoveSpeed = 10.0f;
    public float distanceToMoveTo = 10.0f;
    public GameObject explodeParticleSystemPrefab;

    private bool shouldMoveTowardPlayer;

    private Image[] childBalloonIcons;
    private Vector3 boostDirection;
    private Color color;

    void Start()
    {
        childBalloonIcons = new Image[transform.childCount];
        ResetChildBaloonIcons();
    }


    private void ResetChildBaloonIcons() { 
    
        int i = 0;
        foreach(Transform child in transform) {
            childBalloonIcons[i] = child.GetComponent<Image>();
            childBalloonIcons[i].color = new Color(0, 0, 0, 0.2f);
            i++;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    void Update()
    {
        if (shouldMoveTowardPlayer) {
            MoveTowardsPlayer();
        }
    }
    /// <summary>
    /// Called everytime a baloon is popped
    /// </summary>
    /// <returns> returns wheter this pop should be extra strong (if it is the 3rd one)</returns>
    public bool BalloonPopped(Color color, Vector3 direction) {
        childBalloonIcons[progress].color = color;
        boostDirection = direction;

        progress++;
        bool returnVal = false;
        if (progress > 2) {
            //ResetChildBaloonIcons();
            shouldMoveTowardPlayer = true;
            returnVal = true;
            progress = 0;
        }
        return returnVal;
    }


    public void MoveTowardsPlayer() {
        Vector3 playerWorldPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 playerPos = Camera.main.WorldToScreenPoint(playerWorldPos);
        foreach(Image g in childBalloonIcons) {
            g.transform.position = Vector3.Lerp(g.transform.position,playerPos, balloonMoveSpeed * Time.deltaTime);
        }
        // check if done
        if(Vector2.Distance(playerPos, childBalloonIcons[0].transform.position) < distanceToMoveTo && Vector2.Distance(playerPos, childBalloonIcons[1].transform.position) < distanceToMoveTo
            ) {
            shouldMoveTowardPlayer = false;
            GameObject particle = Instantiate(explodeParticleSystemPrefab, playerWorldPos, Quaternion.identity);
            particle.transform.forward = -boostDirection;
            particle.GetComponent<ParticleSystemRenderer>().material.color = AverageColor(childBalloonIcons);
            Destroy(particle, 2);
            ResetChildBaloonIcons();
        }


    }

    private Color AverageColor(Image[] colors)
    {
        float r = 0;
        float g = 0;
        float b = 0;
        for (int i = 0; i < colors.Length; i++)
        {

            r += colors[i].color.r;

            g += colors[i].color.g;

            b += colors[i].color.b;

        }
        return new Color((r / colors.Length), (g / colors.Length), (b / colors.Length), 1);

    }

}
