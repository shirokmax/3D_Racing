using UnityEngine;

public class SpawnAllObjectsInSceneByPropertiesList : MonoBehaviour
{
    private SpawnObjectsByPropertiesList[] m_Spawners;

    public void Initialize()
    {
        m_Spawners = FindObjectsOfType<SpawnObjectsByPropertiesList>(true);

        foreach (SpawnObjectsByPropertiesList spawner in m_Spawners)
            spawner.Spawn();
    }
}
