using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// ������� �ʴ� �ڵ��Դϴ�.

// ũ�ν����(�߽���) ����� �����ϰ� �;�����
// �����غ� ��� VR�� Ư���� ���� �������� FPSó�� �������� �ʴٴ� ����,
// ���� ���� ����Ʈ�� ��� ���� �� ������ �� �ִ� �ڵ带 �ۼ��Ѵ� �ϴ���
// ������� �Ÿ��� ���� ź�� ������ �޶��� ����� �����ϸ�,
// �̸� ������ ����Ͽ� ũ�ν��� ǥ���ϴ� �� ��ȿ�����̶�� �Ǵ��Ͽ�
// ũ�ν��� �ִ� ����� ����� ������
// (�׸��� ��� �ùķ����ʹϱ� ���� ��Ȳó�� ũ�ν��� ���� ���� �� �������̶� �Ǵ�)

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
