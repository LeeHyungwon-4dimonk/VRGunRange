using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 자식 오브젝트로 과녁을 둔다.
/// 자식 오브젝트에 있는 과녁을 관리하면서
/// 랜덤하게 과녁의 활성화를 담당하는 컴포넌트
/// </summary>
public class TargetManager : MonoBehaviour
{
    // 시간제한 설정(밸런스)
    [SerializeField] private float m_setTimelimit;
    private float m_timeLimit;

    // 과녁 변화 속도 설정(밸런스)
    [SerializeField] private float m_targetRoutineTime;

    // 참조 컴포넌트
    private TargetController[] m_targetControllers;

    // 변수 저장 및 코루틴
    private List<int> m_targets = new List<int>(5);
    Coroutine m_coroutine;

    // 인스펙터 참조
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
        // 게임이 종료된 상태가 아닐 시
        if(!GameManager.Instance.IsGameOver())
        {
            m_timeLimit -= Time.deltaTime;
            
            m_timeText.text = $"{m_timeLimit.ToString("00.0")}";

            if (m_coroutine == null)
            {
                m_coroutine = StartCoroutine(TargetCoroutine());
            }

            // 시간 오버 시 게임 종료
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

    // 게임 시작 존에 들어왔을 시 게임 시작
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

    // 게임 시작 존에서 벗어났을 시 강제로 게임 종료
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

    // 과녁 랜덤 활성화 코루틴
    private IEnumerator TargetCoroutine()
    {
        WaitForSeconds m_targetCooltime = new WaitForSeconds(m_targetRoutineTime);
        
        // 코루틴 반복 진행
        while (true)
        {
            // 리스트에 저장된 변수 제거
            m_targets.Clear();

            // 현재 활성화된 과녁 전부 비활성화
            InactivateTarget();

            // 과녁의 개수의 반 + 1개만큼 랜덤 숫자를 뽑는다(중복X)
            while (m_targets.Count < m_targetControllers.Length / 2 + 1)
            {
                int num = Random.Range(0, m_targetControllers.Length);
                if (!m_targets.Contains(num))
                {
                    m_targets.Add(num);
                }
            }

            // 해당 숫자의 과녁을 활성화함
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

    // 모든 과녁 비활성화 함수
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