using DesignPattern;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    private static int m_score;
    private static int m_highScore;
    private static bool m_isGameOver;

    public UnityAction<int> OnScoreChanged;

    private void Awake() => Init();

    private void Init()
    {
        m_score = 0;
        m_highScore = 0;
        m_isGameOver = true;
    }

    public void GameStart()
    {
        m_score = 0;
        OnScoreChanged?.Invoke(m_score);
        m_isGameOver = false;
    }

    public void GameOver()
    {
        m_isGameOver = true;
        if(m_highScore < m_score)
        {
            m_highScore = m_score;
        }
    }

    public bool IsGameOver()
    {        
        return m_isGameOver;
    }

    public void AddScore(int score)
    {
        m_score += score;
        OnScoreChanged?.Invoke(m_score);
    }

    public int GetScore() => m_score;

    public int GetHighScore() => m_highScore;
}
