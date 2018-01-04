using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TetrisPiece
{
    public enum PieceType
    {
        L,
        J,
        I,
        T,
        O,
        S,
        Z
    }

    public enum Rotation
    {
        CW,
        CCW
    }

    public BoardPos Position;

    public int[,] currentBlockPlacements;

    public TetrisPiece()
    {
    }

    public void Rotate(Rotation rotation)
    {

        int rowLength = currentBlockPlacements.GetLength(0);
        int colLength = currentBlockPlacements.GetLength(1);

        int[,] tmpBlock = new int[rowLength, colLength];

        if (rotation == Rotation.CW)
        {
            int targetCol = colLength - 1;
            for (int originRow = 0; originRow < rowLength; originRow++)
            {
                int targetRow = 0;
                for (int originCol = 0; originCol < colLength; originCol++)
                {
                    tmpBlock[targetRow, targetCol] = currentBlockPlacements[originRow, originCol];
                    targetRow++;
                }
                targetCol--;
            }
        }
        else
        {
            int targetRow = 0;
            for (int originCol = colLength - 1; originCol >= 0; originCol--)
            {
                int targetCol = 0;
                for (int originRow = 0; originRow < rowLength; originRow++)
                {
                    tmpBlock[targetRow, targetCol] = currentBlockPlacements[originRow, originCol];
                    targetCol++;
                }
                targetRow++;
            }
        }

        currentBlockPlacements = tmpBlock;
    }

    public string GetString()
    {
        string pieceStr = "";
        for (int i = 0; i < currentBlockPlacements.GetLength(0); i++)
        {
            for (int j = 0; j < currentBlockPlacements.GetLength(1); j++)
            {
                pieceStr += currentBlockPlacements[i, j].ToString();
            }
            if (i < currentBlockPlacements.GetLength(0) - 1)
                pieceStr += "\n";
        }

        return pieceStr;
    }

}