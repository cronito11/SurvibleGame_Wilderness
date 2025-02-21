using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Initializer {
    public abstract class InitializerBase<T> : MonoBehaviour 
    {
        [SerializeField] private bool initializeOnStart;
        
        protected T manager;
        private bool initialized = false;

        virtual protected void Awake () {
           
        }

        virtual protected void Start () 
        {
            if (!initializeOnStart)
                return;
            Initialize();
        }

        public void Initialize ()
        {
            if (initialized)
                return;

            initialized = true;
            InitializeList(FindELements());
        }

        abstract protected IEnumerable<T> FindELements ();

        public abstract void InitializeList (IEnumerable<T> list);
    }
}
