using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler 
{
    private Timer _cooldown;
    private IAbility _ability;
    private bool _onCD;
    private float _lastUsedTime;

    public AbilityHandler(IAbility ability)
    {
        _ability = ability;
        _cooldown = new Timer(_ability.Cooldown);
        _cooldown.OnDone += _cooldown_OnDone;
    }

    public void TryUse()
    {
        if (!IsReady())
        {
            return;
        }
        _ability.Use();
        BeginCooldown();
    }

    public void Reset()
    {
        _cooldown_OnDone();
    }

    private void BeginCooldown()
    {
        _cooldown.Reset();
        _cooldown.Start();
        _onCD = true;
        _lastUsedTime = Time.time;
    }

    private bool IsReady()
    {
        return !_onCD && _ability.CheckPrerequsites();
    }
    private void _cooldown_OnDone()
    {
        _onCD = false;
    }
}
