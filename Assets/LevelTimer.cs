using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class LevelTimer : MonoBehaviour
{


    private float startTime;
    private float pauseTime;
    private TextMeshProUGUI text;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        text = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        UpdateText();
    }

    public void Pause() {
        if (isPaused) {
            return; 
        }

        pauseTime = Time.time;
        isPaused = true;
    
    }
    public void Play() {
        if (!isPaused) {
            return;
        }
        // add the time we were paused to the start time to counteract
        startTime = startTime + Time.time-pauseTime;
        isPaused = false;
    }

    void UpdateText() {
        text.text = GetTextForTime(CalulateTime());
    }

    public static string GetTextForTime(float time) { 
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time).ToString("00.00");
        return time/60 >= 1 ? minutes + ":" + seconds : seconds;
    }

    float CalulateTime() { 
        if (isPaused) {
             return pauseTime - startTime;
        }
        return Time.time - startTime;
    }

    public void SaveToPlayerPrefs(int level) {
        float time = CalulateTime();
        string key = "Level" + level + "time";

        if (PlayerPrefs.HasKey(key)){ 
            float bestTime = PlayerPrefs.GetFloat(key);
            if(time < bestTime) {
                PlayerPrefs.SetFloat(key, time);
            }
        }
        else {
            PlayerPrefs.SetFloat(key, time);
        }
    }
}
