using DesignPattern;

public class GameManager : Singleton<GameManager>
{
    private static int m_score;
    private static bool m_isGameOver = true;

    public void GameStart()
    {
        m_score = 0;
        m_isGameOver = false;
    }

    public void GameOver()
    {
        m_isGameOver = true;
    }

    public bool IsGameOver()
    {
        return m_isGameOver;
    }

    public void AddScore(int score)
    {
        m_score += score;
    }

    public int GetScore()
    { 
        return m_score;
    }
}
