using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TetrisBoard
{

    int[,] boardData;
    TetrisCell[,] tetrisCells;

    int row;
    public int Row
    {
        get
        {
            return row;
        }
    }

    int col;
    public int Col
    {
        get
        {
            return col;
        }
    }

    public delegate void OnCellCleared(int row, int col);
    public OnCellCleared onCellCleared;

    public enum ObstructionDirection
    {
        LEFT,
        RIGHT,
        DOWN
    }

    public TetrisBoard(int row, int col)
    {
        this.row = row;
        this.col = col;

        InitBoard();
    }

    void InitBoard()
    {
        boardData = new int[row, col];
        for (int itRow = 0; itRow < row; itRow++)
        {
            for (int itCol = 0; itCol < col; itCol++)
            {
                boardData[itRow, itCol] = 0;
            }
        }

        tetrisCells = new TetrisCell[row, col];
    }

    public void AttachPiece(TetrisPiece piece)
    {
        if (IsInside(piece))
        {
            for (int itRow = 0; itRow < piece.size.row; itRow++)
            {
                for (int itCol = 0; itCol < piece.size.col; itCol++)
                {
                    if (piece.Get(itRow, itCol) == 1)
                    {
                        boardData[piece.position.row + itRow, piece.position.col + itCol] = piece.Get(itRow, itCol);
                        tetrisCells[piece.position.row + itRow, piece.position.col + itCol] = piece.GetCell(itRow, itCol);
                    }
                }
            }
        }
    }

    public bool IsInside(TetrisPiece piece)
    {
        for (int itRow = 0; itRow < piece.size.row; itRow++)
        {
            for (int itCol = 0; itCol < piece.size.col; itCol++)
            {
                if (piece.Get(itRow, itCol) == 1)
                {
                    if (piece.position.row + itRow >= 0 && piece.position.row + itRow < row &&
                        piece.position.col + itCol >= 0 && piece.position.col < col)
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool IsObstructed(TetrisPiece piece, ObstructionDirection direction)
    {
        bool isObstructed = false;

        int yCheck = 0;
        int xCheck = 0;
        switch (direction)
        {
            case ObstructionDirection.DOWN:
                {
                    yCheck = 1;
                    xCheck = 0;
                }
                break;
            case ObstructionDirection.LEFT:
                {
                    yCheck = 0;
                    xCheck = -1;
                }
                break;
            case ObstructionDirection.RIGHT:
                {
                    yCheck = 0;
                    xCheck = 1;
                }
                break;
        }

        for (int itRow = piece.size.row - 1; itRow >= 0; itRow--)
        {
            for (int itCol = 0; itCol < piece.size.col; itCol++)
            {
                // check block is filled
                if (piece.Get(itRow, itCol) == 1)
                {
                    int rowPos = (piece.position.row + itRow) + yCheck;
                    int colPos = (piece.position.col + itCol) + xCheck;

                    if (rowPos >= 0 && rowPos < this.row &&
                        colPos >= 0 && colPos < this.col)
                    {
                        if (boardData[rowPos, colPos] == 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // hit bottom
                        return true;
                    }
                }
            }
        }

        return isObstructed;

    }

    public int Get(int row, int col)
    {
        return boardData[row, col];
    }

    public void CheckRowClear()
    {
        bool clearedRow = false;
        for (int itRow = this.row - 1; itRow >= 0; itRow--)
        {
            if (isRowFull(itRow))
            {
                ClearRow(itRow);
                clearedRow = true;
            }
        }

        if (clearedRow)
        {
            DropClearedRow();
        }
    }

    bool isRowFull(int row)
    {
        for (int itCol = 0; itCol < this.col; itCol++)
        {
            if (Get(row, itCol) == 0)
            {
                return false;
            }
        }
        return true;
    }

    bool isRowClear(int row)
    {
        for (int itCol = 0; itCol < this.col; itCol++)
        {
            if (Get(row, itCol) == 1)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearRow(int clearedRow)
    {
        for (int itCol = 0; itCol < this.col; itCol++)
        {
            boardData[clearedRow, itCol] = 0;

            //TODO: Put destroying functionality in other class?
            if (tetrisCells[clearedRow, itCol] != null)
            {
                GameObject.Destroy(tetrisCells[clearedRow, itCol].gameObject);
            }

            if (onCellCleared != null)
            {
                onCellCleared(row, itCol);
            }
        }
    }

    void DropClearedRow()
    {

        for (int currentRow = this.row - 1; currentRow > 0; currentRow--)
        {

            if (isRowClear(currentRow))
            {
                int unclearedRow = currentRow - 1;

                // search non cleared row on top
                bool foundUnclearedRow = false;
                while (!foundUnclearedRow && unclearedRow > 0)
                {
                    if (!isRowClear(unclearedRow))
                    {
                        foundUnclearedRow = true;
                    }
                    else
                    {
                        unclearedRow--;
                    }
                }

                int distanceToUnclearedRow = currentRow - unclearedRow;

                if (foundUnclearedRow)
                {
                    for (int itRow = currentRow; itRow > 0; itRow--)
                    {
                        for (int itCol = 0; itCol < this.col; itCol++)
                        {

                            int swapRowIndex = itRow - distanceToUnclearedRow;

                            if (swapRowIndex >= 0 && swapRowIndex < this.row)
                            {
                                // replace with its top
                                boardData[itRow, itCol] = boardData[itRow - distanceToUnclearedRow, itCol];
                                boardData[itRow - distanceToUnclearedRow, itCol] = 0;


                                //TODO: put cell view updates in other class?
                                if (itRow - distanceToUnclearedRow < this.tetrisCells.GetLength(0) && itRow - distanceToUnclearedRow >= 0)
                                {
                                    TetrisCell cell = this.tetrisCells[itRow - distanceToUnclearedRow, itCol];
                                    if (cell != null)
                                    {
                                        cell.transform.localPosition = new Vector3(itCol * cell.objectSize.x, -((itRow) * cell.objectSize.y), 0);
                                    }
                                    tetrisCells[itRow, itCol] = tetrisCells[itRow - distanceToUnclearedRow, itCol];
                                    tetrisCells[itRow - distanceToUnclearedRow, itCol] = null;
                                }
                            }
                        }
                    }

                    //TODO: Move remaining most top because it wasn't moved as its distanceToUnclearedRow is more than board size
                }
            }
        }
    }

    public string GetString()
    {
        string pieceStr = "";
        for (int itRow = 0; itRow < row; itRow++)
        {
            for (int itCol = 0; itCol < col; itCol++)
            {
                pieceStr += boardData[itRow, itCol].ToString();
            }

            if (itRow < row - 1)
            {
                pieceStr += "\n";
            }
        }
        return pieceStr;
    }
}

