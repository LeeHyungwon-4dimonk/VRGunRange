using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform m_muzzle;
    private Rigidbody m_bulletRigid;

    [SerializeField] float speed;

    private void Awake() => Init();

    private void Init()
    {
        m_muzzle = GetComponent<Transform>();
    }

    public void Fire()
    {
        GameObject bullet;
        bullet = Instantiate(m_bulletPrefab, m_muzzle.transform.position, m_muzzle.transform.rotation);
        m_bulletRigid = bullet.GetComponent<Rigidbody>();
        m_bulletRigid.velocity = m_muzzle.forward * speed;
        Debug.Log(bullet.transform.position);
        Debug.Log("น฿ป็");
    }
}
