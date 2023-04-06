using Assets.Game.Scripts.Entity.Bullet;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Services.PoolService
{
    public interface IPoolService : IService
    {
        ObjectPool<GameObject> Pool { get; }

        public void CreatePool(Bullet obj);
        public void GetElement(GameObject obj);
        public void DisposeElement(GameObject bullet);

    }
}