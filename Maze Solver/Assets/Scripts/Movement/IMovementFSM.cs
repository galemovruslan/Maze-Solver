
using System;

public interface IMovementFSM
{
    event Action<string> OnStateChange;
    void Tick();
    void Init(IMovementState newState);
    void ChangeState(IMovementState newState);
}
