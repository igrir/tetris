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

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(0, 0);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(0, 1);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(0, 2);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(1, 1);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(5, 5);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(5, 6);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(5, 7);

        block = Instantiate(TetrisBlockPrefab, this.transform);
        block.transform.localPosition = GetPosition(4, 7);

    }

    Vector2 GetPosition(int row, int col)
    {
        Vector3 desiredPosition = new Vector3(col * blockSize.x, row * blockSize.y);
        return desiredPosition;
    }
}
