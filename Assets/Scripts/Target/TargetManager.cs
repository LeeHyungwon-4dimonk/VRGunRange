using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private float m_setTimelimit;
    private float m_timeLimit;
    private TargetController[] m_targetControllers;
    private List<int> m_targets = new List<int>(5);
    Coroutine m_coroutine;

    [SerializeField] private GameObject m_timeUI;
    [SerializeField] TMP_Text m_timeText;

    private void Awake() => Init();

    private void Init()
    {
        m_targetControllers = GetComponentsInChildren<TargetController>();
        m_timeUI.SetActive(false);
    }

    private void Update()
    {
        if(!GameManager.Instance.IsGameOver())
        {
            m_timeLimit -= Time.deltaTime;
            
            m_timeText.text = $"{m_timeLimit.ToString("00.0")}";

            if (m_coroutine == null)
            {
                m_coroutine = StartCoroutine(TargetCoroutine());
            }

            if(m_timeLimit <= 0)
            {
                GameManager.Instance.GameOver();
                if(m_coroutine != null)
                {
                    InactivateTarget();
                    StopCoroutine(m_coroutine);
                    m_coroutine = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.GameStart();
            m_timeLimit = m_setTimelimit;
            m_timeUI.SetActive(true);
        }
    }

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
                m_coroutine = null;
            }
        }
    }

    private IEnumerator TargetCoroutine()
    {
        WaitForSeconds m_targetCooltime = new WaitForSeconds(8);
        while (true)
        {
            m_targets.Clear();
            InactivateTarget();
            while (m_targets.Count < m_targetControllers.Length / 2 + 1)
            {
                int num = Random.Range(0, m_targetControllers.Length);
                if (!m_targets.Contains(num))
                {
                    m_targets.Add(num);
                }
            }
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
