using UnityEngine;

/// <summary>
/// 과녁의 애니메이션 조정용
/// </summary>
public class TargetController : MonoBehaviour
{
    private Animator m_animator;

    private void Awake() => Init();

    private void Init()
    {
        m_animator = GetComponent<Animator>();
    }

    // 과녁 활성화
    public void ActiveTarget()
    {
        m_animator.SetBool("IsActive", true);
    }

    // 과녁 비활성화
    public void InactiveTarget()
    {
        m_animator.SetBool("IsActive", false);
    }

    // 과녁 활성화 여부 체크
    public bool IsActiveTarget()
    {
        return m_animator.GetBool("IsActive");
    }

    // 총알과 과녁 충돌 시
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
