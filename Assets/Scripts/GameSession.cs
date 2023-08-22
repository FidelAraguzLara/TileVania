using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int m_PlayerLives = 3;
    [SerializeField] int m_PlayerScore = 0;

    [SerializeField] TMP_Text m_LivesText;
    [SerializeField] TMP_Text m_ScoreText;

    void Awake()
    {
        int a_NumberOfGameSession = FindObjectsOfType<GameSession>().Length;

        if (a_NumberOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        m_LivesText.text = m_PlayerLives.ToString();
        m_ScoreText.text = m_PlayerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(m_PlayerLives > 1)
        {
            TakePlayerLive();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void IncreasePlayerScore(int a_CoinPoints)
    {
        m_PlayerScore += a_CoinPoints;
        m_ScoreText.text = m_PlayerScore.ToString();
    }

    void TakePlayerLive()
    {
        m_PlayerLives--;
        int a_CurrentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(a_CurrentLevelIndex);
        m_LivesText.text = m_PlayerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
