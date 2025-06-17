using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_currentScore;
    [SerializeField] private TMP_Text m_highScore;

    private void OnEnable()
    {
        m_currentScore.text = $"Your Score : {GameManager.Instance.GetScore().ToString("000")}";
        m_highScore.text = $"High Score : {GameManager.Instance.GetHighScore().ToString("000")}";
    }
}
