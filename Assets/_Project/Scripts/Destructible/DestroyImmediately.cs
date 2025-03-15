using UnityEngine;

public class DestroyImmediately : MonoBehaviour
{
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
        Destroy((destructible as MonoBehaviour).gameObject);
    }
}