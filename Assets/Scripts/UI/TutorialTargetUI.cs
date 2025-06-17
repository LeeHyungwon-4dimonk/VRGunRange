using TMPro;
using UnityEngine;

/// <summary>
/// Ʃ�丮�� Ÿ�ٿ� UI ���� ��¿�
/// �÷��̾ ������ �ϸ鼭 ���� ������ ǥ���ϱ� ���� �뵵��
/// ������ ���� ������� �ʵ��� ���� ������ ǥ���� �� �ֵ��� ���� ���
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