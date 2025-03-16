using UnityEngine;
using DG.Tweening;

namespace Surviblewilderness
{
    public class AutoScaleHeal : MonoBehaviour
    {
        [SerializeField] private float _scaleDuration = 0.5f;

        private IDestructible destructible;
        private IHealable healable;
        private bool _isDestroyed = false;

        private void Awake ()
        {
            destructible = GetComponentInParent<IDestructible>();
            healable = GetComponentInParent<IHealable>();
            _isDestroyed = false;
        }

        private void OnEnable ()
        {
            destructible.OnDestroyed += OnDestroyed;
            healable.OnHealed += OnHealed;
        }

        private void OnDisable ()
        {
            destructible.OnDestroyed -= OnDestroyed;
            healable.OnHealed -= OnHealed;
        }

        private void OnDestroyed ()
        {
            _isDestroyed = true;
        }
        private void OnHealed (int amount)
        {
            if (!_isDestroyed)
                return;
            _isDestroyed = false;
            transform.DOScale(Vector3.one, _scaleDuration);
        }
    }
}
