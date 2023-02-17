using Content.Game.Player.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Game.UI.Scripts.Narrate
{
    public class NarrativeCollider : MonoBehaviour
    {
        [SerializeField] private string _text;
        
        private UINarrate _uiNarrate;
        
        [Inject]
        public void Construct(UINarrate uiNarrate)
        {
            _uiNarrate = uiNarrate;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                _uiNarrate.ShowUINarrative(_text, true);
                gameObject.SetActive(false);
            }
        }
    }
}
