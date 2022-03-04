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

    // In case you want to implement a reply feature in the future or somethn I dunno
    //public abstract void Undo();

    public float GetTime()
    {
        return time;
    }


}
