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
        currentLevelNumber = int.Parse(SceneManager.GetActiveScene().name.Substring(5)); // level3

        print(SceneManager.GetSceneByName("Level2").IsValid());
        if (!SceneManager.GetSceneByName("Level" + (currentLevelNumber + 1)).IsValid()) {
            GameObject.Find("NextLevelButton").SetActive(false);
        }

        levelGui = FindObjectOfType<Canvas>().gameObject;
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
        if (SceneManager.GetSceneByName("Level" + (currentLevelNumber + 1)).IsValid())
        {
            SceneManager.LoadScene("Level" + (currentLevelNumber + 1));
        }

    }

    public void LevelEndReached()
    {
        levelFinished = true;
        levelGui.SetActive(true);
    }
}
