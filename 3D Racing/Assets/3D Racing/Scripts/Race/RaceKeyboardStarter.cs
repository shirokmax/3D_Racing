using UnityEngine;

namespace CarRacing
{
    public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                m_RaceStateTracker.LaunchPreparationStart();
        }
    }
}
