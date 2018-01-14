using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UniRx;
using Zenject;

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
        DOT,
        LENGTH
    }


    public enum Rotation
    {
        CW,
        CCW
    }

    public BoardPos position;
    public BoardPos size;
    public BoardPos pivot;
    public Vector3 objectSize;


    private PieceType pieceType;

    int[,] currentBlockPlacements;
    TetrisCell[,] currentBlockCells;
    Rotation currentRotation;

    int[,] tmpBlockPlacements;
    TetrisCell[,] tmpBlockCells;
    Rotation tmpRotation;

    TetrisBoard tetrisBoard;


    // wall kick data from http://tetris.wikia.com/wiki/SRS
    //
    // 0 = spawn state
    // 1 = state resulting from a clockwise rotation("right") from spawn
    // 2 = state resulting from 2 successive rotations in either direction from spawn.
    // 3 = state resulting from a counter-clockwise ("left") rotation from spawn
    int currentRotationState = 0;

    int[,,,] jlstzWallKickData = new int[,,,]
    {
        // Test 1
        {
            {
                { 0, 0 },       //  0 >> 1
                { 0, 0 },       //  1 >> 2
                { 0, 0 },       //  2 >> 3
                { 0, 0 },       //  3 >> 2
            },
            {
                { 0, 0 },       //  0 >> 3
                { 0, 0 },       //  1 >> 0
                { 0, 0 },       //  2 >> 1
                { 0, 0 }        //  3 >> 2
            }
        },
        // Test 2
        {
            {
                { -1, 0 },      //  0 >> 1
                { 1, 0 },       //  1 >> 2
                { -1, 0 },      //  2 >> 1
                { -1, 0 }       //  3 >> 2
            },
            {
                { 1, 0 },       //  0 >> 3
                { 1, 0 },       //  1 >> 0
                { 1, 0 },       //  2 >> 1
                { -1, 0 }       //  3 >> 2
            }
        },
        // Test 3
        {
            {
                { -1, 1 },      //  0 >> 1
                { 1, -1 },      //  1 >> 2
                { 1, 1 },       //  2 >> 3
                { -1, -1 }      //  3 >> 0
            },
            {
                { 1, 1 },       //  0 >> 3
                { 1, -1 },      //  1 >> 0
                { -1, 1 },      //  2 >> 1
                { -1, -1 }      //  3 >> 2
            }
        },
        // Test 4
        {
            {
                { 0, -2 },      //  0 >> 1
                { 0, 2 },       //  1 >> 2
                { 0, -2 },      //  2 >> 3
                { 0, 2 }        //  3 >> 0
            },
            {
                { 0, -2 },      //  0 >> 3
                { 0, 2 },       //  1 >> 0
                { 0, -2 },      //  2 >> 1
                { 0, 2 }        //  3 >> 2
            }
        },
        // Test 5
        {
            {
                { -1, -2 },     //  0 >> 1
                { 1, 2 },       //  1 >> 2
                { 1, -2 },      //  2 >> 3
                { -1, 2 }       //  3 >> 0
            },
            {
                { 1, -2 },      //  0 >> 3
                { 1, 2 },       //  1 >> 0
                { -1, -2 },     //  2 >> 1
                { -1, 2 }       //  3 >> 2
            }
        }
    };

    int[,,,] iWallKickData = new int[,,,]
    {
        // Test 1
        {
            {
                { 0, 0 },       //  0 >> 1
                { 0, 0 },       //  1 >> 2
                { 0, 0 },       //  2 >> 3
                { 0, 0 }        //  3 >> 0
            },
            {
                { 0, 0 },       //  0 >> 3
                { 0, 0 },       //  1 >> 0
                { 0, 0 },       //  2 >> 1
                { 0, 0 }        //  3 >> 2
            }
        },
        // Test 2
        {
            {
                { -2, 0 },      //  0 >> 1
                { -1, 0 },      //  1 >> 2
                { 2, 0 },       //  2 >> 3
                { 1, 0 }        //  3 >> 0
            },
            {
                { -1, 0 },      //  0 >> 3
                { 2, 0 },       //  1 >> 0
                { 1, 0 },       //  2 >> 1
                { -2, 0 }       //  3 >> 2
            }
        },
        // Test 3
        {
            {
                { 1, 0 },       //  0 >> 1
                { 2, 0 },       //  1 >> 2
                { -1, 0 },      //  2 >> 3
                { -2, 0 }       //  3 >> 0
            },
            {
                { 2, 0 },       //  0 >> 3
                { -1, 0 },      //  1 >> 0
                { -2, 0 },      //  2 >> 1
                { 1, 0 }        //  3 >> 2
            }
        },
        // Test 4
        {
            {
                { -2, -1 },     //  0 >> 1
                { -1, 2 },      //  1 >> 2
                { 2, 1 },       //  2 >> 3
                { 1, -2 }       //  3 >> 0
            },
            {
                { -1, 2 },      //  0 >> 3
                { 2, 1 },       //  1 >> 0
                { 1, -2 },      //  2 >> 1
                { -2, -1 }      //  3 >> 2
            }
        },
        // Test 5
        {
            {
                { 1, 2 },       //  0 >> 1
                { 2, -1 },      //  1 >> 2
                { -1, -2 },     //  2 >> 3
                { -2, 1 }       //  3 >> 0
            },
            {
                { 2, -1 },      //  0 >> 3
                { -1, -2 },     //  1 >> 0
                { -2, 1 },      //  2 >> 1
                { 1, 2 }        //  3 >> 2
            }
        }

    };

    public TetrisPiece(TetrisBoard tetrisBoard)
    {
        this.tetrisBoard = tetrisBoard;
    }

    public void Init(TetrisPiece.PieceType type)
    {
        switch (type)
        {
            case TetrisPiece.PieceType.I:
                FillCell(
                new int[,] {
                    { 0, 0, 0, 0 },
                    { 1, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 }
                });
                break;

            case TetrisPiece.PieceType.J:
                FillCell(
                new int[,] {
                    { 1, 0, 0},
                    { 1, 1, 1},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.L:
                FillCell(
                new int[,] {
                    { 0, 0, 1},
                    { 1, 1, 1},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.O:
                FillCell(
                new int[,] {
                    { 1, 1},
                    { 1, 1},
                });
                break;

            case TetrisPiece.PieceType.S:
                FillCell(
                new int[,] {
                    { 0, 1, 1},
                    { 1, 1, 0},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.T:
                FillCell(
                new int[,] {
                    { 0, 1, 0},
                    { 1, 1, 1},
                    { 0, 0, 0}
                });
                break;

            case TetrisPiece.PieceType.Z:
                FillCell(
                new int[,] {
                    { 1, 1, 0},
                    { 0, 1, 1},
                    { 0, 0, 0}
                });
                break;

        }
    }



    public void Rotate(Rotation rotation)
    {
        this.tmpBlockPlacements = new int[size.row, size.col];
        this.tmpBlockCells = new TetrisCell[size.row, size.col];

        int rowLength = size.row;
        int colLength = size.col;

        if (rotation == Rotation.CW)
        {
            int targetCol = colLength - 1;
            for (int originRow = 0; originRow < rowLength; originRow++)
            {
                int targetRow = 0;
                for (int originCol = 0; originCol < colLength; originCol++)
                {
                    tmpBlockPlacements[targetRow, targetCol] = currentBlockPlacements[originRow, originCol];
                    tmpBlockCells[targetRow, targetCol] = currentBlockCells[originRow, originCol];

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
                    tmpBlockPlacements[targetRow, targetCol] = currentBlockPlacements[originRow, originCol];
                    tmpBlockCells[targetRow, targetCol] = currentBlockCells[originRow, originCol];

                    targetCol++;
                }
                targetRow++;
            }
        }

        this.tmpRotation = rotation;
    }

    public void ApplyChange()
    {
        this.currentBlockPlacements = this.tmpBlockPlacements;
        this.currentBlockCells = this.tmpBlockCells;

        UpdateRotationState(this.tmpRotation);
    }

    /// <summary>
    /// Update rotation state cyclically
    /// </summary>
    /// <param name="rotation"></param>
    void UpdateRotationState(Rotation rotation)
    {
        if (rotation == Rotation.CW)
        {
            this.currentRotationState++;
            if (this.currentRotationState > 3) { this.currentRotationState = 0; }
        }
        else if (rotation == Rotation.CCW)
        {
            this.currentRotationState--;
            if (this.currentRotationState < 0) { this.currentRotationState = 3; }
        }

        this.currentRotation = rotation;
    }

    void FillCell(int[,] placements)
    {
        this.currentBlockPlacements = placements;

        size.row = placements.GetLength(0);
        size.col = placements.GetLength(1);

        this.currentBlockCells = new TetrisCell[size.row, size.col];

        // initialize temporary
        this.tmpBlockCells = new TetrisCell[size.row, size.col];
        this.tmpBlockPlacements = placements;
    }



    public void SetCell(int row, int col, TetrisCell obj)
    {
        currentBlockCells[row, col] = obj;
        tmpBlockCells[row, col] = obj;
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
    public void Move(int row, int col)
    {
        this.position.row += row;
        this.position.col += col;
    }

    public void SetPos(int row, int col)
    {
        this.position.row = row;
        this.position.col = col;
    }

    public void RefreshView()
    {
        for (int itRow = 0; itRow < size.row; itRow++)
        {
            for (int itCol = 0; itCol < size.col; itCol++)
            {
                if (currentBlockCells[itRow, itCol] != null)
                    currentBlockCells[itRow, itCol].transform.localPosition = new Vector3((itCol + position.col) * objectSize.x, -((itRow + position.row) * objectSize.y), 0);
            }
        }
    }

    public int Get(int row, int col)
    {
        return currentBlockPlacements[row, col];
    }

    public TetrisCell GetCell(int row, int col)
    {
        return currentBlockCells[row, col];
    }

    public BoardPos GetWallKick(Rotation rotation, int testIteration)
    {
        int initialRotationState = this.currentRotationState;

        int nextRotationState = initialRotationState;
        if (rotation == Rotation.CW)
        {
            nextRotationState++;
            if (nextRotationState > 3) { nextRotationState = 0; }
        }
        else
        {
            nextRotationState--;
            if (nextRotationState < 0) { nextRotationState = 3; }
        }

        int xKick = 0;
        int yKick = 0;
        if (this.pieceType == PieceType.J || this.pieceType == PieceType.L || this.pieceType == PieceType.S ||
            this.pieceType == PieceType.T || this.pieceType == PieceType.Z)
        {
            xKick = jlstzWallKickData[testIteration, currentRotationState, nextRotationState, 0];
            yKick = jlstzWallKickData[testIteration, currentRotationState, nextRotationState, 1];

        }
        else if (this.pieceType == PieceType.I)
        {
            xKick = iWallKickData[testIteration, currentRotationState, nextRotationState, 0];
            yKick = iWallKickData[testIteration, currentRotationState, nextRotationState, 1];
        }

        return new BoardPos(yKick, xKick);
    }

    public bool IsObstructed(TetrisBoard.ObstructionDirection direction)
    {
        return this.tetrisBoard.IsObstructed(this, direction);
    }

    #region Debug
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
            {
                pieceStr += "\n";
            }
        }
        return pieceStr;
    }
    #endregion


    public class Factory : Factory<TetrisPiece>
    {
    }

}