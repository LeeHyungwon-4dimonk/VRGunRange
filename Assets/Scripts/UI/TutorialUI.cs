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
    // ���� ������Ʈ
    [SerializeField] private TMP_Text m_StepButtonTextField;
    [SerializeField] private TMP_Text m_StepNumTextField;

    // �Է� ������Ʈ
    [SerializeField] List<Step> m_StepList = new List<Step>();

    int m_CurrentStepIndex = 0;

    private void Awake() => Init();

    private void Init()
    {
        m_StepNumTextField.text = $"{m_CurrentStepIndex + 1} / {m_StepList.Count}";
    }

    // ��ư�� On Click�� ������ ���
    // ��ư�� ������ Ʃ�丮�� UI�� ���� �������� �Ѿ �� �ְ� �ϸ�,
    // UI�� ������ ���������� �Ѿ�� �ٽ� ù �������� ���ƿ� �� �ְ� ����
    public void Next()
    {
        m_StepList[m_CurrentStepIndex].StepObject.SetActive(false);
        m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
        m_StepList[m_CurrentStepIndex].StepObject.SetActive(true);
        m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].ButtonText;
        m_StepNumTextField.text = $"{m_CurrentStepIndex + 1} / {m_StepList.Count}";
    }
}