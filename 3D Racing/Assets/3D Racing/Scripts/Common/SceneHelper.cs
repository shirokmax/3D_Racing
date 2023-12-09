using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class SceneHelper : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(GlobalConsts.MAIN_MENU_SCENE_NAME);
        }

        public void RestartRace()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
