using DesignPattern;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ParticleBulletController m_bulletPrefab;
    private ObjectPool m_bulletPool;    

    [SerializeField] private GameObject m_muzzleFire;

    [SerializeField] private float speed;
    [SerializeField] private float m_cooltime;

    private Transform m_muzzle;
    private Rigidbody m_bulletRigid;

    private float m_shootCooltime;

    // ������Ʈ Ǯ ���� �������� Ȯ�ο� ������Ʈ
    //[SerializeField] private MuzzleFire m_muzzleFire;
    // ������Ʈ Ǯ ���� �������� Ȯ�ο� ������Ʈ
    //private ObjectPool m_muzzlePool;

    private void Awake() => Init();

    private void Init()
    {
        m_muzzle = GetComponent<Transform>();
        m_bulletPool = new ObjectPool(transform, m_bulletPrefab);        
    }

    private void Update()
    {
        m_shootCooltime += Time.deltaTime;
    }

    public void MuzzleFire()
    {
        if (m_shootCooltime > m_cooltime)
        {
            GameObject muzzleFire = Instantiate(m_muzzleFire, m_muzzle.position, m_muzzle.rotation);
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

    public void Fire()
    {
        if (m_shootCooltime > m_cooltime)
        {
            PooledObject bullet = m_bulletPool.PopPool() as ParticleBulletController;
            bullet.transform.position = m_muzzle.position;
            bullet.transform.rotation = m_muzzle.rotation;
            Debug.Log(bullet.transform.position);
            m_bulletRigid = bullet.GetComponent<Rigidbody>();
            m_bulletRigid.velocity = m_muzzle.forward * speed;
            m_shootCooltime = 0;
        }
    }
}
