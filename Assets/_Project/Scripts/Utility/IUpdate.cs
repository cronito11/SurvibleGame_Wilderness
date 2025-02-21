namespace Utility {
    public interface IUpdate {
        int idx { get;}
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdate {
        int idx { get; }
        void OnFixUpdate (float deltaTime);
    }
}