namespace Core.Logic;
using Core.Pieces;
using Core.Board;

public class CheckValidator
{
    private Board _board;

    public CheckValidator(Board board)
    {
        _board = board;
    }
    
    public bool IsKingInCheck(PieceColor kingColor, Piece[,] board)
    {
        var kingPosition = FindKing(kingColor, board);
        if(kingPosition == null)
            return false;

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var attacker = board[row, col];
                if(attacker == null || attacker.Color == kingColor)
                    continue;

                var validator = _board.GetMoveValidatorForPiece(attacker);
                if (validator.IsValidMove(row, col, kingPosition.Value.Row, kingPosition.Value.Col, board))
                    return true;
            }
        }
        return false;
    }

    private (int Row, int Col)? FindKing(PieceColor kingColor, Piece[,] board)
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var piece = board[row, col];
                if (piece != null && piece.Color == kingColor && piece.Type == PieceType.King)
                    return (row, col);
            }
        }
        return null;
    }
}