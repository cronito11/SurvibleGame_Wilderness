using UnityEngine;
using DG.Tweening;

namespace Surviblewilderness
{
    public class DestroyByScale : MonoBehaviour
    {
        [SerializeField] private float _scaleDuration = 0.5f;

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
            transform.DOScale(Vector3.zero, _scaleDuration).OnComplete(OnCompleteDestroy);
        }

        private void OnCompleteDestroy ()
        { 
                
        }
    }

}
