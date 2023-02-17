using System;
using Content.Game.Player.Scripts;
using Infrastructure.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Content.Game.SceneManagement
{
    public class TeleportScript : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                SceneManager.LoadScene(SceneNames.MenuScene);
            }
        }
    }
}