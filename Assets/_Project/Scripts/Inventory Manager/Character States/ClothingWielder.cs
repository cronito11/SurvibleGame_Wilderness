using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public class ClothingWielder : MonoBehaviour
    {
        Dictionary<string, GameObject> outfitDict = new Dictionary<string, GameObject>();
        [SerializeField] List<GameObject> outfitList = new List<GameObject>();


        private void Start()
        {
            foreach (GameObject weapon in outfitList)
            {
                if (!outfitDict.ContainsKey(weapon.name))
                {
                    outfitDict.Add(weapon.name, weapon);
                }
            }
        }

        private void OnEnable()
        {
            InventoryItem.OnConsumeClothingItem += EquipOutfit;
            OutfitSlot.OnUnequipOutfit += UnequipOutfit;
        }

        private void OnDisable()
        {
            InventoryItem.OnConsumeClothingItem -= EquipOutfit;
            OutfitSlot.OnUnequipOutfit -= UnequipOutfit;
        }

        public void EquipOutfit(GameItemSO outfit)
        {
            string outfitName = outfit.itemName;
            if (outfitDict.ContainsKey(outfitName))
            {
                outfitDict[outfitName].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Weapon {outfitName} not found in the dictionary.");
            }
        }

        public void UnequipOutfit(GameItemSO outfit)
        {
            string outfitName = outfit.itemName;
            if (outfitDict.ContainsKey(outfitName))
            {
                outfitDict[outfitName].SetActive(false);
            }
            else
            {
                Debug.LogWarning($"Weapon {outfitName} not found in the dictionary.");
            }
        }
        private void OnDestroy()
        {
            UnequiptAllOutfit();
        }

        private void UnequiptAllOutfit()
        {
            foreach (var weapon in outfitDict)
            {
                weapon.Value.SetActive(false);
            }
        }
    }
}
