using System;
using System.Collections;
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

    [SerializeField]
    private TextMeshProUGUI m_StepButtonTextField;

    [SerializeField]
    List<Step> m_StepList = new List<Step>();

    int m_CurrentStepIndex = 0;

    public void Next()
    {
        m_StepList[m_CurrentStepIndex].StepObject.SetActive(false);
        m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
        m_StepList[m_CurrentStepIndex].StepObject.SetActive(true);
        m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].ButtonText;
    }
}
