using Content.Game.Player.Scripts.Weapon;
using UnityEngine;

namespace Content.Game.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public Gun Gun;
        [HideInInspector] public MiniGun MiniGun;
        [HideInInspector] public SpikesGun SpikesGun;
        [HideInInspector] public Bazooka Bazooka;
        [HideInInspector] public PlayerXP PlayerXp;
        [HideInInspector] public PlayerHealth PlayerHealth;

        private void Start()
        {
            Gun = GetComponentInChildren<Gun>();
            MiniGun = GetComponentInChildren<MiniGun>();
            MiniGun.enabled = false;
            SpikesGun = GetComponentInChildren<SpikesGun>(); 
            SpikesGun.enabled = false;
            Bazooka = GetComponentInChildren<Bazooka>();
            Bazooka.enabled = false;
            PlayerXp =  GetComponentInChildren<PlayerXP>();
            PlayerHealth = GetComponentInChildren<PlayerHealth>();
        }
    }
}
