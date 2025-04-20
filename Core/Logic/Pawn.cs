using Core.Pieces;

namespace Core.Logic;

public class Pawn : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];
        
        int direction = piece.Color == PieceColor.White ? 1 : -1;

        if (startCol == endCol)
        {
            if ((startRow == 1 && piece.Color == PieceColor.White || startRow == 6 && piece.Color == PieceColor.Black) &&
                endRow == startRow + 2 * direction && board[endRow, endCol] == null)
                return true;
            
            if (endRow == startRow + direction && board[endRow, endCol] == null)
                return true;
        }

        if (Math.Abs(startCol - endCol) == 1 && endRow == startRow + direction)
        {
            Piece target = board[endRow, endCol];
            if (target != null && target.Color != piece.Color)
                return true;
        }
        return false;
    }
}