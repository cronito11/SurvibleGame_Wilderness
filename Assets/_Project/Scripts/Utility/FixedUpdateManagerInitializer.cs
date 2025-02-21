using Initializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace Manager {
    public class FixedUpdateManagerInitializer : InitializerBase<IFixedUpdate> {//Change to one class
        private IFixedUpdater updateManager;

        override protected void Awake () {
            base.Awake();
            updateManager = GetComponentInChildren<IFixedUpdater>();
        }

        protected override IEnumerable<IFixedUpdate> FindELements () {
            var allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return allMonoBehaviours.OfType<IFixedUpdate>();
        }

        public override void InitializeList (IEnumerable<IFixedUpdate> list) {
            //Iterate list
            foreach (IFixedUpdate element in list) {
                updateManager.RegisterFixedUpdate(element, element.idx, element.OnFixUpdate);
            }
        }
    }
}
