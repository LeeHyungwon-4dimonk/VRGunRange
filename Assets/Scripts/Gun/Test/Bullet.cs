using DesignPattern;
using UnityEngine;

public class Bullet : PooledObject
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        ReturnPool();
    }
}
