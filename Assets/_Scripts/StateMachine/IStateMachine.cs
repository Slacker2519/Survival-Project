public interface IStateMachine<T>
{
    void EnterState(T t) { }
    void UpdateState(T t) { }
    void ExitState(T t) { }
}
