using DesignPattern;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Muzzle에 적용된 총알 발사용 스크립트
/// </summary>
public class Shooter : MonoBehaviour
{
    // Bullet 관련 컴포넌트
    [SerializeField] private ParticleBulletController m_bulletPrefab;
    [SerializeField] private GameObject m_muzzleFire;
    private ObjectPool m_bulletPool;    
    private Rigidbody m_bulletRigid;

    // 참조 컴포넌트
    private XRGrabInteractable m_interactable;
    private AudioSource m_audioSource;
    private Transform m_muzzle;

    // 밸런스용 변수 - 인스펙터 조정
    [SerializeField] private float speed;
    [SerializeField] private float m_cooltime;

    // 변수
    private float m_shootCooltime;

    // 오브젝트 풀 적용 가능한지 확인용 컴포넌트
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

    // 총을 발사할 수 있는지 확인용
    // 총을 양 손으로 잡고 있는지와, 발사 쿨타임이 지났는지 확인 후 bool 반환
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
            // 소리 재생
            m_audioSource.Play();

            // 총구 발사 이펙트
            GameObject muzzleFire = Instantiate(m_muzzleFire, m_muzzle.position, m_muzzle.rotation);

            // 날아가는 총알을 오브젝트 풀로 꺼내오고, 물리력 작용
            BulletFire();
            
            m_shootCooltime = 0;
        }
    }

    // Muzzle 발사 부분도 ObjectPool로 처리하고 싶었으나, Object Pool로 처리하면
    // 다시 Pop했을 때 모션 재생이 되지 않는 오류가 있음
    // 우선은 Instantiate로 처리하고 파괴하는 방식으로 처리
    // 파티클 출력 오류 문제를 해결해 보고 해결이 되면 오브젝트 풀로 전환 예정 
    /*
    public void MuzzleFire()
    {
        PooledObject muzzleFire = m_muzzlePool.PopPool() as MuzzleFire;
        muzzleFire.transform.position = m_muzzle.position;
    }
    */

    // 총알을 오브젝트 풀로 가져오고 날아가게 함
    private void BulletFire()
    {
        PooledObject bullet = m_bulletPool.PopPool() as ParticleBulletController;
        bullet.transform.position = m_muzzle.position;
        bullet.transform.rotation = m_muzzle.rotation;
        m_bulletRigid = bullet.GetComponent<Rigidbody>();
        m_bulletRigid.velocity = m_muzzle.forward * speed;
    }
}