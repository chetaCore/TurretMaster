using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class BaseObserver : MonoBehaviour
    {
        private Collider[] _detectedColliders = new Collider[1];

        private float _fieldOfView;
        private LayerMask _detectionMask;
        private float _detectionInterval;

        private bool _detecting;
        public bool CanDetect;

        private Color _detectingColor = Color.green;

        public float FieldOfView { get => _fieldOfView; set => _fieldOfView = value; }
        public LayerMask DetectionMask { get => _detectionMask; set => _detectionMask = value; }
        public float DetectionInterval { get => _detectionInterval; set => _detectionInterval = value; }

        public Collider[] DetectedColliders => _detectedColliders;

        private void Update()
        {
            if (!CanDetect)
            {
                _detectingColor = Color.black;
                return;
            }
            if (_detecting) return;

            Detecting();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _detectingColor;
            Gizmos.DrawWireSphere(transform.position, _fieldOfView);
        }

        private void Detecting()
        {
            _detecting = true;
 
            Array.Clear(DetectedColliders, 0, DetectedColliders.Length);

            var detectSeq = DOTween.Sequence();

            Physics.OverlapSphereNonAlloc(transform.position, _fieldOfView, DetectedColliders, DetectionMask);

            if (DetectedColliders[0] != null)
                _detectingColor = Color.red;
            else
                _detectingColor = Color.yellow;

            detectSeq.AppendInterval(_detectionInterval).OnComplete(() =>
            {
                _detecting = false;
                detectSeq.Kill();
            });
        }
    }
}