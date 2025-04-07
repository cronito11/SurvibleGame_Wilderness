using Surviblewilderness;
using UnityEngine;

public class DropperPassiveAnimalManager : MonoBehaviour
{
    [SerializeField] private Drops drop;
    [Min(0)]
    [SerializeField] private int amount =1;
    [SerializeField] private GameItemSO item;
     private PassiveAnimalManager passiveAnimal;

    private IDamageable destructible;

    private void Awake ()
    {
        destructible = GetComponentInParent<IDamageable>(true);
        passiveAnimal = GetComponentInParent<PassiveAnimalManager>(true);
    }

    private void OnEnable ()
    {
        destructible.OnDamageRecieved += OnDamageRecieved;
    }

    private void OnDisable ()
    {
        destructible.OnDamageRecieved -= OnDamageRecieved;
    }

    private void OnDamageRecieved (int _)
    {
        if (destructible.health != 0)
            return;
        InteractableObject.OnItemPickedUp?.Invoke(item, amount);
    }
}
