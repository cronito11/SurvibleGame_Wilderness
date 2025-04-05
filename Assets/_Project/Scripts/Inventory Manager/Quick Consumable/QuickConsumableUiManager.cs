using NUnit.Framework;
using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class QuickConsumableUiManager : MonoBehaviour
    {
        [SerializeField] QuickConsumableManager quickConsumableManager;

        

        private void OnEnable()
        {
            QuickConsumableManager.OnConsumableChanged += UpdateQuickConsumableUI;
        }

        private void UpdateQuickConsumableUI()
        {
            
        }

        private void OnDisable()
        {
            QuickConsumableManager.OnConsumableChanged -= UpdateQuickConsumableUI;  
        }
    }
}
