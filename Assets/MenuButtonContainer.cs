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
        print(" anc " + rect.anchoredPosition);
        print(" size " + rect.sizeDelta);
    }

    // Update is called once per frame
    void Update()
    {
        print(" anc " + rect.anchoredPosition);
        print(" size " + rect.sizeDelta);

        print(" ancb " + bottomOfScreen.anchoredPosition);
        print(" sizeb " + bottomOfScreen.sizeDelta);
        
    }
}
