using UnityEngine;

public class SpawnObjectsByPropertiesList : MonoBehaviour
{
    [SerializeField] private Transform m_Parent;
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private ScriptableObject[] m_Properties;

    [ContextMenu(nameof(Spawn))]
    public void Spawn()
    {
        GameObject[] allObjects = new GameObject[m_Parent.childCount];

        for (int i = 0; i < m_Parent.childCount; i++)
            allObjects[i] = m_Parent.GetChild(i).gameObject;

        for (int i = 0; i < allObjects.Length; i++)
            DestroyImmediate(allObjects[i]);

        foreach (var properties in m_Properties)
        {
            GameObject gameObj = Instantiate(m_Prefab, m_Parent);
            IScriptableObjectProperty scriptableObjProp = gameObj.GetComponent<IScriptableObjectProperty>();
            scriptableObjProp.ApplyProperty(properties);
        }
    }
}
