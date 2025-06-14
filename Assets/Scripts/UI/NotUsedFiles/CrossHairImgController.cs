using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// 사용하지 않는 코드입니다.

// 크로스헤어(중심점) 기능을 구현하고 싶었으나
// 실험해본 결과 VR의 특성상 총의 조준점이 FPS처럼 고정되지 않다는 점과,
// 설령 에임 포인트가 어디에 닿을 지 예측할 수 있는 코드를 작성한다 하더라도
// 과녁과의 거리에 따라 탄착 지점이 달라져 계산이 복잡하며,
// 이를 일일히 계산하여 크로스헤어를 표시하는 건 비효율적이라고 판단하여
// 크로스헤어를 넣는 기능은 빼기로 결정함
// (그리고 사격 시뮬레이터니까 실제 상황처럼 크로스헤어가 없는 것이 더 현실적이라 판단)

public class CrossHairImgController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable m_gunInteractable;
    [SerializeField] private GameObject m_canvas;
    [SerializeField] private GameObject m_image;
    [SerializeField] private Transform m_muzzlePoint;

    void Update()
    {
        if(m_gunInteractable.isSelected)
        {
            m_canvas.SetActive(true);
            m_image.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, m_muzzlePoint.position);
        }
        else
        {
            m_canvas.SetActive(false);
        }
    }
}
