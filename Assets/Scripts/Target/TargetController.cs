using UnityEngine;

/// <summary>
/// 과녁의 애니메이션 조정용
/// 과녁 점수 계산
/// </summary>
public class TargetController : MonoBehaviour
{
    private Animator m_animator;
    private AudioSource m_audioSource;

    [SerializeField] Transform m_target;

    private void Awake() => Init();

    private void Init()
    {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
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
            // 과녁 충돌 지점에 따른 점수 차등 지급
            Vector3 hitPoint = collision.GetContact(0).point;
            Vector3 targetCenter = m_target.position;
            float distance = Vector3.Distance(hitPoint, targetCenter);
            int score = CalculateScore(distance);

            GameManager.Instance.AddScore(score);
            m_animator.SetBool("IsActive", false);
        }
    }

    // 과녁 충돌 지점 - 중심부와의 거리와 가까울 수록 고득점으로 점수를 지급
    private int CalculateScore(float distance)
    {
        if (distance <= 0.15f) { m_audioSource.Play(); return 500; }
        else if (distance <= 0.25f) return 400;
        else if (distance <= 0.35f) return 300;
        else if (distance <= 0.45f) return 200;
        else return 100;
    }
}