using Core.Pieces;

namespace Core.Logic;

public class Queen : IMoveValidator
{
    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Piece[,] board)
    {
        Piece piece = board[startRow, startCol];
        
        if (startRow == endRow && startCol == endCol)
            return new Rook().IsValidMove(startRow, startCol, endRow, endCol, board);
        else if (Math.Abs(startRow - endRow) == Math.Abs(startCol - endCol))
            return new Bishop().IsValidMove(startRow, startCol, endRow, endCol, board);
        
        return false;
    }
}