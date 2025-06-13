using DesignPattern;

public class GameManager : Singleton<GameManager>
{
    private static int m_score;

    public void Start()
    {
        m_score = 0;
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
