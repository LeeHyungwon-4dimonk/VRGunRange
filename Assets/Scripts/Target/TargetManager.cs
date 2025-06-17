using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �ڽ� ������Ʈ�� ������ �д�.
/// �ڽ� ������Ʈ�� �ִ� ������ �����ϸ鼭
/// �����ϰ� ������ Ȱ��ȭ�� ����ϴ� ������Ʈ
/// </summary>
public class TargetManager : MonoBehaviour
{
    // �ð����� ����(�뷱��)
    [SerializeField] private float m_setTimelimit;
    private float m_timeLimit;

    // ���� ��ȭ �ӵ� ����(�뷱��)
    [SerializeField] private float m_targetRoutineTime;

    // ���� ������Ʈ
    private TargetController[] m_targetControllers;

    // ���� ���� �� �ڷ�ƾ
    private List<int> m_targets = new List<int>(5);
    Coroutine m_coroutine;

    // �ν����� ����
    [SerializeField] private GameObject m_timeUI;
    [SerializeField] TMP_Text m_timeText;
    [SerializeField] private GameObject m_gameOverUI;

    private void Awake() => Init();

    private void Init()
    {
        m_targetControllers = GetComponentsInChildren<TargetController>();        
        m_timeUI.SetActive(false);
        m_gameOverUI.SetActive(false);
    }

    private void Update()
    {
        // ������ ����� ���°� �ƴ� ��
        if(!GameManager.Instance.IsGameOver())
        {
            m_timeLimit -= Time.deltaTime;
            
            m_timeText.text = $"{m_timeLimit.ToString("00.0")}";

            if (m_coroutine == null)
            {
                m_coroutine = StartCoroutine(TargetCoroutine());
            }

            // �ð� ���� �� ���� ����
            if(m_timeLimit <= 0)
            {
                GameManager.Instance.GameOver();
                if(m_coroutine != null)
                {
                    InactivateTarget();
                    StopCoroutine(m_coroutine);
                    m_gameOverUI.SetActive(true);
                    m_coroutine = null;
                }
            }
        }
    }

    // ���� ���� ���� ������ �� ���� ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.GameStart();
            m_timeLimit = m_setTimelimit;
            m_gameOverUI.SetActive(false);
            m_timeUI.SetActive(true);
        }
    }

    // ���� ���� ������ ����� �� ������ ���� ����
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            m_timeUI.SetActive(false);
            if (m_coroutine != null)
            {
                InactivateTarget();
                StopCoroutine(m_coroutine);
                m_gameOverUI.SetActive(false);
                m_coroutine = null;
            }
        }
    }

    // ���� ���� Ȱ��ȭ �ڷ�ƾ
    private IEnumerator TargetCoroutine()
    {
        WaitForSeconds m_targetCooltime = new WaitForSeconds(m_targetRoutineTime);
        
        // �ڷ�ƾ �ݺ� ����
        while (true)
        {
            // ����Ʈ�� ����� ���� ����
            m_targets.Clear();

            // ���� Ȱ��ȭ�� ���� ���� ��Ȱ��ȭ
            InactivateTarget();

            // ������ ������ �� + 1����ŭ ���� ���ڸ� �̴´�(�ߺ�X)
            while (m_targets.Count < m_targetControllers.Length / 2 + 1)
            {
                int num = Random.Range(0, m_targetControllers.Length);
                if (!m_targets.Contains(num))
                {
                    m_targets.Add(num);
                }
            }

            // �ش� ������ ������ Ȱ��ȭ��
            foreach (int num in m_targets)
            {
                if (m_targets.Contains(num))
                {
                    m_targetControllers[num].ActiveTarget();
                }
            }
            yield return m_targetCooltime;
        }
    }

    // ��� ���� ��Ȱ��ȭ �Լ�
    private void InactivateTarget()
    {
        for (int i = 0; i < m_targetControllers.Length; i++)
        {
            if (m_targetControllers[i].IsActiveTarget())
            {
                m_targetControllers[i].InactiveTarget();
            }
        }
    }
}