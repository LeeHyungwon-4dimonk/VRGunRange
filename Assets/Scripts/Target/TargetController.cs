using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ������ �ִϸ��̼� ������
/// </summary>
public class TargetController : MonoBehaviour
{
    private Animator m_animator;

    [SerializeField] Transform m_target;

    private void Awake() => Init();

    private void Init()
    {
        m_animator = GetComponent<Animator>();
    }

    // ���� Ȱ��ȭ
    public void ActiveTarget()
    {
        m_animator.SetBool("IsActive", true);
    }

    // ���� ��Ȱ��ȭ
    public void InactiveTarget()
    {
        m_animator.SetBool("IsActive", false);
    }

    // ���� Ȱ��ȭ ���� üũ
    public bool IsActiveTarget()
    {
        return m_animator.GetBool("IsActive");
    }

    // �Ѿ˰� ���� �浹 ��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && IsActiveTarget()
            && !GameManager.Instance.IsGameOver())
        {
            // ���� �浹 ������ ���� ���� ���� ����
            Vector3 hitPoint = collision.GetContact(0).point;
            Vector3 targetCenter = m_target.position;
            float distance = Vector3.Distance(hitPoint, targetCenter);
            int score = CalculateScore(distance);

            GameManager.Instance.AddScore(score);
            m_animator.SetBool("IsActive", false);
        }
    }

    // ���� �浹 ���� - �߽ɺο��� �Ÿ��� ����� ���� ��������� ������ ����
    private int CalculateScore(float distance)
    {
        if (distance <= 0.1f) return 500;
        else if (distance <= 0.2f) return 400;
        else if (distance <= 0.3f) return 300;
        else if (distance <= 0.4f) return 200;
        else return 100;
    }
}
