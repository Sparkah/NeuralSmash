using Content.Game.Player.Scripts.Bullet;
using UnityEngine;

namespace Content.Game.Player.Scripts.Weapon
{
    public class Bazooka : EnemiesDetecter
    {
        [SerializeField] private GameObject _bazookaBullet;
        [SerializeField] private float _bulletDamage = 15;
        [SerializeField] private float _bulletSpeed = 5f; 
        [SerializeField] private float _reloadTimer = 2f;
        private float _reloadTimeDefault;

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
            var bullet = Instantiate(_bazookaBullet, transform.position, Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<BazookaBullet>().Construct(_bulletSpeed, _bulletDamage);
        }

        #region Upgrades

        public void BazookaUpgrade(int reloadSpeedIncrease)
        {
            _reloadTimer -= reloadSpeedIncrease;
        }

        #endregion
    }
}
