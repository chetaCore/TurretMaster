using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.ScriptableObject
{
    public class IdComparer : IComparer<TurretData>
    {
        public int Compare(TurretData x, TurretData y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}