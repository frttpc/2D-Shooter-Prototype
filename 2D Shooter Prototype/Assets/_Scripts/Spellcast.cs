using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcast : ProjectileBase
{
    [SerializeField] private ParticleSystem _particleSystem;

    protected override void DestroyObject()
    {
        ParticleSystem particle = Instantiate(_particleSystem, gameObject.transform.position, Quaternion.identity);
        Destroy(particle.gameObject, particle.main.duration);
        base.DestroyObject();
    }
}
