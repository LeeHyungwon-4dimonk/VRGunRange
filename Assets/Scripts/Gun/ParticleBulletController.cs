using DesignPattern;
using UnityEngine;

/// <summary>
/// 파티클 적용 총알 발사용 스크립트
/// 기존 에셋에 적용되어 있던 코드를 변형하여 적용
/// </summary>
public class ParticleBulletController : PooledObject
{
    public GameObject hitPrefab;

    void OnCollisionEnter(Collision co)
    {
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                ReturnPool();
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                ReturnPool();
            }
        }
        ReturnPool();
    }
}
