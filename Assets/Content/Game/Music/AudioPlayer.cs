using UnityEngine;

namespace Content.Game.Music
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Audio { get; private set; }

        [Header("AudioSources")] 
        [SerializeField] private AudioSource _audioSourceMain;
        [SerializeField] private AudioSource _audioSourceSecondary;

        private void Awake()
        {
            if (Audio != null && Audio != this)
                Destroy(this);
            else
                Audio = this;
        }
    }
}