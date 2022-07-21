using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot(Transform shootPosition)
    {
        Instantiate(Bullet, shootPosition.position, Quaternion.identity);
    }
}