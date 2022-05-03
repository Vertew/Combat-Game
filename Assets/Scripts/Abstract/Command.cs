using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{

    protected IEntity entity;
    protected float time;

    public Command(IEntity _entity, float _time)
    {

        entity = _entity;
        time = _time;

    }

    public abstract void Execute();

    public float GetTime()
    {
        return time;
    }
}

