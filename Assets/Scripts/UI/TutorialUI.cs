using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [Serializable]
    class Step
    {
        [SerializeField]
        public GameObject StepObject;

        [SerializeField]
        public string ButtonText;
    }
    // 참조 컴포넌트
    [SerializeField] private TMP_Text m_StepButtonTextField;
    [SerializeField] private TMP_Text m_StepNumTextField;

    // 입력 컴포넌트
    [SerializeField] List<Step> m_StepList = new List<Step>();

    int m_CurrentStepIndex = 0;

    private void Awake() => Init();

    private void Init()
    {
        m_StepNumTextField.text = $"{m_CurrentStepIndex + 1} / {m_StepList.Count}";
    }

    // 버튼에 On Click로 연결할 기능
    // 버튼을 눌러서 튜토리얼 UI를 다음 페이지로 넘어갈 수 있게 하며,
    // UI가 마지막 페이지까지 넘어가도 다시 첫 페이지로 돌아올 수 있게 설정
    public void Next()
    {
        m_StepList[m_CurrentStepIndex].StepObject.SetActive(false);
        m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
        m_StepList[m_CurrentStepIndex].StepObject.SetActive(true);
        m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].ButtonText;
        m_StepNumTextField.text = $"{m_CurrentStepIndex + 1} / {m_StepList.Count}";
    }
}