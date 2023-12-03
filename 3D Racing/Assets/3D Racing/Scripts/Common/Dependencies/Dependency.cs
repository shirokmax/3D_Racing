using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void BindAll(MonoBehaviour monoBehaviourInScene) { }

    /// <summary>
    /// ����� ���������� ������.
    /// ���� ������� ��������� ��������� � ������ �����, �� �������� ����� �������� ������ �� ������ ������
    /// </summary>
    /// <param name="mono"></param>
    protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class
    {
        (target as IDependency<T>)?.Construct(bindObject as T);
    }

    protected void FindAllObjectsToBind()
    {
        // ������ ���� ���������, ����������� �� �����
        MonoBehaviour[] monosInScene = FindObjectsOfType<MonoBehaviour>();

        // ��������� ���� ��������� �� �����.
        // ��� ����������� ����������� ���������� ������ ��������� ����� ���� � ������� ��� � ������ ������� �� ����������� ������������ ������ ������.
        foreach (var mono in monosInScene)
            BindAll(mono);
    }
}
