using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Enabler/Mini Gun Enabler", order = 1)]
    public class MiniGunEnabler : ScriptableObject
    {
        public string UpgradeName = "Mini Gun Enabler";
        public bool MiniGunEnabled = true;
        public Sprite MiniGunEnablerSprite;
    }
}