using UnityEngine;

namespace CarRacing
{
    public class UIRaceButtonSpawner : MonoBehaviour
    {
        [SerializeField] private Transform m_Parent;
        [SerializeField] private UIRaceButton m_ButtonPrefab;
        [SerializeField] private RaceInfo[] m_Properties;

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            if (Application.isPlaying) return;

            GameObject[] allObjects = new GameObject[m_Parent.childCount];

            for (int i = 0; i < m_Parent.childCount; i++)
                allObjects[i] = m_Parent.GetChild(i).gameObject;

            for (int i = 0; i < allObjects.Length; i++)
                DestroyImmediate(allObjects[i]);

            for (int i = 0; i < m_Properties.Length; i++)
            {
                UIRaceButton button = Instantiate(m_ButtonPrefab, m_Parent);
                button.ApplyProperties(m_Properties[i]);
            }
        }
    }
}
