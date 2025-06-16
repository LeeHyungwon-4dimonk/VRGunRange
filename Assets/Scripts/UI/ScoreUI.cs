using TMPro;
using UnityEngine;

/// <summary>
/// ���ھ ǥ������ UI�� ���� ������ �ݿ��ϴ� ��ũ��Ʈ
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

    // ���� ��ȭ�� Text�� �ݿ� -> �̺�Ʈ�� ó��
    private void ScoreUpdate(int score)
    {
        m_text.text = $"{score.ToString("000")}";
    }
}