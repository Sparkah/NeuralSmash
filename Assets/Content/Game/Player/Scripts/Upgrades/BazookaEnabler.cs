using UnityEngine;

namespace Content.Game.Player.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "Neural", menuName = "Upgrades/Enabler/Bazooka Enabler", order = 1)]
    public class BazookaEnabler : ScriptableObject
    {
        public string UpgradeName = "Bazooka Enabler";
        public bool BazookaEnabled = true;
        public Sprite BazookaEnablerSprite;
    }
}
