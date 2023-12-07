using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    public class GlobalDependenciesContainer : Dependency
    {
        [SerializeField] private Pauser m_Pauser;

        private static GlobalDependenciesContainer m_Instance;

        private void Awake()
        {
            if (m_Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            m_Instance = this;

            DontDestroyOnLoad(gameObject);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Pauser>(m_Pauser, monoBehaviourInScene);
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            FindAllObjectsToBind();
        }
    }
}
