using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using TMPro;

public class TetrisGameController : MonoBehaviour
{

    public int row = 20;
    public int col = 10;

    // TODO: Acquire this number from prefab
    //public Vector3 blockSize = new Vector3(0.4482649f, 0.4482649f, 0.4482649f);
    public Vector3 blockSize;

    public float fallingUpdateSpeed = 0.2f;

    GameObject tetrisBlockPrefab;

    public TextMeshPro DebugTextPrefab;

    TetrisPiece currentFallingPiece;
    public TetrisPiece CurrentFallingPiece
    {
        get { return this.currentFallingPiece; }
    }

    TextMeshPro[,] debugData;

    TetrisPiece.Factory tetrisPieceFactory;
    TetrisBoard tetrisBoard;
    public bool isUpdatingFallingPiece = true;

    [Inject(Id = "IterateTestWaitTime")]
    float iterateTestWaitTime = 0.01f;

    [Inject]
    void Init(
        TetrisPiece.Factory tetrisPieceFactory,
        TetrisBoard tetrisBoard,
        [Inject(Id = "TetrisBlockPrefab")]
        GameObject tetrisBlockPrefab
        )
    {
        this.tetrisPieceFactory = tetrisPieceFactory;
        this.tetrisBoard = tetrisBoard;
        this.tetrisBlockPrefab = tetrisBlockPrefab;

        tetrisBoard.onCellCleared += TetrisBoard_OnCellCleared;

        // setup block size
        BoxCollider boxCollider = tetrisBlockPrefab.GetComponent<BoxCollider>();
        blockSize.x = tetrisBlockPrefab.transform.localScale.x * boxCollider.size.x;
        blockSize.y = tetrisBlockPrefab.transform.localScale.y * boxCollider.size.y;
        blockSize.z = tetrisBlockPrefab.transform.localScale.z * boxCollider.size.z;

        InitDebug();
    }

    private void Start()
    {
        var inputInterval = Observable
            .EveryUpdate();

        var leftKey = inputInterval
            .Where(_ => Input.GetKeyDown(KeyCode.LeftArrow))
            .Subscribe(x =>
            {
                MovePiece(0, -1);
            });

        var rightKey = inputInterval
            .Where(_ => Input.GetKeyDown(KeyCode.RightArrow))
            .Subscribe(x =>
            {
                MovePiece(0, 1);
            });

        var upKey = inputInterval
            .Where(_ => Input.GetKeyDown(KeyCode.UpArrow))
            .Subscribe(x =>
            {
                RotatePiece();
            });

        StartCoroutine(UpdateGame());
        StartCoroutine(IEUpdateFallingPiece());
    }


