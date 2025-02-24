using UnityEngine;

namespace Surviblewilderness
{
    public class PlayerAttackController : MonoBehaviour, IAttacker
    {

        [SerializeField] private LayerMask hitMask;
        [SerializeField] private InputReader input;
        [SerializeField] private int attackDistance = 10;
        [SerializeField] private int atttackAmount = 10;


        private void OnEnable ()
        {
            input.Attack += Attack;
        }

        private void OnDisable ()
        {
            input.Attack -= Attack;
        }

        private void Update ()
        {
            Vector3 origin = transform.position;

            Vector3 direction = transform.forward;

           
            Debug.DrawRay(origin, direction * attackDistance, Color.red);
        }
        public void Attack ()
        {
            Vector3 origin = transform.position;

            Vector3 direction = transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit, attackDistance, hitMask))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.ApplyDamage(atttackAmount);
                }
                Debug.Log($"{hit.collider.gameObject}", hit.collider.gameObject);
            }

            Debug.DrawRay(origin, direction * attackDistance, Color.red);
        }
    }
}
