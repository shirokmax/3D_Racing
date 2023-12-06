using UnityEngine;

namespace CarRacing
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        private Pauser m_Pauser;
        public void Construct(Pauser obj) => m_Pauser = obj;


    }
}
