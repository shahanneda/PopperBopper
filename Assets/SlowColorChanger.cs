using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowColorChanger : MonoBehaviour
{
    public Color[] colorsToChangeTo;
    public float colorTransitionSpeed = 10.0f;

    private int nextColorIndex = 0; // the next color we are going to 
    private int finalColorIndex = 0; // the last colors we will stop at

    public bool DoTransition = false;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!spriteRenderer.color.IsCloseTo(colorsToChangeTo[nextColorIndex], 0.01f)){
            TickTransition();
        }
        else if (nextColorIndex != finalColorIndex) {
            nextColorIndex = finalColorIndex > nextColorIndex ? nextColorIndex + 1 : nextColorIndex - 1;
        }

        if (DoTransition) { // just as a button for the inspector
            DoTransition = false;
            DoCompleteTransition();
        }
    }

    private void TickTransition() {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, colorsToChangeTo[nextColorIndex], colorTransitionSpeed * Time.deltaTime);
    }

    public void DoCompleteTransition() {
        finalColorIndex = finalColorIndex == colorsToChangeTo.Length - 1 ? 0 : colorsToChangeTo.Length - 1;
    }
}

static class Extension
{
    /// <summary>
    /// For comparing to anothher color, with a tolerance
    /// </summary>
    /// <param name="other">The other color</param>
    /// <param name="tolerance">How close it needs to be</param>
    /// <returns>wheter is within tolerence or not</returns>
    public static bool IsCloseTo(this Color me, Color other, float tolerance)
    {
        return me.r - other.r  < tolerance && me.g - other.g < tolerance && me.b - other.b < tolerance&& me.a - other.a < tolerance;
    }
}