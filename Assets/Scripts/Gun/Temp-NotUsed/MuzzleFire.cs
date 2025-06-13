using DesignPattern;
using UnityEngine;

public class MuzzleFire : PooledObject
{    
    private float muzzleFireTime;

    private void Update()
    {
        muzzleFireTime += Time.deltaTime;
        if(muzzleFireTime > 2)
        {
            ReturnPool();
        }
    }
}
