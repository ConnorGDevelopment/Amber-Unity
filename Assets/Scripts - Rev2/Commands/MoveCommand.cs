
using UnityEngine;

public class MoveCommand: CommandManager.ICommand {
    public Pawn2 Pawn;
    public Vector3 initialPos;
    public Vector3 finalPos;

    public MoveCommand(Pawn2 pawn, Vector3 destination) {
        Pawn = pawn;
        initialPos = pawn.gameObject.transform.position;
        finalPos = destination;
    }

    public void Execute() {
        Pawn.gameObject.transform.position = finalPos;
    }
    public void Undo() {
        Pawn.gameObject.transform.position = initialPos;
    }
}