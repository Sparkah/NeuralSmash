using Infrastructure.Scripts.Constants;
using Infrastructure.Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioClip _menuSceneMusic;
    [SerializeField] private AudioClip _levelSceneMusic;

    private AudioSource _audioSource;
    private World _world;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals(SceneNames.MenuScene))
            _audioSource.clip = _menuSceneMusic;
        else
            _audioSource.clip = _levelSceneMusic;
        
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
