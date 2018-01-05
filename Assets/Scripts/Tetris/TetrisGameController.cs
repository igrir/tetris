using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TetrisGameController : MonoBehaviour
{

    public int row = 20;
    public int col = 10;

    public Vector3 blockSize = new Vector3(0.4482649f, 0.4482649f, 0.4482649f);

    // TODO: Put to installer for spawner
    public GameObject TetrisBlockPrefab;

    TetrisPiece currentFallingPiece;

    TetrisPieceFactory tetrisPieceFactory;

    [Inject]
    void Init(TetrisPieceFactory tetrisPieceFactory)
    {
        this.tetrisPieceFactory = tetrisPieceFactory;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TetrisPiece tetrisPiece = this.tetrisPieceFactory.Generate((TetrisPiece.PieceType)Random.Range(0, (int)TetrisPiece.PieceType.LENGTH));
            RenderPiece(tetrisPiece);

            currentFallingPiece = tetrisPiece;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentFallingPiece.MoveObj(0, -1);
            currentFallingPiece.RefreshObj();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentFallingPiece.MoveObj(0, 1);
            currentFallingPiece.RefreshObj();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentFallingPiece.MoveObj(-1, 0);
            currentFallingPiece.RefreshObj();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentFallingPiece.Rotate(TetrisPiece.Rotation.CW);
            currentFallingPiece.RefreshObj();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(currentFallingPiece.GetString());
        }
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

                    piece.objectSize = new Vector3(boxCollider.size.x * block.transform.localScale.x, boxCollider.size.y * block.transform.localScale.y);
                    piece.SetBlockObject(i, j, block);
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
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }
}
