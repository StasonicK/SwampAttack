using System.Collections;
using UnityEngine;

public class Uzi : Weapon
{
    private Coroutine _coroutine;

    public override void Shoot(Transform shootPosition)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DoShoot(shootPosition));
    }

    private IEnumerator DoShoot(Transform shootPosition)
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Bullet, shootPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}