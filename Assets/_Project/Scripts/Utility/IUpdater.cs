using System;
namespace Utility {
    public interface IUpdater {
        void RegisterUpdate (object obj, int idx, Action<float> method);
        void UnregisterUpdate (object obj);
    }

    public interface IFixedUpdater {
        void RegisterFixedUpdate (object obj, int idx, Action<float> method);
        void UnregisterFixUpdate (object obj);
    }
}