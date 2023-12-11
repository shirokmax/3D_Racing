using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class GlobalDependenciesContainer : Dependency
    {
        [SerializeField] private Pauser m_Pauser;
        [SerializeField] private LoadedRaceSceneInfo m_SceneInfo;
        [SerializeField] private RaceCompletion m_RaceCompletion;

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

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Pauser>(m_Pauser, monoBehaviourInScene);
            Bind<LoadedRaceSceneInfo>(m_SceneInfo, monoBehaviourInScene);
            Bind<RaceCompletion>(m_RaceCompletion, monoBehaviourInScene);
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            FindAllObjectsToBind();
        }
    }
}
