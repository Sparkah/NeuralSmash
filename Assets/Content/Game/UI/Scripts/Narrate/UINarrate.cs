using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Content.Game.UI.Scripts.Narrate
{
    public class UINarrate : MonoBehaviour
    {
        private SignalBus _signalBus;
    
        [SerializeField] private CanvasGroup _image;
        public TextMeshProUGUI Text;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<CloseUINarrativeSignal>(FadeUINarrative);
        }

        private void Start()
        {
            ShowUINarrative(Dialogues.MonoLogIntroOne, false);
        }

        public void FadeUINarrative()
        {
            _image.DOFade(0f, 1.5f).SetUpdate(true);
            Time.timeScale = 1;
        }
    
        public void ShowUINarrative(string text, bool closeSelf)
        {
            Text.text = text;
            Time.
                timeScale = 0;
            _image.DOFade(1f, 1.5f).SetUpdate(true).onComplete += delegate
            {
                TryToSelfClose(closeSelf);
            };
        }

        private void TryToSelfClose(bool canSelfClose)
        {
            if (canSelfClose)
            {
                _image.DOFade(0.95f, 5f).SetUpdate(true).onComplete += FadeUINarrative;
            }
        }
    }
}