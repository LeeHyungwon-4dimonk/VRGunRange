using TMPro;
using UnityEngine;

/// <summary>
/// 스코어를 표시히는 UI에 현재 점수를 반영하는 스크립트
/// </summary>
public class ScoreUI : MonoBehaviour
{
    private TMP_Text m_text;

    private void Awake() => Init();

    private void Init()
    {
        m_text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable() => GameManager.Instance.OnScoreChanged += ScoreUpdate;

    private void OnDisable() => GameManager.Instance.OnScoreChanged -= ScoreUpdate;

    // 점수 변화시 Text에 반영 -> 이벤트로 처리
    private void ScoreUpdate(int score)
    {
        m_text.text = $"{score.ToString("000")}";
    }
}