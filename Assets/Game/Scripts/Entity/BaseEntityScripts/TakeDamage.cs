using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public interface IDamageTaker
    {
        event Action TakeDamageEvent;

        void TakeDamage(float damage);
    }
}