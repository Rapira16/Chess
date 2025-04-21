namespace Core.Logic;
using Core.Pieces;

public class MoveHistory
{
    private Stack<Move> _history = new();

    public void RecordMove(int startRow, int startColumn, int endRow, int endColumn, Piece? captured)
    {
        _history.Push(new Move(startRow, startColumn, endRow, endColumn, captured));
    }

    public Move? UndoLastMove(Piece?[,] board)
    {
        if (_history.Count == 0)
            return null;

        var last = _history.Pop();
        
        board[last.StartRow, last.StartColumn] = board[last.EndRow, last.EndColumn];
        board[last.StartRow, last.StartColumn]?.MarkMoved();
        board[last.EndRow, last.EndColumn] = last.CapturedPiece;
        
        return last;
    }
    
    public IReadOnlyList<Move> GetMoves() => _history.Reverse().ToList();
}

public class Move(int startRow, int startColumn, int endRow, int endColumn, Piece? captured)
{
    public int StartRow { get; } = startRow;
    public int StartColumn { get; } = startColumn;
    public int EndRow { get; } = endRow;
    public int EndColumn { get; } = endColumn;
    public Piece? CapturedPiece { get; } = captured;
}