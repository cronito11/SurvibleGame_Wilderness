using System;
using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class PhoneMenuUiManager : MonoBehaviour
    {
        [SerializeField] private Button attackButton;
        [SerializeField] private Button collectButton;

        public static event Action AttackButtonClicked;
        public static event Action CollectButtonClicked;

        private void OnEnable()
        {
            attackButton.onClick.AddListener(OnAttackButtonClicked);
            collectButton.onClick.AddListener(OnCollectButtonClicked);
        }
        
        private void OnDisable()
        {
            attackButton.onClick.RemoveListener(OnAttackButtonClicked);
            collectButton.onClick.RemoveListener(OnCollectButtonClicked);
        }

        private void OnAttackButtonClicked()
        {
            AttackButtonClicked?.Invoke();
        }

        private void OnCollectButtonClicked()
        {
            CollectButtonClicked?.Invoke();
        }
    }
}
