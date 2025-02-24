using UnityEngine;

namespace Surviblewilderness
{
    public class AttackSoundManager : MonoBehaviour
    {
        [SerializeField] private IAttacker attacker;

        private void Awake()
        {
            attacker = GetComponentInParent<IAttacker>();
        }

        private void OnEnable()
        {
            //attacker.Onatt += OnDamageRecieved;
        }

        private void OnDisable()
        {
            //attacker.OnDamageRecieved -= OnDamageRecieved;
        }

        private void OnAttack(int _)
        {
            //reproduce sound of everithin that you want
        }

    }
}
