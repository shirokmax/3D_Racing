using UnityEngine;

namespace CarRacing
{
    public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Pauser>
    {
        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private Pauser m_Pauser;
        public void Construct(Pauser obj) => m_Pauser = obj;

        private void Update()
        {
            if (m_Pauser.IsPaused == false)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                    m_RaceStateTracker.LaunchPreparationStart();
            }
        }
    }
}
