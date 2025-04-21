namespace Core.Game;
using Core.Pieces;
using Core.Board;
using Core.Logic;

public class Game
{
    private Board _board;
    private GameStateValidator _gameStateValidator;
    private CheckValidator _checkValidator;
    private PieceColor _playerColor;

    public Game()
    {
        _board = new Board();
        _gameStateValidator = new GameStateValidator(_board);
        _checkValidator = new CheckValidator(_board);
        _playerColor = PieceColor.White;
    }

    public void Start()
    {
        _board.InitializeBoard();
        while (true)
        {
            _board.DisplayBoard();
            Console.WriteLine($"{_playerColor}'s turn");
            
            Console.Write("Enter start row (0-7): ");
            int startRow = int.Parse(Console.ReadLine()!);
            Console.Write("Enter start col (0-7): ");
            int startCol = int.Parse(Console.ReadLine()!);
            Console.Write("Enter end row (0-7): ");
            int endRow = int.Parse(Console.ReadLine()!);
            Console.Write("Enter end col (0-7): ");
            int endCol = int.Parse(Console.ReadLine()!);
            
            bool moveSuccessful = _board.MovePiece(startRow, startCol, endRow, endCol);
            if (moveSuccessful)
            {
                if (_checkValidator.IsKingInCheck(_playerColor, _board.GetBoard()))
                {
                    Console.WriteLine($"{_playerColor} is in check!");
                }

                if (_gameStateValidator.IsCheckmate(_playerColor, _board.GetBoard()))
                {
                    Console.WriteLine($"{_playerColor} is checkmate!");
                    break;
                }

                if (_gameStateValidator.IsStalemate(_playerColor, _board.GetBoard()))
                {
                    Console.WriteLine($"{_playerColor} is stalemate!");
                    break;
                }
                
                _playerColor = _playerColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
            }

            else
            {
                Console.WriteLine($"{_playerColor}. No such move!");
            }
        }
    }
}