using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuLevelButtonCreator : MonoBehaviour
{
    public int numberOfLevels = 1;
    public GameObject levelButtonPrefab;


    void Start()
    {
        for (int i = 1; i <= numberOfLevels; i++)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, transform);
            levelButton.GetComponentsInChildren<TextMeshProUGUI>()[0].text = i.ToString();


            if(PlayerPrefs.HasKey("Level" + i) && PlayerPrefs.GetInt("Level"+i) == 1) {
                levelButton.GetComponent<Image>().color = new Color(0.03f,0.39f,0);
            }

            //00.00.00
            // update best level time
            string keyForTime = "Level" + i + "time";
            TextMeshProUGUI timeText = levelButton.GetComponentsInChildren<TextMeshProUGUI>()[1];
            if (PlayerPrefs.HasKey(keyForTime))
            {
                float time = PlayerPrefs.GetFloat(keyForTime);
                string text = LevelTimer.GetTextForTime(time);
                timeText.text = text;
            }
            else { 
                // to avoid spacing issue
                timeText.text = "00:00.00";
                timeText.color = Color.clear;
            }

            if(levelButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text.Length >= 6) {
                timeText.fontSize = 33;
            }

            



            levelButton.GetComponent<Button>().onClick.AddListener( () => {
                 LevelButtonClicked(int.Parse(levelButton.GetComponentInChildren<TextMeshProUGUI>().text)) ;
            } );

        }
    }


    public void LevelButtonClicked(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

}
