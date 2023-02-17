using UnityEngine;
using UnityEngine.UI;

namespace Content.Game.UI.Scripts
{
    public class XPManager : MonoBehaviour
    {
        [SerializeField] private Image XPImage;
        public void IncreaseXPView(float amount)
        {
            XPImage.fillAmount += amount;
        }

        public void SetXPPercentage(float value)
        {
            XPImage.fillAmount = value;
        }
    }
}