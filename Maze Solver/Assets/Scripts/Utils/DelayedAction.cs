using System;

public class DelayedAction
{
    public bool IsRunning => _timer.IsRunning;

    private Timer _timer;
    private Action _action;

    public DelayedAction(Action action, float delay)
    {
        _action = action;
        _timer = new Timer(delay);
        _timer.OnDone += _timer_OnDone;
    }

    public void Tick(float deltaTime)
    {
        if (IsRunning)
        {
            _timer.Tick(deltaTime);
        }
    }

    public void Delay()
    {
        _timer.Reset();
        _timer.Start();
    }

    public void Cancel()
    {
        _timer.Reset();
    }

    private void _timer_OnDone()
    {
        _action?.Invoke();
    }
}
