using System;
using Content.Game.Enemies;
using UnityEngine;
using UnityEngine.Pool;

namespace Content.Game.Player.Scripts.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private Transform _target;
        private float _moveSpeed = 0.1f;
        private float _damage = 1;
        private SphereCollider _enemyCollider;

        public void Construct(Transform target, float moveSpeed, float damage)
        {
            _target = target;
            _moveSpeed = moveSpeed;
            _damage = damage;
            _enemyCollider = _target.GetComponent<SphereCollider>();
        }
    
        private void Update()
        {
            if (_target != null && _enemyCollider.enabled)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out EnemyBulletCatcher enemy)) return;
            //if (!other.TryGetComponent(out Enemy enemy)) return;
            //if (!(Math.Abs(transform.position.magnitude - _target.position.magnitude) < 0.01f)) return;
            
            enemy.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
        
        
    }
}