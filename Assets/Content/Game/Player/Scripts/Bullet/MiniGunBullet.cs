using System;
using Content.Game.Enemies;
using UnityEngine;

namespace Content.Game.Player.Scripts.Bullet
{
    public class MiniGunBullet : MonoBehaviour
    {
        private float _moveSpeed = 5f;
        private float _damage = 1f;
        
        public void Construct(float moveSpeed, float damage)
        {
            _moveSpeed = moveSpeed;
            _damage = damage;
        }
        
        private void Update()
        {
            transform.position += transform.forward * (Time.deltaTime * _moveSpeed);
            transform.position = new Vector3(transform.position.x, 0.75f, transform.position.z);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out EnemyBulletCatcher enemy)) return;
            
            enemy.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}
