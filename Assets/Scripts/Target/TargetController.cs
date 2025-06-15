using UnityEngine;

/// <summary>
/// ������ �ִϸ��̼� ������
/// </summary>
public class TargetController : MonoBehaviour
{
    private Animator m_animator;

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
            GameManager.Instance.AddScore(100);
            m_animator.SetBool("IsActive", false);
        }
    }
}
