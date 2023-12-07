using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(GlobalConsts.MAIN_MENU_SCENE_NAME);
        }

        public void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
