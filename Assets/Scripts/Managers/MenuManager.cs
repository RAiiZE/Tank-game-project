using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject highScorePanel;

    public Button highScore;

    public Button returnToMenu;

    public Text highScoreText;

    public HighScores highScores;

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

    public void ReturnToMainMenu()
    {
        highScorePanel.SetActive(false);
        returnToMenu.gameObject.SetActive(false);
    }
}
