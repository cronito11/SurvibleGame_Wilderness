using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public static Action<GameItemSO, int> OnItemPickedUp;
    public GameItemSO item;  // This should be assigned in the inspector
    public int amount = 1; // This should be assigned in the inspector


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has collided with " + gameObject.name);
            OnItemPickedUp?.Invoke(item, amount);
            this.gameObject.SetActive(false);  // Deactivate the object after it has been picked up 
            //trigger qeuipt item event from inventory manager  
        }
    }
    

}
