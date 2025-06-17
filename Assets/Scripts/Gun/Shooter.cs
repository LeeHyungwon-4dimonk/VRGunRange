using DesignPattern;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Muzzle�� ����� �Ѿ� �߻�� ��ũ��Ʈ
/// </summary>
public class Shooter : MonoBehaviour
{
    // Bullet ���� ������Ʈ
    [SerializeField] private ParticleBulletController m_bulletPrefab;
    [SerializeField] private GameObject m_muzzleFire;
    private ObjectPool m_bulletPool;    
    private Rigidbody m_bulletRigid;

    // ���� ������Ʈ
    private XRGrabInteractable m_interactable;
    private AudioSource m_audioSource;
    private Transform m_muzzle;

    // �뷱���� ���� - �ν����� ����
    [SerializeField] private float speed;
    [SerializeField] private float m_cooltime;

    // ����
    private float m_shootCooltime;

    // ������Ʈ Ǯ ���� �������� Ȯ�ο� ������Ʈ
    //[SerializeField] private MuzzleFire m_muzzleFire;
    //private ObjectPool m_muzzlePool;

    private void Awake() => Init();

    private void Init()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_muzzle = GetComponent<Transform>();
        m_interactable = GetComponentInParent<XRGrabInteractable>();
        m_bulletPool = new ObjectPool(transform, m_bulletPrefab);        
    }

    private void Update()
    {
        m_shootCooltime += Time.deltaTime;
    }

    // ���� �߻��� �� �ִ��� Ȯ�ο�
    // ���� �� ������ ��� �ִ�����, �߻� ��Ÿ���� �������� Ȯ�� �� bool ��ȯ
    private bool TryFire()
    {
        if(m_interactable.interactorsSelecting.Count >= 2 && m_shootCooltime > m_cooltime)
        {
            return true;
        }
        return false;
    }    

    public void Fire()
    {
        if (TryFire())
        {
            // �Ҹ� ���
            m_audioSource.Play();

            // �ѱ� �߻� ����Ʈ
            GameObject muzzleFire = Instantiate(m_muzzleFire, m_muzzle.position, m_muzzle.rotation);

            // ���ư��� �Ѿ��� ������Ʈ Ǯ�� ��������, ������ �ۿ�
            BulletFire();
            
            m_shootCooltime = 0;
        }
    }

    // Muzzle �߻� �κе� ObjectPool�� ó���ϰ� �;�����, Object Pool�� ó���ϸ�
    // �ٽ� Pop���� �� ��� ����� ���� �ʴ� ������ ����
    // �켱�� Instantiate�� ó���ϰ� �ı��ϴ� ������� ó��
    // ��ƼŬ ��� ���� ������ �ذ��� ���� �ذ��� �Ǹ� ������Ʈ Ǯ�� ��ȯ ���� 
    /*
    public void MuzzleFire()
    {
        PooledObject muzzleFire = m_muzzlePool.PopPool() as MuzzleFire;
        muzzleFire.transform.position = m_muzzle.position;
    }
    */

    // �Ѿ��� ������Ʈ Ǯ�� �������� ���ư��� ��
    private void BulletFire()
    {
        PooledObject bullet = m_bulletPool.PopPool() as ParticleBulletController;
        bullet.transform.position = m_muzzle.position;
        bullet.transform.rotation = m_muzzle.rotation;
        m_bulletRigid = bullet.GetComponent<Rigidbody>();
        m_bulletRigid.velocity = m_muzzle.forward * speed;
    }
}