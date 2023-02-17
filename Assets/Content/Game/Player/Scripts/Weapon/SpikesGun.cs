using System.Collections;
using System.Collections.Generic;
using Content.Game.Player.Scripts.Bullet;
using UnityEngine;

namespace Content.Game.Player.Scripts.Weapon
{
    public class SpikesGun : EnemiesDetecter
    {
        [SerializeField] private int _numberOfSpikeBulletsInitial = 3;
        [SerializeField] private GameObject _spikesGunBullet;
        [SerializeField] private float _bulletDamage = 1;
        [SerializeField] private float _bulletSpeed = 5f;
        
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
            
            Debug.Log(_enemiesInRange.Count);
            _reloadTimer = _reloadTimeDefault;

            var degree = 360 / _numberOfSpikeBulletsInitial;
            for (int i = 0; i < _numberOfSpikeBulletsInitial; i++)
            {
                var rotationDegree = i * degree;
                ShootBullet(rotationDegree);
            }
        }

        private void ShootBullet(int degree)
        {
            var bullet = GetBulletFromThePool(degree);
            bullet.transform.position = transform.position;
            bullet.GetComponent<SpikesBullet>().Construct(_bulletSpeed, _bulletDamage);
            StartCoroutine(AddBulletBackToPool(bullet));
        }
        
        private GameObject GetBulletFromThePool(int degree)
        {
            foreach (var bullet in _bulletPool)
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    return bullet;
                }
            }
            var bulletNew = Instantiate(_spikesGunBullet, transform.position, Quaternion.Euler(new Vector3(0,degree,0)));
            _bulletPool.Add(bulletNew);
            return bulletNew;
        }
        
        private IEnumerator AddBulletBackToPool(GameObject bullet)
        {
            yield return new WaitForSeconds(5f);
            bullet.SetActive(false);
        }

        #region Upgrades

        public void SpikesGunUpgrade(int increaseSpikesAmount, int increaseSpikesDamage)
        {
            _numberOfSpikeBulletsInitial += increaseSpikesAmount;
            _bulletDamage += increaseSpikesDamage;
        }

        #endregion
    }
}
