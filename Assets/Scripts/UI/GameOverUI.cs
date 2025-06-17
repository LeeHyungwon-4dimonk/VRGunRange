using TMPro;
using UnityEngine;

/// <summary>
/// 게임이 종료된 후 최종 점수를 볼 수 있도록 표시하는 UI
/// 자신의 현재 점수와 최고 점수를 표기함
/// 게임의 재시작은 플레이 존 밖으로 나갔다가 들어오는 방식으로 하도록 안내
/// </summary>
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