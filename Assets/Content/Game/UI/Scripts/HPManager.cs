using UnityEngine;
using UnityEngine.UI;

namespace Content.Game.UI.Scripts
{
    public class HPManager : MonoBehaviour
    {
        [SerializeField] private Image HPImage;

        public void UpdateHPView(float amount)
        {
            HPImage.fillAmount += amount;
        }
    }
}
