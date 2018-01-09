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
    public Vector3 blockSize = new Vector3(0.4482649f, 0.4482649f, 0.4482649f);

    // TODO: Put to installer for spawner
    public GameObject TetrisBlockPrefab;

    public TextMeshPro DebugTextPrefab;
    TextMeshPro[,] debugData;

    TetrisPiece currentFallingPiece;
    TetrisPieceFactory tetrisPieceFactory;
    TetrisBoard tetrisBoard;

    [Inject]
    void Init(TetrisPieceFactory tetrisPieceFactory)
    {
        this.tetrisPieceFactory = tetrisPieceFactory;

        tetrisBoard = new TetrisBoard(row, col);
        tetrisBoard.onCellCleared += TetrisBoard_OnCellCleared;

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
                if (currentFallingPiece != null)
                {
                    if (!tetrisBoard.IsObstructed(currentFallingPiece, TetrisBoard.ObstructionDirection.LEFT))
                    {
                        currentFallingPiece.MoveObj(0, -1);
                        currentFallingPiece.RefreshObj();
                    }
                }
            });

        var rightKey = inputInterval
            .Where(_ => Input.GetKeyDown(KeyCode.RightArrow))
            .Subscribe(x =>
            {
                if (currentFallingPiece != null)
                {
                    if (!tetrisBoard.IsObstructed(currentFallingPiece, TetrisBoard.ObstructionDirection.RIGHT))
                    {
                        currentFallingPiece.MoveObj(0, 1);
                        currentFallingPiece.RefreshObj();
                    }
                }
            });

        var upKey = inputInterval
            .Where(_ => Input.GetKeyDown(KeyCode.UpArrow))
            .Subscribe(x =>
            {
                if (currentFallingPiece != null)
                {
                    currentFallingPiece.Rotate(TetrisPiece.Rotation.CW);
                    currentFallingPiece.RefreshObj();
                }
            });

        StartCoroutine(UpdateFallingPiece());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //TetrisPiece tetrisPiece = this.tetrisPieceFactory.Generate((TetrisPiece.PieceType)Random.Range(0, (int)TetrisPiece.PieceType.LENGTH));
            TetrisPiece tetrisPiece = this.tetrisPieceFactory.Generate(TetrisPiece.PieceType.I);
            tetrisPiece.position.row = 0;
            tetrisPiece.position.col = 3;

            RenderPiece(tetrisPiece);

            currentFallingPiece = tetrisPiece;
        }
        UpdateDebug();
    }

    //TODO: Refactor to factory
    void RenderPiece(TetrisPiece piece)
    {
        for (int i = 0; i < piece.size.row; i++)
        {
            for (int j = 0; j < piece.size.col; j++)
            {
                if (piece.GetPiece(i, j) == 1)
                {
                    GameObject block = Instantiate(TetrisBlockPrefab, this.transform);
                    BoxCollider boxCollider = block.GetComponent<BoxCollider>();

                    TetrisCell tetrisCell = block.AddComponent<TetrisCell>();
                    tetrisCell.objectSize = new Vector3(boxCollider.size.x * block.transform.localScale.x, boxCollider.size.y * block.transform.localScale.y);

                    piece.objectSize = new Vector3(boxCollider.size.x * block.transform.localScale.x, boxCollider.size.y * block.transform.localScale.y);
                    piece.SetCell(i, j, tetrisCell);
                }
            }
        }

        piece.RefreshObj();
    }

    Vector2 GetPosition(int row, int col)
    {
        Vector3 desiredPosition = new Vector2(col * blockSize.x, -row * blockSize.y);
        return desiredPosition;
    }

    IEnumerator UpdateFallingPiece()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (currentFallingPiece != null)
            {
                if (!tetrisBoard.IsObstructed(currentFallingPiece, TetrisBoard.ObstructionDirection.DOWN))
                {
                    // move object
                    currentFallingPiece.MoveObj(1, 0);
                    currentFallingPiece.RefreshObj();
                }
                else if (tetrisBoard.IsObstructed(currentFallingPiece, TetrisBoard.ObstructionDirection.DOWN))
                {
                    tetrisBoard.AttachPiece(currentFallingPiece);
                    tetrisBoard.CheckRowClear();

                    currentFallingPiece = null;
                }
            }
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
}
