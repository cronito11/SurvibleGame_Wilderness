using UnityEngine;

namespace Surviblewilderness
{
    public class GameplayerGameOverControlleer : MonoBehaviour
    {
        [SerializeField] private IDamageable damageable;

        [SerializeField] private GameOverMenuUiManager gameOverMenuUi;

        private void Awake ()
        {
            damageable = GetComponent<IDamageable>();
        }

        private void OnEnable ()
        {
            if (damageable != null)
                damageable.OnDamageRecieved += OnDamageRecieved;

        }

        private void OnDisable ()
        {
            if (damageable != null)
                damageable.OnDamageRecieved -= OnDamageRecieved;
        }

        private void OnDamageRecieved (int _)
        {
            if (damageable.health > 0)
                return;
            gameOverMenuUi.Show();
        }
    }
}
