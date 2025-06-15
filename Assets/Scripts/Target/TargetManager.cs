using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private float m_setTimelimit;
    private float m_timeLimit;
    [SerializeField] private TargetController[] targetControllers;
    [SerializeField] private GameObject m_timeUI;
    [SerializeField] TMP_Text m_timeText;

    private void Awake() => Init();

    private void Init()
    {
        targetControllers = GetComponentsInChildren<TargetController>();
        m_timeUI.SetActive(false);
    }

    private void Update()
    {
        if(!GameManager.Instance.IsGameOver())
        {
            m_timeLimit -= Time.deltaTime;
            
            m_timeText.text = $"{m_timeLimit.ToString("00.0")}";

            if(m_timeLimit <= 0 )
            {
                GameManager.Instance.GameOver();
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
        }
    }
}
