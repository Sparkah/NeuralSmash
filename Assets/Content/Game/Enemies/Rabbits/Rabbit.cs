using UnityEngine;

namespace Content.Game.Enemies.Rabbits
{
    public class Rabbit : MonoBehaviour
    {
        [SerializeField] private float _hp = 10;
        [SerializeField] private float _xpDropOnDeath = 1;
        [SerializeField] private XpDropScript _batXpDrop;
        
        private Enemy _enemy;
    
        private void Start()
        {
            _batXpDrop.Construct(_xpDropOnDeath);
            _enemy = GetComponentInChildren<Enemy>();
            _enemy.ConstructHealth(_hp);
            _enemy.ConstructDropXp(_batXpDrop.gameObject);
        }
    }
}