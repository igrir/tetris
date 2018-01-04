using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGameController : MonoBehaviour
{

    public int row = 20;
    public int col = 10;

    public Vector3 blockSize = new Vector3(0.4482649f, 0.4482649f, 0.4482649f);

    // TODO: Put to installer for spawner
    public GameObject TetrisBlockPrefab;

    TetrisPiece currentFallingPiece;

    // Use this for initialization
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateBoard()
    {
        GameObject block;

        TetrisPiece jPiece = new TetrisPiece();

        for (int i = 0; i < jPiece.currentBlockPlacements.GetLength(0); i++)
        {
            block = Instantiate(TetrisBlockPrefab, this.transform);
            block.transform.localPosition = GetPosition(jPiece.currentBlockPlacements[i, 0], jPiece.currentBlockPlacements[i, 1]);
        }

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(0, 0);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(0, 1);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(0, 2);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(1, 1);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(5, 5);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(5, 6);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(5, 7);

        //block = Instantiate(TetrisBlockPrefab, this.transform);
        //block.transform.localPosition = GetPosition(4, 7);

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
