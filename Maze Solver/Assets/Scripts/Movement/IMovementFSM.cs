
public interface IMovementFSM
{
    void Tick();
    void Init(IMovementState newState);
    void ChangeState(IMovementState newState);
}
