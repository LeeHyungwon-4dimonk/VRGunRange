using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text m_text;
    private int score;

    private void Awake() => Init();

    private void Init()
    {
        m_text = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        score = GameManager.Instance.GetScore();
        m_text.text = $"{score.ToString("000")}";
    }
}
