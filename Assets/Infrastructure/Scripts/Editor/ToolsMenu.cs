using Infrastructure.Scripts.Models;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Scripts.Editor
{
    public class ToolsMenu : MonoBehaviour
    {
        [MenuItem("Neural Tools/Drop State")]
        static void Init()
        {
            GameProgressHandler.DeleteFile();
        }
    }
}
