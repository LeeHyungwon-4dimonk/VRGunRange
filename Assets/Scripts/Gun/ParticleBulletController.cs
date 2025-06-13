using DesignPattern;
using UnityEngine;

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
