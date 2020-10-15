using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentLevelNumber;

    public bool levelFinished = false;

    private GameObject levelGui;
    private GameObject nextLevelButton;
    private GameObject restartLevelButton;
    private GameObject menuButton;
    private GameObject resumeButton;

    private GameObject pauseButton;

    private TextMeshProUGUI levelFinishedText;
    private SlowColorChanger levelEndTint;
    private SlowColorChanger levelFinishedLogo;

    public bool isPaused = false;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        levelGui = GameObject.FindGameObjectWithTag("LevelGui");

        levelFinishedText = levelGui.transform.Find("LevelFinishedLogo").GetComponentInChildren<TextMeshProUGUI>();
        levelEndTint = levelGui.transform.Find("LevelEndTint").GetComponent<SlowColorChanger>();
        levelFinishedLogo = levelGui.transform.Find("LevelFinishedLogo").GetComponent<SlowColorChanger>();

        currentLevelNumber = int.Parse(SceneManager.GetActiveScene().name.Substring(5));

        menuButton = levelGui.transform.Find("Buttons").Find("MenuButton").gameObject;
        restartLevelButton = levelGui.transform.Find("Buttons").Find("RestartLevelButton").gameObject;
        nextLevelButton = levelGui.transform.Find("Buttons").Find("NextLevelButton").gameObject;
        resumeButton = levelGui.transform.Find("Buttons").Find("ResumeButton").gameObject;
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");



        Invoke("SetupButtons", 1.0f);


        levelFinishedLogo.colorsToChangeTo = new Color[] { Color.yellow, Color.red };

        levelFinishedLogo.colorTransitionSpeed = 1;


        if (!Application.CanStreamedLevelBeLoaded("Level" + (currentLevelNumber + 1))) {
            nextLevelButton.SetActive(false);
        }

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
            //    //SceneManager.LoadScene("Level6");
            SceneManager.LoadScene("Level" + (currentLevelNumber + 1));
        }

    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseButtonClicked() {
        if (isPaused) {
            return;
        }
        pauseButton.SetActive(false);
        levelGui.gameObject.SetActive(true);
        TurnOffAllMenuButtons();

        menuButton.SetActive(true);
        resumeButton.SetActive(true);
        isPaused = true;

        levelEndTint.colorsToChangeTo = new Color[] { new Color(1.0f,0.0f,0.0f,0), new Color(0.8f, 1.0f, 0.5f, 0.5f)};
        levelEndTint.SetFirstColor();
        levelEndTint.DoCompleteTransition();

    }
    public void ResumeButtonClicked() {
        pauseButton.SetActive(true);
        levelGui.gameObject.SetActive(false);
        isPaused = false;
        levelEndTint.DoCompleteTransition();
    }

    private void TurnOffAllMenuButtons() {
        menuButton.SetActive(false);
        resumeButton.SetActive(false);
        nextLevelButton.SetActive(false);
        restartLevelButton.SetActive(false);
    }


    public void LevelEndReached()
    {
        levelFinished = true;
        levelGui.SetActive(true);


        TurnOffAllMenuButtons();
        menuButton.SetActive(true);
        nextLevelButton.SetActive(true);
        pauseButton.SetActive(false);


        levelEndTint.colorsToChangeTo = new Color[] {levelEndTint.spriteRenderer.color, new Color(0,1,0,0.5f)};
        levelEndTint.SetFirstColor();
        levelEndTint.DoCompleteTransition();

        levelFinishedLogo.colorsToChangeTo = new Color[] { Color.yellow, Color.green };
        levelFinishedText.text = "Level\n" + currentLevelNumber;
        levelFinishedLogo.DoCompleteTransition();

        Balloon.balloonPoppedCounter = 0;
        PlayerPrefs.SetInt("Level" + currentLevelNumber, 1);
    }


    public void PlayerDead() {

        if (isDead) { // player shoulnd die twice
            return;
        }
        isDead = true;

        levelFinished = true;
        levelGui.SetActive(true);

        pauseButton.SetActive(false);

        TurnOffAllMenuButtons();
        menuButton.SetActive(true);
        restartLevelButton.SetActive(true);


        levelEndTint.colorsToChangeTo = new Color[] { new Color(1,1,0,1), new Color(1, 0, 0, 0.5f)};
        levelEndTint.SetFirstColor();
        levelEndTint.DoCompleteTransition();

        levelFinishedLogo.colorsToChangeTo = new Color[] { Color.yellow, Color.red };
        levelFinishedText.text = "Level\n" + currentLevelNumber;
        levelFinishedLogo.DoCompleteTransition();

    }

}
