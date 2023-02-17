using System;
using System.Collections;
using Content.Game.Enemies;
using UnityEngine;

namespace Content.Game.Player.Scripts.Bullet
{
    public class BazookaBullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionPS;
        private float _moveSpeed = 1f;
        private float _damage = 20;
        private MeshRenderer _mesh;
        private SphereCollider _collider;
        
        public void Construct(float moveSpeed, float damage)
        {
            _moveSpeed = moveSpeed;
            _damage = damage;
        }

        private void Start()
        {
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<SphereCollider>();
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
            _explosionPS.Play();
            _mesh.enabled = false;
            _collider.enabled = false;
            StartCoroutine(BazookaExplosion());
        }

        private IEnumerator BazookaExplosion()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}