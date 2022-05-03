using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{

    private List<Command> commands = new List<Command>();
    private int currentCommandIndex = -1;

    public void ExecuteCommand(Command command)
    {
        commands.Add(command);
        command.Execute();
        currentCommandIndex = commands.Count - 1;
    }
}

