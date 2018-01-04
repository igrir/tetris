using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPieceFactory
{
    public TetrisPieceFactory()
    {
    }

    public TetrisPiece Generate(TetrisPiece.PieceType type)
    {

        TetrisPiece tetrisPiece = new TetrisPiece();

        switch (type)
        {
            case TetrisPiece.PieceType.I:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 0, 0, 0, 0 },
                    { 1, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 }
                };
                break;

            case TetrisPiece.PieceType.J:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 1, 0, 0},
                    { 1, 1, 1},
                    { 0, 0, 0}
                };
                break;

            case TetrisPiece.PieceType.L:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 0, 0, 1},
                    { 1, 1, 1},
                    { 0, 0, 0}
                };
                break;

            case TetrisPiece.PieceType.O:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 1, 1},
                    { 1, 1},
                };
                break;

            case TetrisPiece.PieceType.S:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 0, 1, 1},
                    { 1, 1, 0},
                    { 0, 0, 0}
                };
                break;

            case TetrisPiece.PieceType.T:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 0, 1, 0},
                    { 1, 1, 1},
                    { 0, 0, 0}
                };
                break;

            case TetrisPiece.PieceType.Z:
                tetrisPiece.currentBlockPlacements = new int[,] {
                    { 1, 1, 0},
                    { 0, 1, 1},
                    { 0, 0, 0}
                };
                break;

        }

        return tetrisPiece;

    }

}
