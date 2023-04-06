using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using System;
using Assets.Game.Scripts.Services.PoolService;
using Assets.Game.Scripts.Entity.Bullet;

public class PoolService : IPoolService
{
    private Bullet _prefab;
    private ObjectPool<GameObject> _pool;
    public ObjectPool<GameObject> Pool { get => _pool; }

    public void CreatePool(Bullet obj)
    {
        _prefab = obj;
        _pool = new ObjectPool<GameObject>(Create, GetElement, DisposeElement);
    }

    private GameObject Create()
    {
        var init = GameObject.Instantiate(_prefab);

        init.Speed = _prefab.Speed;
        init.Damage = _prefab.Damage;
        init.TargetMask = _prefab.TargetMask;
        init.SetModel(_prefab.Model);
        init.LifeTime = _prefab.LifeTime;

        return init.gameObject;
    }

    public void DisposeElement(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void GetElement(GameObject obj)
    {
        obj.SetActive(true);
    }
}