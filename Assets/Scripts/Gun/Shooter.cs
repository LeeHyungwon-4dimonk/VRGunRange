using DesignPattern;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ParticleBulletController m_bulletPrefab;
    private ObjectPool m_bulletPool;    

    [SerializeField] private GameObject m_muzzleFire;

    [SerializeField] private float speed;

    private Transform m_muzzle;
    private Rigidbody m_bulletRigid;

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


    public void MuzzleFire()
    {
        GameObject muzzleFire = Instantiate(m_muzzleFire, m_muzzle.position, m_muzzle.rotation);
    }

    // Muzzle �߻� �κе� ObjectPool�� ó���ϰ� �;�����, Object Pool�� ó���ϸ�
    // �ٽ� Pop���� �� ��� ����� ���� �ʴ� ������ ����
    // �켱�� Instantiate�� ó���ϰ� �ı��ϴ� ������� ó��
    /*
    public void MuzzleFire()
    {
        PooledObject muzzleFire = m_muzzlePool.PopPool() as MuzzleFire;
        muzzleFire.transform.position = m_muzzle.position;
    }
    */

    public void Fire()
    {
        PooledObject bullet = m_bulletPool.PopPool() as ParticleBulletController;
        bullet.transform.position = m_muzzle.position;
        bullet.transform.rotation = m_muzzle.rotation;
        Debug.Log(bullet.transform.position);
        m_bulletRigid = bullet.GetComponent<Rigidbody>();
        m_bulletRigid.velocity = m_muzzle.forward * speed;
    }
}
