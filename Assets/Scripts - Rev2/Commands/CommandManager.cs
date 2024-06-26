using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }    

    public static CommandManager Instance {get; private set;}

    private Stack<ICommand> m_CommandsBuffer = new();

    private void Awake() {
        Instance = this;
    }

    public void AddCommand(ICommand command) {
        command.Execute();
        m_CommandsBuffer.Push(command);
    }

    public void Undo() {
        if(m_CommandsBuffer.Count == 0) {
            return;
        }

        var cmd = m_CommandsBuffer.Pop();
        cmd.Undo();
    }
}