using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Animator m_animator;
    private bool m_isActive = false;

    private void Awake() => Init();

    private void Init()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_animator.SetBool("IsActive", !m_isActive);
    }

    private void Update()
    {

    }
}
