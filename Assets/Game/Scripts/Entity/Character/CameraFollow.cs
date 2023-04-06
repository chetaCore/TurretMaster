using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Character
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _folloing;

        public float RotationAngleX;
        public int Distance;
        public float OffsetY;


        private void LateUpdate()
        {
            if (_folloing == null) return;

            Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);

            Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
            
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            _folloing = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followPosition = _folloing.position;
            followPosition.y += OffsetY;

            return followPosition;
        }
    }
}