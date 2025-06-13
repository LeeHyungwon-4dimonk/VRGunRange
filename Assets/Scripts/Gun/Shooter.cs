using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet m_bulletPrefab;

    private ObjectPool m_bulletPool;
    private Transform m_muzzle;
    private Rigidbody m_bulletRigid;

    [SerializeField] float speed;

    private void Awake() => Init();

    private void Init()
    {
        m_muzzle = GetComponent<Transform>();
        m_bulletPool = new ObjectPool(transform, m_bulletPrefab);
    }

    public void Fire()
    {
        PooledObject bullet = m_bulletPool.PopPool() as Bullet;
        m_bulletRigid = bullet.GetComponent<Rigidbody>();
        m_bulletRigid.velocity = m_muzzle.forward * speed;
    }
}
