using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

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
        Z,
        LENGTH
    }

    public enum Rotation
    {
        CW,
        CCW
    }

    public BoardPos position;
    public BoardPos size;
    public Vector3 objectSize;

    int[,] currentBlockPlacements;
    GameObject[,] currentBlockObject;

    public TetrisPiece()
    {
    }

    public void Rotate(Rotation rotation)
    {

        int rowLength = size.row;
        int colLength = size.col;

        int[,] placementTmpBlock = new int[rowLength, colLength];
        GameObject[,] goTmpBlock = new GameObject[rowLength, colLength];

        if (rotation == Rotation.CW)
        {
            int targetCol = colLength - 1;
            for (int originRow = 0; originRow < rowLength; originRow++)
            {
                int targetRow = 0;
                for (int originCol = 0; originCol < colLength; originCol++)
                {
                    placementTmpBlock[targetRow, targetCol] = currentBlockPlacements[originRow, originCol];
                    goTmpBlock[targetRow, targetCol] = currentBlockObject[originRow, originCol];

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
                    placementTmpBlock[targetRow, targetCol] = currentBlockPlacements[originRow, originCol];
                    goTmpBlock[targetRow, targetCol] = currentBlockObject[originRow, originCol];

                    targetCol++;
                }
                targetRow++;
            }
        }

        currentBlockPlacements = placementTmpBlock;
        currentBlockObject = goTmpBlock;
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

    public void SetBlockPlacements(int[,] placements)
    {
        this.currentBlockPlacements = placements;

        size.row = placements.GetLength(0);
        size.col = placements.GetLength(1);
        this.currentBlockObject = new GameObject[size.row, size.col];
    }

    public void SetBlockObject(int row, int col, GameObject obj)
    {
        currentBlockObject[row, col] = obj;
    }

    public int GetPiece(int row, int col)
    {
        return this.currentBlockPlacements[row, col];
    }


    /// <summary>
    /// Move the game objects
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void MoveObj(int row, int col)
    {
        this.position.row += row;
        this.position.col += col;
    }

    public void RefreshObj()
    {
        for (int itRow = 0; itRow < size.row; itRow++)
        {
            for (int itCol = 0; itCol < size.col; itCol++)
            {
                if (currentBlockObject[itRow, itCol] != null)
                    currentBlockObject[itRow, itCol].transform.localPosition = new Vector3((itCol + position.col) * objectSize.x, (itRow + position.row) * objectSize.y, 0);
            }
        }
    }
}