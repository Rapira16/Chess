using Core.Pieces;

namespace Core.Logic;

public class King : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];

        if (Math.Abs(startRow - endRow) <= 1 && Math.Abs(startCol - endCol) <= 1)
        {
            Piece target = board[endRow, endCol];
            if (target == null || target.Color != piece.Color)
                return true;
        }
        
        return false;
    }
}