using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CarRacing
{
    public class UISelectableButton : UIButton
    {
        [SerializeField] private Image m_SelectImage;

        public UnityEvent OnSelect;
        public UnityEvent OnUnSelect;

        public override void EnableFocuse()
        {
            base.EnableFocuse();
            
            m_SelectImage.enabled = true;
            OnSelect?.Invoke();
        }

        public override void DisableFocuse()
        {
            base.DisableFocuse();

            m_SelectImage.enabled = false;
            OnUnSelect?.Invoke();
        }
    }
}
