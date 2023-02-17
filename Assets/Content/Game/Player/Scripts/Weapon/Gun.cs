using System.Collections;
using System.Collections.Generic;
using Content.Game.Enemies;
using UnityEngine;

namespace Content.Game.Player.Scripts.Weapon
{
    public class Gun : EnemiesDetecter
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _bulletDamage = 1;
        [SerializeField] private float _bulletSpeed = 0.1f;
        
        private float _reloadTimer = 1f;
        private float _reloadTimeDefault;
        private Transform _closestEnemy;
        private List<GameObject> _bulletPool = new List<GameObject>();

        private void Awake()
        {
            _reloadTimeDefault = _reloadTimer;
        }

        private void Update()
        {
            _reloadTimer -= Time.deltaTime;
            if (!(_reloadTimer <= 0) || _enemiesInRange.Count < 1) return;
            
            _reloadTimer = _reloadTimeDefault;
            ShootBullet();
        }

        private void ShootBullet()
        {
            float distanceToClosestEnemy = 999;

            foreach (var enemy in _enemiesInRange)
            {
                if (enemy == null || !enemy.enabled)
                {
                    _enemiesInRange.Remove(enemy);
                    return;
                }
                var distanceToCurrentEnemy = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                if (!(distanceToCurrentEnemy < distanceToClosestEnemy)) continue;
                distanceToClosestEnemy = distanceToCurrentEnemy;
                _closestEnemy = enemy.gameObject.transform;
            }

            var bullet = GetBulletFromThePool();
            bullet.transform.position = gameObject.transform.position;
            bullet.GetComponent<Bullet.Bullet>().Construct(_closestEnemy.GetComponentInChildren<EnemyBulletCatcher>().transform, _bulletSpeed, _bulletDamage);
            StartCoroutine(AddBulletBackToPool(bullet));
        }

        private GameObject GetBulletFromThePool()
        {
            foreach (var bullet in _bulletPool)
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    return bullet;
                }
            }
            var bulletNew = Instantiate(_bullet, transform.position, Quaternion.identity);
            _bulletPool.Add(bulletNew);
            return bulletNew;
        }

        private IEnumerator AddBulletBackToPool(GameObject bullet)
        {
            yield return new WaitForSeconds(5f);
            bullet.SetActive(false);
        }
        
        #region Upgrades

        public void GunUpgrade(float addDamage)
        {
            _bulletDamage += addDamage;
        }

        #endregion
    }
}