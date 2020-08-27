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
            levelButton.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();

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
