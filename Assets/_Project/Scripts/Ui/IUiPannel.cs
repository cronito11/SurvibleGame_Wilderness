using UnityEngine;

namespace Surviblewilderness
{
    public interface IUiPannel 
    {
        void Show();
        void Hide();
        bool IsActive();
    }
}
