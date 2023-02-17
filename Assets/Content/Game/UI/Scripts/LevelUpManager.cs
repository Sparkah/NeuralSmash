using System.Collections.Generic;
using System.Linq;
using Content.Game.Player.Scripts;
using Content.Game.Player.Scripts.Upgrades;
using Infrastructure.Scripts.Constants;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Content.Game.UI.Scripts
{
    public class LevelUpManager : MonoBehaviour
    {
        public AbilityItem[] Abilities;
        
        private bool _firstTimeLaunch;
        private SignalBus _signalBus;
        [ShowInInspector]
        private List<ScriptableObject> _upgrades = new List<ScriptableObject>();
        private PlayerController _playerController;
        private bool _tutorScene; //Смешение ответственности не совсем хорошо. Вынести систему тутора в отедльный менеджер, оставить методы апи

        [Inject]
        private void Construct(SignalBus signalBus, PlayerController playerController)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<LevelUpSignal>(LevelUpUI);
            _playerController = playerController;
        }
        
        private void Start()
        {
            if (_firstTimeLaunch) return;

            ShowTutor();
            _upgrades = Resources.LoadAll<ScriptableObject>("Upgrades").ToList();
            _firstTimeLaunch = true;
            LevelUpUI();
        }

        private void LevelUpUI()
        {
            if (_upgrades.Count <= 0) return;
            
            gameObject.SetActive(true);
            Time.timeScale = 0;
            var upgradesCount = _upgrades.Count - 1;
            List<int> upgradesToDisplay = new List<int>();

            for (var i = 0; i < 3; i++)
            {
                Abilities[i].gameObject.SetActive(true);
                var upgradeNumber = RollNumber(upgradesCount, upgradesToDisplay);
                upgradesToDisplay.Add(upgradeNumber);
                ShowUpgrade(upgradeNumber, i);
                upgradesCount -= 1;
                if(upgradesCount<0)
                    return;
            }
        }

        private int RollNumber(int upgradesCount, List<int> upgradesToDisplay)
        {
            var range = Enumerable.Range(0, 100).Where(i => !upgradesToDisplay.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, upgradesCount);
            return range.ElementAt(index);
        }
        

        //Метод будет слишком большим. Использовать switch?
        private void ShowUpgrade(int upgradeNumber, int currentAbilityNum)
        {
            if (_tutorScene)
            {
                Abilities[currentAbilityNum].button.onClick.AddListener(CloseUINarrative);
            }
            
            if (_upgrades[upgradeNumber].GetType() == typeof(GunUpgrade))
            {
                GunUpgrade(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(HealthRestore))
            {
                RestoreHealth(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(MiniGunEnabler))
            {
                EnableMiniGun(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(BazookaEnabler))
            {
                EnableBazooka(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(SpikesGunEnabler))
            {
                EnableSpikesGun(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(SpikesGunUpgrade))
            {
                UpgradeSpikesGun(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(BazookaUpgrade))
            {
                UpgradeBazooka(upgradeNumber, currentAbilityNum);
            }
            if (_upgrades[upgradeNumber].GetType() == typeof(MiniGunUpgrade))
            {
                UpgradeMiniGun(upgradeNumber, currentAbilityNum);
            }
        }

        private void RemoveUpgradeFromList(ScriptableObject itemObject)
        {
            _upgrades.Remove(itemObject);
            CloseUpgradeWindow();
        }

        private void CloseUpgradeWindow()
        {
            foreach (var ability in Abilities)
            {
                ability.button.onClick.RemoveAllListeners();
                ability.gameObject.SetActive(false);
            }

            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        //Тут не смог придумать как высасывать из Scriptable Object информацию без явной типизации. Пришлось повторять код
        #region Upgrades
        
        //Gun Upgrade
        private void GunUpgrade(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as GunUpgrade;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplyGunUpgrade(currentUpgrade.AdditionalDamage);
                RemoveUpgradeFromList(currentUpgrade);
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.GunUpgradeSprite;
        }

        private void ApplyGunUpgrade(float additionalDamage)
        {
            _playerController.Gun.GunUpgrade(additionalDamage);
        }

        //Mini Gun Upgrade
        private void UpgradeMiniGun(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as MiniGunUpgrade;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplyMiniGunUpgrade(currentUpgrade.AdditionalBulletsShot);
                RemoveUpgradeFromList(currentUpgrade);
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.MiniGinUpgradeSprite;
        }

        private void ApplyMiniGunUpgrade(int currentUpgradeAdditionalBulletsShot)
        {
            _playerController.MiniGun.MiniGunUpgrade(currentUpgradeAdditionalBulletsShot);
        }

        //Bazooka Upgrade
        private void UpgradeBazooka(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as BazookaUpgrade;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplyBazookaUpgrade(currentUpgrade.ReloadSpeedDecrease);
                RemoveUpgradeFromList(currentUpgrade);
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.BazookaUpgradeSprite;
        }
        
        private void ApplyBazookaUpgrade(int decreaseReloadTime)
        {
            _playerController.Bazooka.BazookaUpgrade(decreaseReloadTime);
        }

        //Spikes Gun Upgrade
        private void UpgradeSpikesGun(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as SpikesGunUpgrade;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplySpikesGunUpgrade(currentUpgrade.IncreaseSpikesAmount, currentUpgrade.AdditionalDamage);
                RemoveUpgradeFromList(currentUpgrade);
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.SpikesGunUpgradeSprite;
        }
        
        private void ApplySpikesGunUpgrade(int increaseSpikesAmount, int increaseSpikesDamage)
        {
            _playerController.SpikesGun.SpikesGunUpgrade(increaseSpikesAmount, increaseSpikesDamage);
        }

        #endregion

        #region Stat Boosters

        //Health Restore
        private void RestoreHealth(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as HealthRestore;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                RestoreHealth(currentUpgrade.RestoreAmount);
                CloseUpgradeWindow();
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.HealthRestoreSprite;
        }

        private void RestoreHealth(int amount)
        {
            _playerController.PlayerHealth.TakeDamage(-amount);
        }

        #endregion

        //Использовать Generic? Нельзя неявно прочитать Scriptable Object
        #region Gun Enablers
        
        //MiniGun Enable
        private void EnableMiniGun(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as MiniGunEnabler;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplyMiniGunEnabler(currentUpgrade.MiniGunEnabled);
                RemoveUpgradeFromList(currentUpgrade);
                EnableMiniGunUpgrades();
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.MiniGunEnablerSprite;
        }

        private void ApplyMiniGunEnabler(bool enable)
        {
            _playerController.MiniGun.enabled = enable;
        }

        private void EnableMiniGunUpgrades()
        {
            var miniGunUpgrades = Resources.LoadAll<ScriptableObject>("MiniGunAdditionalUpgrades").ToList();
            foreach (var upgrade in miniGunUpgrades)
            {
                _upgrades.Add(upgrade);
            }
        }

        //Spikes Gun Enabler
        private void EnableSpikesGun(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as SpikesGunEnabler;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplySpikesGunEnabler(currentUpgrade.SpikesGunEnabled);
                RemoveUpgradeFromList(currentUpgrade);
                EnableSpikesUpgrades();
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.SpikesGunEnablerSprite;
        }
        
        private void ApplySpikesGunEnabler(bool enable)
        {
            _playerController.SpikesGun.enabled = enable;
        }

        private void EnableSpikesUpgrades()
        {
            var spikesGunUpgrades = Resources.LoadAll<ScriptableObject>("SpikesGunAdditionalUpgrades").ToList();
            foreach (var upgrade in spikesGunUpgrades)
            {
                _upgrades.Add(upgrade);
            }
        }

        //Bazooka Enabler
        private void EnableBazooka(int upgradeNumber, int currentAbilityNum)
        {
            var currentUpgrade = _upgrades[upgradeNumber] as BazookaEnabler;
            
            Abilities[currentAbilityNum].button.onClick.AddListener(delegate
            {
                ApplyBazookaEnabler(currentUpgrade.BazookaEnabled);
                RemoveUpgradeFromList(currentUpgrade);
                EnableBazookaUpgrades();
            });
            Abilities[currentAbilityNum].abilityName.text = currentUpgrade.UpgradeName;
            Abilities[currentAbilityNum].icon.sprite = currentUpgrade.BazookaEnablerSprite;
        }
        
        private void ApplyBazookaEnabler(bool enable)
        {
            _playerController.Bazooka.enabled = enable;
        }

        private void EnableBazookaUpgrades()
        {
            var bazookaUpgrades = Resources.LoadAll<ScriptableObject>("BazookaAdditionalUpgrades").ToList();
            foreach (var upgrade in bazookaUpgrades)
            {
                _upgrades.Add(upgrade);
            }
        }

        #endregion

        #region Tutorial

        private void CloseUINarrative()
        {
            _signalBus.Fire<CloseUINarrativeSignal>();
        }

        private void ShowTutor()
        {
            if (SceneManager.GetActiveScene().name == SceneNames.LevelIntroScene)
            {
                _tutorScene = true;
            }
        }

        #endregion
    }
}