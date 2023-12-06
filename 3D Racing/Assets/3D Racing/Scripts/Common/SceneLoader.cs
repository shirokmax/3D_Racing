using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(GlobalConsts.MAIN_MENU_SCENE_NAME);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
