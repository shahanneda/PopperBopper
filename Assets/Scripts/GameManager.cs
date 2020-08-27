using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevelNumber;

    public bool levelFinished = false;

    private GameObject levelGui;
    // Start is called before the first frame update
    void Start()
    {
        currentLevelNumber = int.Parse(SceneManager.GetActiveScene().name.Substring(5)); 

        print(Application.CanStreamedLevelBeLoaded("Level2"));
        if (!Application.CanStreamedLevelBeLoaded("Level" + (currentLevelNumber + 1))) {
            print("next level not valid");
            GameObject.Find("NextLevelButton").SetActive(false);
        }

        levelGui = GameObject.FindGameObjectWithTag("LevelGui");
        levelGui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                handleClickOn(touch.position);
            }
        }

        if (Input.GetMouseButton(0))
        {
            handleClickOn(Input.mousePosition);
        }
    }


    void handleClickOn(Vector3 pos)
    { // doing this manually instead of the built in OnClick because of some bugs with the unity systemk
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        worldPos.z = 0;
        //GameObject.FindGameObjectWithTag("Player").transform.position = worldPos;
        RaycastHit2D hit = Physics2D.CircleCast(worldPos, 0.5f, Vector2.up, 0.1f);
        if (hit.collider != null)
        {
            Balloon balloon = hit.collider.GetComponent<Balloon>();
            if (balloon != null)
            { // check if object is actually a balloon
                balloon.BalloonClicked();
            }
        }

    }


    public void MenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevelButtonClicked()
    {
        if (Application.CanStreamedLevelBeLoaded("Level" + (currentLevelNumber + 1)))
        {
            SceneManager.LoadScene("Level" + (currentLevelNumber + 1));
        }

    }

    public void LevelEndReached()
    {
        levelFinished = true;
        levelGui.SetActive(true);
        Balloon.balloonPoppedCounter = 0;
    }
}
