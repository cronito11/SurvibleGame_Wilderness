using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private Drops drop;
    [Min(0)]
    [SerializeField] private int amount =1;
    [SerializeField] private GameItemSO item;


    private IDestructible destructible;

    private void Awake ()
    {
        destructible = GetComponentInParent<IDestructible>();
    }

    private void OnEnable ()
    {
        destructible.OnDestroyed += OnDestroyed;
    }

    private void OnDisable ()
    {
        destructible.OnDestroyed -= OnDestroyed;
    }

    private void OnDestroyed ()
    {
        InteractableObject.OnItemPickedUp?.Invoke(item, amount);
    }
}
