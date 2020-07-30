using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public RectTransform levelsPanel;
    public RectTransform playButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayButtonClicked() {
        levelsPanel.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false); 
    }

    public void BackButtonClicked() { 
        levelsPanel.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true); 
    }
}
