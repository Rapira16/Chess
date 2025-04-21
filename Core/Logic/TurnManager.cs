namespace Core.Logic;
using Core.Pieces;

public class TurnManager
{
    public PieceColor CurrentTurn { get; private set; } = PieceColor.White;

    public bool IsValidTurn(Piece piece) => 
        piece.Color == CurrentTurn;

    public void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}