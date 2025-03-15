using UnityEngine;
using DG.Tweening;

namespace Surviblewilderness
{
    
    public class AutoHealProps : MonoBehaviour
    {
        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private float _healTime = 120;
        [SerializeField] private int _healAmount =100;

        private float _healTimer;
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
            _healTimer = _healTime;
            _isDestroyed = true;
        }

        private void Update ()
        {
            if (!_isDestroyed)
                return;

            if (_healTimer > 0)
            {
                _healTimer -= Time.deltaTime;
                if (_healTimer <= 0)
                {
                    _isDestroyed = false;
                    healable.Heal(_healAmount);
                }
            }
        }

        private void OnHealed (int amount)
        {
            if (_isDestroyed)
            {
                _isDestroyed = false;
                transform.DOScale(Vector3.one, _scaleDuration);
            }
        }
    }
}
