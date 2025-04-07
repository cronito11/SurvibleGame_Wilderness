using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public class WeaponWielder : MonoBehaviour
    {
        Dictionary<string, GameObject> weaponDict = new Dictionary<string, GameObject>();
        [SerializeField] List<GameObject> weaponList = new List<GameObject>();

        private void Start()
        {
            foreach (GameObject weapon in weaponList)
            {
                if (!weaponDict.ContainsKey(weapon.name))
                {
                    weaponDict.Add(weapon.name, weapon);
                }
            }
        }

        private void OnEnable()
        {
            InventoryItem.OnConsumeWeaponItem += EquipWeapon;
            WeaponSlot.OnUnequipWeapon += UnequipWeapon;
        }

        private void OnDisable()
        {
            InventoryItem.OnConsumeWeaponItem -= EquipWeapon;
            WeaponSlot.OnUnequipWeapon -= UnequipWeapon;
        }

        public void EquipWeapon(GameItemSO weapon)
        {
           string weaponName = weapon.itemName;
            if(weaponDict.ContainsKey(weaponName))
            {
                weaponDict[weaponName].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Weapon {weaponName} not found in the dictionary.");
            }
        }

        public void UnequipWeapon(GameItemSO weapon)
        {
            string weaponName = weapon.itemName;
            if (weaponDict.ContainsKey(weaponName))
            {
                weaponDict[weaponName].SetActive(false);
            }
            else
            {
                Debug.LogWarning($"Weapon {weaponName} not found in the dictionary.");
            }
        }
        private void OnDestroy()
        {
            UnequiptAllWeapon();
        }

        private void UnequiptAllWeapon()
        {
            foreach (var weapon in weaponDict)
            {
                weapon.Value.SetActive(false);
            }
        }
    }
}
