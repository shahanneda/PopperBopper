using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleBalloonpoppedPowerup : MonoBehaviour
{
    public static int progress = 0;

    public float balloonMoveSpeed = 10.0f;
    public float distanceToMoveTo = 10.0f;
    public float explosionForce = 10.0f;
    public GameObject explodeParticleSystemPrefab;
    public float growBackSpeed = 100f;

    private bool shouldMoveTowardPlayer;

    private Image[] childBalloonIcons;
    private Vector3 boostDirection;
    private Color color;
    private bool shouldGrowBackToFullSize = false;


    void Start()
    {
        childBalloonIcons = new Image[transform.childCount];
        ResetChildBaloonIcons();
    }


    private void ResetChildBaloonIcons()
    {

        int i = 0;
        foreach (Transform child in transform)
        {
            childBalloonIcons[i] = child.GetComponent<Image>();
            childBalloonIcons[i].color = new Color(0, 0, 0, 0.2f);
            shouldGrowBackToFullSize = true;

            i++;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    void Update()
    {
        if (shouldMoveTowardPlayer)
        {
            MoveTowardsPlayer();
        }
        if (shouldGrowBackToFullSize)
        {

            for (int i = 0; i < childBalloonIcons.Length; i++)
            {
                childBalloonIcons[i].transform.localScale = Vector3.Lerp(childBalloonIcons[i].transform.localScale, Vector3.one, balloonMoveSpeed * Time.deltaTime);
            }
        }
        if (shouldGrowBackToFullSize && Vector3.Distance(childBalloonIcons[0].transform.localScale, Vector3.one) < 0.01f)
        {
            shouldGrowBackToFullSize = false;
        }
    }
    /// <summary>
    /// Called everytime a baloon is popped
    /// </summary>
    /// <returns> returns wheter this pop should be extra strong (if it is the 3rd one)</returns>
    public bool BalloonPopped(Color color, Vector3 direction)
    {
        childBalloonIcons[progress].color = color;
        boostDirection = direction;

        progress++;
        bool returnVal = false;
        if (progress > 2)
        {
            //ResetChildBaloonIcons();
            shouldMoveTowardPlayer = true;
            returnVal = true;
            progress = 0;
        }
        return returnVal;
    }


    public void MoveTowardsPlayer()
    {
        Player player = FindObjectOfType<Player>();
        Vector3 playerWorldPos = player.transform.position;
        Vector2 playerPos = Camera.main.WorldToScreenPoint(playerWorldPos);

        foreach (Image g in childBalloonIcons)
        {
            g.transform.position = Vector3.Lerp(g.transform.position, playerPos, balloonMoveSpeed * Time.deltaTime);
            g.transform.localScale = Vector3.Lerp(g.transform.localScale, Vector3.zero, balloonMoveSpeed * Time.deltaTime);
        }

        // check if done
        if (Vector2.Distance(playerPos, childBalloonIcons[0].transform.position) < distanceToMoveTo && Vector2.Distance(playerPos, childBalloonIcons[1].transform.position) < distanceToMoveTo
            )
        {
            shouldMoveTowardPlayer = false;
            GameObject particle = Instantiate(explodeParticleSystemPrefab, playerWorldPos, Quaternion.identity);
            particle.transform.forward = -boostDirection;
            particle.GetComponent<ParticleSystemRenderer>().material.color = AverageColor(childBalloonIcons);
            player.GetComponent<Rigidbody2D>().AddForce(boostDirection * explosionForce, ForceMode2D.Impulse);


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
