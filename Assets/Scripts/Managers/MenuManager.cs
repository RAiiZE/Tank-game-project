using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject highScorePanel;

    public Button highScore;

    public Button smallHighScore;

    public Button returnToMenu;

    public Text highScoreText;

    public HighScores highScores;

    public SmallHighScores smallHighScores;

    public Button rainbowButton;

    public static bool rainbowOn;

    public Text toggle;

    private void Start()
    {
        highScorePanel.SetActive(false);
        returnToMenu.gameObject.SetActive(false);
    }

    public void ViewHighScore()
    {
        highScorePanel.gameObject.SetActive(true);
        returnToMenu.gameObject.SetActive(true);

        string text = "";
        for (int i = 0; i < highScores.scores.Length; i++)
        {
            int seconds = highScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        highScoreText.text = text;
    }

    public void ViewSmallHighScore()
    {
        highScorePanel.gameObject.SetActive(true);
        returnToMenu.gameObject.SetActive(true);

        string text = "";
        for (int i = 0; i < smallHighScores.scores.Length; i++)
        {
            int seconds = smallHighScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        highScoreText.text = text;
    }
    public void ReturnToMainMenu()
    {
        highScorePanel.SetActive(false);
        returnToMenu.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (rainbowOn == true)
        {
            toggle.text = "Rainbow Light: On";
        }
        else
        {
            toggle.text = "Rainbow Light: Off";
        }
    }
    public void RainbowToggle()
    {
        if (rainbowOn == false)
        {
            rainbowOn = true;
            Debug.Log("rainbows on");
        }
        else
        {
            rainbowOn = false;
            Debug.Log("rainbows off");
        }
    }
}
