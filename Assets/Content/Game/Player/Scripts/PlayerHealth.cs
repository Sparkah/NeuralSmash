using Content.Game.UI.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Content.Game.Player.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _healthPoints = 10;
        private float _maxHP;
        private UIManager _uiManager;

        [Inject]
        public void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        private void Start()
        {
            _maxHP = _healthPoints;
        }

        public void TakeDamage(float damageAmount)
        {
            _healthPoints -= damageAmount;
            _uiManager.HPManager.UpdateHPView(-damageAmount/_maxHP);
            if (_healthPoints <= 0)
            {
                Debug.Log("Defeated");
                SceneManager.LoadScene(0); // конечно это некорректно. Надо создать отедльный сцен менеджер и стрелять сигналы/подписывать на ивенты. В связи с экономией времени сделал костыль
            }
        }
    }
}