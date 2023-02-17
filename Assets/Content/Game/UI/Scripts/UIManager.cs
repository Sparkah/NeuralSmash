using UnityEngine;

namespace Content.Game.UI.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public XPManager XPManager;
        public HPManager HPManager;

        private void Start()
        {
            XPManager = GetComponentInChildren<XPManager>();
            HPManager = GetComponentInChildren<HPManager>();
        }
    }
}
