using UnityEngine;

namespace Content.Game.Enemies
{
    public class EnemyBulletCatcher : MonoBehaviour
    {
        private Enemy _enemy;
        private SphereCollider _sphereCollider;
    
        void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _enemy = GetComponentInParent<Enemy>();
            transform.position = new Vector3(transform.position.x, 0.75f, transform.position.z);
        }

        public void TakeDamage(float damageAmount)
        {
            _enemy.TakeDamage(damageAmount);
            if (_enemy.IsDead)
            {
                _sphereCollider.enabled = false;
            }
        }
    }
}
