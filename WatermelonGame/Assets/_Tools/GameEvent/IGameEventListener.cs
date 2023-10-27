namespace HNK.Tools {
    public interface IGameEventListener<T> {
        void OnEventRaised(T item);
    }
}


