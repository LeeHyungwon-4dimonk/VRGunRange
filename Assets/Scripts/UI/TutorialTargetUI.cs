using TMPro;
using UnityEngine;

/// <summary>
/// 튜토리얼 타겟용 UI 점수 출력용
/// 플레이어가 연습을 하면서 맞힌 점수를 표기하기 위한 용도로
/// 점수가 따로 저장되지 않도록 맞힌 점수만 표기할 수 있도록 만든 기능
/// </summary>
public class TutorialTargetUI : MonoBehaviour
{
    private TargetController m_targetController;
    [SerializeField] Transform m_target;
    [SerializeField] TMP_Text m_scoreText;

    private void Awake() => Init();

    private void Init()
    {
        m_targetController = GetComponent<TargetController>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitPoint = collision.GetContact(0).point;
        Vector3 targetCenter = m_target.position;
        float distance = Vector3.Distance(hitPoint, targetCenter);
        int score = m_targetController.CalculateScore(distance);
        m_scoreText.text = score.ToString();
    }
}