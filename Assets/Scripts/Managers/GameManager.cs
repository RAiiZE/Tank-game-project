using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public Button returnToMenu;

    public Button returnToMainMenu;

    public List <GameObject> m_Tanks = new List<GameObject>();

    public EnemySpawner spawner;

    // Buttons for powerups
    public Button powerUpFireRate, powerUpHealth, powerUpDamage;


    public GameObject player;

    public GameObject shell;

    private float m_GameTime = 0;
    public float GameTime { get { return m_GameTime; } }

    // The gamestates
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
        //Invoke("ButtonOn", 1);

        // wave system
        spawner.NextWave();

        for (int i = 0; i < m_Tanks.Count; i++)
        {
            m_Tanks[i].SetActive(false);
        }

        // Disable certains texts and buttons on game start
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Press Enter to Start!";

        m_HighScorePanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        returnToMenu.gameObject.SetActive(false);
        returnToMainMenu.gameObject.SetActive(false);

        powerUpDamage.gameObject.SetActive(false);
        powerUpFireRate.gameObject.SetActive(false);
        powerUpHealth.gameObject.SetActive(false);

        activeStats.gameObject.SetActive(false);

        shell.GetComponent<Shell>().m_MaxDamage = 34;
    }
    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Count; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    // Triggers if player is dead
    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Count; i++)
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

        // Switch statement for the differnet gamestates

        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;

                    for (int i = 0; i < m_Tanks.Count; i++)
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
                    if (IsPlayerDead() == true)
                    {
                        m_MessageText.text = "Game Over!";
                        m_NewGameButton.gameObject.SetActive(true);
                        returnToMainMenu.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (spawner.wave == 10) // Triggers if you successfully defeat the boss on wave 10
                        {
                            m_MessageText.text = "Congratulations, You Win!";

                            m_HighScores.AddScore(Mathf.RoundToInt(m_GameTime));
                            m_HighScores.SaveScoresToFile();

                            returnToMenu.gameObject.SetActive(true);
                        }
                        else // Triggers at the end of the wave, letting you select a powerup
                        {
                            powerUpDamage.gameObject.SetActive(true);
                            powerUpFireRate.gameObject.SetActive(true);
                            powerUpHealth.gameObject.SetActive(true);
                            Invoke("ButtonOn", 1);
                        }
                    }
                }
                activeStats.text = "Fire Rate: " + player.GetComponent<PlayerShooting>().fireRate 
                    +
                    " Damage: " + shell.GetComponent<Shell>().m_MaxDamage;
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
            m_GameTime = 0;
            spawner.PlayerDeath();

            m_GameState = GameState.Playing;
            for (int i = 0; i < m_Tanks.Count; i++)
            {
                m_Tanks[i].gameObject.SetActive(true);
            }

            // On Death, reset position and stats back to the base
            player.GetComponent<PlayerShooting>().fireRate = 0.5f;
            shell.GetComponent<Shell>().m_MaxDamage = 34;
            player.GetComponent<Damage>().m_CurrentHealth = 250;
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
            returnToMainMenu.gameObject.SetActive(false);
        }
        spawner.NextWave();

        // Disable some buttons again
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

        for (int i = 0; i < m_Tanks.Count; i++)
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

    // button to load back to main menu
    public void ReturnToMenu(string MainMenu)
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Next 3 functions are for the three powerups that pop up at the end of the waves
    public void PowerUpFireRate()
    {
        player.GetComponent<PlayerShooting>().fireRate -= 0.07f;

        powerUpFireRate.GetComponent<Button>().enabled = false;
        powerUpDamage.GetComponent<Button>().enabled = false;
        powerUpHealth.GetComponent<Button>().enabled = false;
    }

    public void PowerUpDamage()
    {
        shell.GetComponent<Shell>().m_MaxDamage += 10;

        powerUpDamage.GetComponent<Button>().enabled = false;
        powerUpHealth.GetComponent<Button>().enabled = false;
        powerUpFireRate.GetComponent<Button>().enabled = false;
    }

    public void PowerUpHealth()
    {
        player.GetComponent<Damage>().m_CurrentHealth += 75;

        powerUpHealth.GetComponent <Button>().enabled = false;
        powerUpFireRate.GetComponent<Button>().enabled = false;
        powerUpDamage.GetComponent<Button>().enabled = false;
    }

    public void ButtonOn()
    {
        powerUpHealth.GetComponent<Button>().enabled = true;
        powerUpFireRate.GetComponent<Button>().enabled = true;
        powerUpDamage.GetComponent<Button>().enabled = true;
    }

    public void RemoveTank(GameObject Tank)
    {
        if (m_Tanks.Contains(Tank))
        {
            m_Tanks.Remove(Tank);
        }
    }
}
