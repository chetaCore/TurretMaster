using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.GamePlay
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private int _stage;

        public int Id { get => _id;}
        public int Stage { get => _stage;}


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 100, transform.position.z));
            
        }
        

        
    }
}