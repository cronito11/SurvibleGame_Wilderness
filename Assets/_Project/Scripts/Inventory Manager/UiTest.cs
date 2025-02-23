using Unity.VisualScripting;
using UnityEngine;

public class UiTest : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUi;

    [SerializeField] private GameObject foodItemsInventory;
    [SerializeField] private GameObject cothingItemsInventory;
    [SerializeField] private GameObject weaponItemsInventory;
    [SerializeField] private GameObject materialItemsInventory;
    [SerializeField] private GameObject characterStates;
    [SerializeField] private GameObject craftingPannel;

    private bool isInventoryOpen = false;   

    public void OnInventoryClick()
    {
        //Debug.Log("Inventory clicked");
        
        inventoryUi.SetActive(!isInventoryOpen);
       
        isInventoryOpen = !isInventoryOpen;
    }

    public void OnClothinhOptionClicked()
    {
        Debug.Log("Clothing option clicked");
        cothingItemsInventory.SetActive(true);
        foodItemsInventory.SetActive(false);
        weaponItemsInventory.SetActive(false);
        materialItemsInventory.SetActive(false);
        characterStates.SetActive(true);
        craftingPannel.SetActive(false);

    }

    public void OnWeaponOptionClicked()
    {
        Debug.Log("Weapon option clicked");
        cothingItemsInventory.SetActive(false);
        foodItemsInventory.SetActive(false);
        weaponItemsInventory.SetActive(true);
        materialItemsInventory.SetActive(false);
        characterStates.SetActive(true);
        craftingPannel.SetActive(false);    
    }

    public void OnMaterialOptionClicked() 
    { 
        Debug.Log("Material option clicked");
        cothingItemsInventory.SetActive(false);
        foodItemsInventory.SetActive(false);
        weaponItemsInventory.SetActive(false);
        materialItemsInventory.SetActive(true);
        characterStates.SetActive(false);
        craftingPannel.SetActive(true);
    }

    public void OnFoodOptionClicked()
    {
        Debug.Log("Food option clicked");
        cothingItemsInventory.SetActive(false);
        foodItemsInventory.SetActive(true);
        weaponItemsInventory.SetActive(false);
        materialItemsInventory.SetActive(false);
        characterStates.SetActive(true);
        craftingPannel.SetActive(false);
    }
}
