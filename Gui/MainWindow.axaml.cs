using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Core.Board;
using Core.Pieces;
using System;
using System.Collections.Generic;
using Avalonia.Input;
using Core.Logic;

namespace Gui;

public partial class MainWindow : Window
{
    private readonly Core.Board.Board _board;
    private readonly Button[,] _buttons = new Button[8, 8];
    private readonly TurnManager _turnManager = new();
    private List<(int, int)> _highlightedMoves = new();
    
    private (int Row, int Column)? _selected;
    
    public MainWindow()
    {
        InitializeComponent();
        _board = new Core.Board.Board();
        InitBoardUI();
    }

    private void InitBoardUI()
    {
        BoardGrid.Children.Clear();

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var button = new Button
                {
                    Background = GetCellColor(row, col),
                    FontSize = 24,
                    Tag = (row, col)
                };

                button.Click += OnCellClicked;
                
                _buttons[row, col] = button;
                BoardGrid.Children.Add(button);
            }
        }
        RedrawBoard();
    }

    private void RedrawBoard()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var piece = _board._board[row, col];
                _buttons[row, col].Content = piece != null ? GetUnicode(piece) : "";
                _buttons[row, col].Background = GetCellColor(row, col);
            }
        }
        
        TurnLabel.Text = _turnManager.CurrentTurn == PieceColor.White ? "White" : "Black";
    }

    private void OnCellClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not ValueTuple<int, int> cell)
            return;
        
        int row = cell.Item1;
        int col = cell.Item2;

        if (_selected is null)
        {
            var selected = _board._board[row, col];
            if (selected == null || !_turnManager.IsValidTurn(selected))
                return;
            
            _selected = (row, col);
            _buttons[row, col].Background = Brushes.Yellow;
            
            _highlightedMoves = _board.GetValidMoves(row, col);
            foreach (var (r, c) in _highlightedMoves)
                _buttons[r, c].Background = Brushes.LightGreen;
        }
        else
        {
            var (fromRow, fromCol) = _selected.Value;

            if (_board.MovePiece(fromRow, fromCol, row, col))
            {
                _turnManager.SwitchTurn();
                RedrawBoard();
            }

            _buttons[fromRow, fromCol].Background = GetCellColor(fromRow, fromCol);
            _selected = null;
        }
        
        foreach(var (r, c) in _highlightedMoves)
            _buttons[r, c].Background = GetCellColor(r, c);
        
        _highlightedMoves.Clear();
    }

    private IBrush GetCellColor(int row, int col)
    {
        return (row + col) % 2 == 0 ? Brushes.Bisque : Brushes.SaddleBrown;
    }

    private string GetUnicode(Piece? piece)
    {
        return (piece.Type, piece.Color) switch
        {
            (PieceType.King, PieceColor.White) => "♔",
            (PieceType.Queen, PieceColor.White) => "♕",
            (PieceType.Rook, PieceColor.White) => "♖",
            (PieceType.Bishop, PieceColor.White) => "♗",
            (PieceType.Knight, PieceColor.White) => "♘",
            (PieceType.Pawn, PieceColor.White) => "♙",
            (PieceType.King, PieceColor.Black) => "♚",
            (PieceType.Queen, PieceColor.Black) => "♛",
            (PieceType.Rook, PieceColor.Black) => "♜",
            (PieceType.Bishop, PieceColor.Black) => "♝",
            (PieceType.Knight, PieceColor.Black) => "♞",
            (PieceType.Pawn, PieceColor.Black) => "♟",
            _ => "?"
        };
    }
}