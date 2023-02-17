using Infrastructure.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartCampaign()
    {
        SceneManager.LoadScene(SceneNames.LevelIntroScene);
    }

    public void StartHordeSurvival()
    {
        SceneManager.LoadScene(SceneNames.HordeSurvival);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
