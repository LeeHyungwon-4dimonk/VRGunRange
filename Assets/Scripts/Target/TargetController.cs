using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Animator m_animator;

    private void Awake() => Init();

    private void Init()
    {
        m_animator = GetComponent<Animator>();
    }

    public void ActiveTarget()
    {
        m_animator.SetBool("IsActive", true);
    }

    public void InactiveTarget()
    {
        m_animator.SetBool("IsActive", false);
    }

    public bool IsActiveTarget()
    {
        return m_animator.GetBool("IsActive");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && m_animator.GetBool("IsActive")
            && !GameManager.Instance.IsGameOver())
        {
            GameManager.Instance.AddScore(100);
            m_animator.SetBool("IsActive", false);
        }
    }
}
