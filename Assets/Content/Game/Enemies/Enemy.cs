using System;
using System.Collections;
using Content.Game.Player.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Content.Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public bool IsDead;
        public Action<NavMeshAgent> OnEnemyDeath;
        
        private float _detectRadius = 1.5f;
        private float _hp = 1;
        private float _damage = 1;
        private float _moveSpeed = 1;
        private GameObject _xpDrop;
        private float _distanceToPlayerToAttack = 1f;
        private float _attackRechargeTime = 1f;
        private bool _playerDetected;
        private NavMeshAgent _agent;
        private Transform _playerPosition;
        private Animator _animator;
        private SphereCollider _collider;
        private PlayerHealth _playerHealth;
        private bool _canAttack = true;

        #region Construct Enemy

        public void Construct(float detectRadius, float hp, float damage, float moveSpeed, GameObject xpDrop, float distanceToPlayerToAttack, float attackRechargeTime)
        {
            _detectRadius = detectRadius;
            _hp = hp;
            _damage = damage;
            _moveSpeed = moveSpeed;
            _xpDrop = xpDrop;
            _distanceToPlayerToAttack = distanceToPlayerToAttack;
            _attackRechargeTime = attackRechargeTime;
        }

        public void ConstructHealth(float hp)
        {
            _hp = hp;
        }

        public void ConstructDropXp(GameObject xpDrop)
        {
            _xpDrop = xpDrop;
        }

        public void ConstructDetectRadius(float detectRadius)
        {
            _detectRadius = detectRadius;
        }

        #endregion

        private void Start()
        {
            _agent = GetComponentInParent<NavMeshAgent>();
            _agent.speed = _moveSpeed;
            _animator = GetComponentInParent<Animator>();
            _collider = GetComponent<SphereCollider>();
            _collider.radius = _detectRadius;
        }

        private void Update()
        {
            if (IsDead) return;
            if (!_playerDetected) return;
            
            CheckIfPlayerCanBeAttacked();
            UpdatePlayerPosition();
        }

        private void UpdatePlayerPosition()
        {
            _agent.destination = _playerPosition.position;
            _agent.transform.LookAt(_playerPosition);
        }

        private void CheckIfPlayerCanBeAttacked()
        {
            if (Vector3.Distance(transform.position, _playerPosition.position) <= _distanceToPlayerToAttack && _canAttack)
            {
                AttackPlayer();
            }
        }

        private void AttackPlayer()
        {
            _animator.SetTrigger("Attack");
            _playerHealth.TakeDamage(_damage);
            _canAttack = false;
            StartCoroutine(RechargeAttack());
        }

        private IEnumerator RechargeAttack()
        {
            yield return new WaitForSeconds(_attackRechargeTime);
            _canAttack = true;
        }

        private void OnTriggerStay(Collider player)
        {
            if (!player.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) return;
            _playerDetected = true;
            _playerHealth = playerHealth;
            _playerPosition = playerHealth.transform;
        }

        private void OnTriggerExit(Collider player)
        {
            if (!player.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) return;
            _playerHealth = null;
            _playerPosition = default;
            _playerDetected = false;
        }

        private void KillEnemy()
        {
            _animator.SetTrigger("Die");
            _collider.center = new Vector3(1000, 1000, 1000);
            OnEnemyDeath.Invoke(_agent);
            _agent.SetDestination(transform.position);
            _agent.speed = 0;
            StartCoroutine(DestroyObject());
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(1.9f);
            Instantiate(_xpDrop, transform.position
                                 + new Vector3(0, 1, 0), Quaternion.identity);
            //Pull instead of destroy
            Destroy(_agent.gameObject);
        }

        public void TakeDamage(float damage)
        {
            _hp -= damage;

            if (!(_hp <= 0)) return;
            IsDead = true;
            _collider.enabled = false;
            KillEnemy();
        }
    }
}
