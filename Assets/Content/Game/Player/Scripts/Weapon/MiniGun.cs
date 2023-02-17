using System.Collections;
using System.Collections.Generic;
using Content.Game.Player.Scripts.Bullet;
using UnityEngine;

namespace Content.Game.Player.Scripts.Weapon
{
    public class MiniGun : EnemiesDetecter
    {
        [SerializeField] private GameObject _miniGunBullet;
        [SerializeField] private float _bulletDamage = 1;
        [SerializeField] private float _bulletSpeed = 0.1f;
        
        private float _reloadTimer = 1f;
        private float _reloadTimeDefault;
        private Transform _closestEnemy;
        private int _numberOfShots = 3;
        private float _fireRate = 5;
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
            for (int i = 0; i < _numberOfShots; i++)
            {
                Invoke(nameof(ShootBullet), i/_fireRate);
            }
        }

        private void ShootBullet()
        {
            var bullet = GetBulletFromThePool();
            bullet.transform.rotation = transform.rotation;
            bullet.transform.position = transform.position;
            bullet.GetComponent<MiniGunBullet>().Construct(_bulletSpeed, _bulletDamage);
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
            var bulletNew = Instantiate(_miniGunBullet, transform.position, Quaternion.identity);
            _bulletPool.Add(bulletNew);
            return bulletNew;
        }
        
        private IEnumerator AddBulletBackToPool(GameObject bullet)
        {
            yield return new WaitForSeconds(5f);
            bullet.SetActive(false);
        }
        
        #region Upgrades

        public void MiniGunUpgrade(int addShots)
        {
            _numberOfShots += addShots;
        }

        #endregion
    }
}
