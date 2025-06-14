using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Animator m_animator;

    private void Awake() => Init();

    private void Init()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_animator.SetBool("IsActive", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && m_animator.GetBool("IsActive"))
        {
            GameManager.Instance.AddScore(100);
            m_animator.SetBool("IsActive", false);
        }
    }
}
