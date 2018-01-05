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
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 0, 0, 0, 0 },
                    { 1, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 }
                });
                break;

            case TetrisPiece.PieceType.J:
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 1, 0, 0},
                    { 1, 1, 1},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.L:
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 0, 0, 1},
                    { 1, 1, 1},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.O:
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 1, 1},
                    { 1, 1},
                });
                break;

            case TetrisPiece.PieceType.S:
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 0, 1, 1},
                    { 1, 1, 0},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.T:
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 0, 1, 0},
                    { 1, 1, 1},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.Z:
                tetrisPiece.SetBlockPlacements(
                new int[,] {
                    { 1, 1, 0},
                    { 0, 1, 1},
                    { 0, 0, 0}
                });
                break;

        }

        return tetrisPiece;

    }

}
