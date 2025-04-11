using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "Material Item", menuName = "Scriptable Objects/Items/Material")]
    public class MaterialItemSO : GameItemSO
    {
        public MaterialType materialType;
        //material attributes
        public override void UseItem ()
        {

        }
    }
}
