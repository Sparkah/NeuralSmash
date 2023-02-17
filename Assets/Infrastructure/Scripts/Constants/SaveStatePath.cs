using UnityEngine;

namespace Infrastructure.Scripts.Constants
{
    public static class SaveStatePath
    {
        public static readonly string StatePath = Application.persistentDataPath + "/config/local.state";
    }
}