    IEnumerator UpdateGame()
    {
        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (this.tetrisPieceFactory != null)
                {
                    CreatePiece((TetrisPiece.PieceType)Random.Range(0, (int)TetrisPiece.PieceType.LENGTH));
                }
            }
            UpdateDebug();
        }
    }


    #region Board Action
    public void MovePiece(int row, int col)
    {
        if (currentFallingPiece != null)
        {
            bool moveLeftAllowed = col < 0 && !tetrisBoard.IsObstructed(currentFallingPiece, TetrisBoard.ObstructionDirection.LEFT);
            bool moveRightAllowed = col > 0 && !tetrisBoard.IsObstructed(currentFallingPiece, TetrisBoard.ObstructionDirection.RIGHT);
            if (moveLeftAllowed || moveRightAllowed)
            {
                currentFallingPiece.Move(row, col);
                currentFallingPiece.ApplyChange();
                currentFallingPiece.RefreshView();
            }
        }
    }

    public void RotatePiece()
    {
        if (currentFallingPiece != null)
        {
            currentFallingPiece.Rotate(TetrisPiece.Rotation.CW);
            currentFallingPiece.ApplyChange();
            currentFallingPiece.RefreshView();
        }
    }

    public void UpdateFallingPiece()
    {
        if (currentFallingPiece != null)
        {
            if (!currentFallingPiece.IsObstructed(TetrisBoard.ObstructionDirection.DOWN))
            {
                // move object
                currentFallingPiece.Move(1, 0);
                currentFallingPiece.RefreshView();
            }
            else if (currentFallingPiece.IsObstructed(TetrisBoard.ObstructionDirection.DOWN))
            {
                tetrisBoard.AttachPiece(currentFallingPiece);
                tetrisBoard.CheckRowClear();

                currentFallingPiece = null;
            }
        }
    }

    public TetrisPiece CreatePiece(TetrisPiece.PieceType type)
    {
        TetrisPiece tetrisPiece = this.tetrisPieceFactory.Create();
        tetrisPiece.Init(type);
        //TetrisPiece tetrisPiece = this.tetrisPieceFactory.Generate(TetrisPiece.PieceType.I);
        tetrisPiece.position.row = 0;
        tetrisPiece.position.col = 3;

        RenderPiece(tetrisPiece);

        currentFallingPiece = tetrisPiece;

        return tetrisPiece;
    }

    public void AttachPiece(TetrisPiece piece)
    {
        tetrisBoard.AttachPiece(piece);
    }

    public void AttachPiece(TetrisPiece piece, int row, int col)
    {
        piece.SetPos(row, col);
        piece.ApplyChange();
        piece.RefreshView();

        tetrisBoard.AttachPiece(piece);
        tetrisBoard.CheckRowClear();
    }

    public void CheckRowClear()
    {
        tetrisBoard.CheckRowClear();
    }

    #endregion

    //TODO: Refactor to factory
    void RenderPiece(TetrisPiece piece)
    {
        for (int i = 0; i < piece.size.row; i++)
        {
            for (int j = 0; j < piece.size.col; j++)
            {
                if (piece.GetPiece(i, j) == 1)
                {
                    GameObject block = Instantiate(tetrisBlockPrefab, this.transform);
                    BoxCollider boxCollider = block.GetComponent<BoxCollider>();

                    TetrisCell tetrisCell = block.AddComponent<TetrisCell>();
                    tetrisCell.objectSize = new Vector3(boxCollider.size.x * block.transform.localScale.x, boxCollider.size.y * block.transform.localScale.y);

                    piece.objectSize = new Vector3(boxCollider.size.x * block.transform.localScale.x, boxCollider.size.y * block.transform.localScale.y);
                    piece.SetCell(i, j, tetrisCell);
                }
            }
        }

        piece.RefreshView();
    }

    Vector2 GetPosition(int row, int col)
    {
        Vector3 desiredPosition = new Vector2(col * blockSize.x, -row * blockSize.y);
        return desiredPosition;
    }

    IEnumerator IEUpdateFallingPiece()
    {
        while (isUpdatingFallingPiece)
        {
            yield return new WaitForSeconds(fallingUpdateSpeed);

            UpdateFallingPiece();
        }
    }

    public void InitDebug()
    {
        Vector3 pos;
        debugData = new TextMeshPro[tetrisBoard.Row, tetrisBoard.Col];
        for (int itRow = 0; itRow < tetrisBoard.Row; itRow++)
        {
            for (int itCol = 0; itCol < tetrisBoard.Col; itCol++)
            {
                TextMeshPro debugObj = Instantiate(DebugTextPrefab, this.transform);
                pos = GetPosition(itRow, itCol);
                pos.z = -2;
                debugObj.transform.localPosition = pos;
                debugData[itRow, itCol] = debugObj;
            }
        }
    }

    public void UpdateDebug()
    {
        if (tetrisBoard == null)
            return;

        for (int itRow = 0; itRow < tetrisBoard.Row; itRow++)
        {
            for (int itCol = 0; itCol < tetrisBoard.Col; itCol++)
            {
                if (debugData != null)
                {
                    if (debugData[itRow, itCol] != null)
                    {
                        debugData[itRow, itCol].text = tetrisBoard.Get(itRow, itCol).ToString();
                    }
                }
            }
        }
    }

    #region Delegates Listener
    void TetrisBoard_OnCellCleared(int row, int coll)
    {

    }
    #endregion

    #region Debug
    public string GetBoardString()
    {
        return tetrisBoard.GetString();
    }
    #endregion


    #region Test
    /// <summary>
    /// 
    /// </summary>
    /// <param name="count">Default to max row</param>
    /// <returns></returns>
    public IEnumerator IterateFall(int count = 20)
    {
        int maxCount = (count <= 1 ? 1 : count);
        for (int i = 0; i < maxCount; i++)
        {
            UpdateFallingPiece();
            if (CurrentFallingPiece == null) { break; }
            else { yield return new WaitForSeconds(iterateTestWaitTime); }
        }
    }

    public IEnumerator IterateMoveCol(int col)
    {
        for (int i = 0; i < Mathf.Abs(col); i++)
        {
            MovePiece(0, col != 0 ? (col < 0 ? -1 : 1) : 0);
            if (CurrentFallingPiece == null) { break; }
            else { yield return new WaitForSeconds(iterateTestWaitTime); }
        }
    }

    public IEnumerator IterateRotate(int count = 1, TetrisPiece.Rotation rotation = TetrisPiece.Rotation.CW)
    {
        int maxCount = (count <= 1 ? 1 : count);
        for (int i = 0; i < maxCount; i++)
        {
            RotatePiece();
            if (CurrentFallingPiece == null) { break; }
            else { yield return new WaitForSeconds(iterateTestWaitTime); }
        }
    }
    #endregion
}
