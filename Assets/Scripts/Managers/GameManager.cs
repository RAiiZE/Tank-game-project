using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public HighScores m_HighScores;

    public Text m_MessageText;
    public Text m_TimerText;
    public Text activeStats;

    public GameObject m_HighScorePanel;
    public Text m_HighScoresText;

    public Button m_NewGameButton;
    public Button m_HighScoresButton;

    public GameObject[] m_Tanks;

    public EnemySpawner spawner;

    // Buttons for powerups
    public Button powerUpFireRate, powerUpHealth, powerUpDamage;


    public GameObject player;

    public GameObject shell;

    private float m_GameTime = 0;
    public float GameTime { get { return m_GameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        Gameover
    };

    private GameState m_GameState;

    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        m_GameState = GameState.Start;
    }
    void Start()
    {


        // wave system
        spawner.NextWave();

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }

        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready!";

        m_HighScorePanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);

        powerUpDamage.gameObject.SetActive(false);
        powerUpFireRate.gameObject.SetActive(false);
        powerUpHealth.gameObject.SetActive(false);

        activeStats.gameObject.SetActive(false);

        shell.GetComponent<Shell>().m_MaxDamage = 34;
    }
    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }


    void Update()
    {

        

        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                }
                break;
            case GameState.Playing:
                bool isGameOver = false;
                m_GameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_GameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
                activeStats.gameObject.SetActive(true);
                

                if (OneTankLeft() == true)
                {
                    isGameOver = true;
                    
                }
                else if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }
                if (isGameOver == true)
                {
                    m_GameState = GameState.Gameover;
                    // m_TimerText.gameObject.SetActive(false); "for testing"

                    //m_NewGameButton.gameObject.SetActive(true); "FOR TESTING"
                    //m_HighScoresButton.gameObject.SetActive(true); //"for testing"

                    if (IsPlayerDead() == true)
                    {
                        m_MessageText.text = "Game Over!";
                        m_NewGameButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        //m_MessageText.text = "Winner!!"; "for testing"

                        //save the score
                        //m_HighScores.AddScore(Mathf.RoundToInt(m_GameTime)); "POSSIBLY CHANGE THESE TWO LINES TO AN IF STATEMENT FOR BEATING FINAL WAVE"
                        //m_HighScores.SaveScoresToFile();
                        powerUpDamage.gameObject.SetActive(true);
                        powerUpFireRate.gameObject.SetActive(true);
                        powerUpHealth.gameObject.SetActive(true);

                        

                        //OnNewGame();
                    }
                }
                activeStats.text = "Fire Rate: " + player.GetComponent<PlayerShooting>().fireRate 
                    +
                    " Damage: " + shell.GetComponent<Shell>().m_MaxDamage;
                break;
            case GameState.Gameover:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_GameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);

                    

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void OnNewGame()
    {
        if (IsPlayerDead())
        {
            spawner.PlayerDeath();
            player.GetComponent<PlayerShooting>().fireRate = 0.5f;
            shell.GetComponent<Shell>().m_MaxDamage = 34;
            player.GetComponent<Damage>().m_CurrentHealth = 100;
        }
        spawner.NextWave();

        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.gameObject.SetActive(false);

        powerUpDamage.gameObject.SetActive(false);
        powerUpFireRate.gameObject.SetActive(false);
        powerUpHealth.gameObject.SetActive(false);

        

        //m_GameTime = 0; "For testing"
        m_GameState = GameState.Playing;
        //m_TimerText.gameObject.SetActive(true); "for testing
        m_MessageText.text = "";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(true);
        }
        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0); "for testing"
        
    }

    public void OnHighScores()
    {
        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(true);

        string text = "";
        for (int i = 0; i < m_HighScores.scores.Length; i++)
        {
            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        m_HighScoresText.text = text;
    }

    public void PowerUpFireRate()
    {
        player.GetComponent<PlayerShooting>().fireRate -= 0.1f;
        Debug.Log("Fire Rate Increased!");
    }

    public void PowerUpDamage()
    {
        shell.GetComponent<Shell>().m_MaxDamage += 10;
    }

    public void PowerUpHealth()
    {
        player.GetComponent<Damage>().m_CurrentHealth += 20;
        
        
    }
}
