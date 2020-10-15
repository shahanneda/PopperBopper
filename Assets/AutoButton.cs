using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AutoButton : MonoBehaviour
{
    private GameManager gm;
    private Button button;

    public ButtonType type;

    private void Start()
    {

        gm = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Clicked());

    }

    public void Clicked() {
        switch (type) {
            case ButtonType.Pause:
                gm.PauseButtonClicked();
                break;
            case ButtonType.Resume:
                gm.ResumeButtonClicked();
                break;
            case ButtonType.Next:
                gm.NextLevelButtonClicked();
                break;
            case ButtonType.Restart:
                gm.RestartButtonClicked();
                break;
            case ButtonType.Menu:
                gm.MenuButtonClicked();
                break;
            default:
                gm.PauseButtonClicked();
                break;
        }
    }

}
public enum ButtonType {
    Pause,
    Resume,
    Menu,
    Restart,
    Next,
}