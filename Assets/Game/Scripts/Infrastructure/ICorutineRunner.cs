using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public interface ICorutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}