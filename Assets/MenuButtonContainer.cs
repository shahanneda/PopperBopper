using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonContainer : MonoBehaviour
{
    private RectTransform rect;
    public RectTransform bottomOfScreen;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // this is to set the bottom of the scroll view correctly so it will scroll properly
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, -bottomOfScreen.sizeDelta.y);
    }
}
