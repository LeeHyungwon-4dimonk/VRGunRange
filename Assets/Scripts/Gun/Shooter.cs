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

    // 오브젝트 풀 적용 가능한지 확인용 컴포넌트
    //[SerializeField] private MuzzleFire m_muzzleFire;
    // 오브젝트 풀 적용 가능한지 확인용 컴포넌트
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

    // Muzzle 발사 부분도 ObjectPool로 처리하고 싶었으나, Object Pool로 처리하면
    // 다시 Pop했을 때 모션 재생이 되지 않는 오류가 있음
    // 우선은 Instantiate로 처리하고 파괴하는 방식으로 처리
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
