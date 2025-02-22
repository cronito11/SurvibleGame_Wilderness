using UnityEngine;

public class DropperManager : MonoBehaviour
{
    [SerializeField] private Drops drop;
    [Min(0)]
    [SerializeField] private int amount =1;

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
        Debug.Log($"Dropping {amount} of {drop}");
    }
}
