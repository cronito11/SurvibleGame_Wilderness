using Initializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace Manager {
    public class UpdateManagerInitializer : InitializerBase<IUpdate> {
        private IUpdater updateManager;

        override protected void Awake () 
        {
            base.Awake();
            updateManager = GetComponentInChildren<IUpdater>();
        }

        protected override IEnumerable<IUpdate> FindELements () {
            var allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return allMonoBehaviours.OfType<IUpdate>();
        }

        public override void InitializeList (IEnumerable<IUpdate> list) 
        {
            //Iterate list
            foreach (IUpdate element in list) {
                updateManager.RegisterUpdate(element, element.idx, element.OnUpdate);
            }
        }
    }
}
