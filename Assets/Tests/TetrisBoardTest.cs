using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Zenject;

[TestFixture]
public class TetrisBoardTest : ZenjectIntegrationTestFixture
{



    [Inject]
    TetrisPiece.Factory factory;

    void CommonInstall()
    {
        PreInstall();

        var gameSettings = GameSettingsInstaller.InstallFromResource("GameSettingsInstaller");

        TetrisBoard tetrisBoard = new TetrisBoard(gameSettings.BoardRow, gameSettings.BoardCol);

        Container.Bind<GameObject>().WithId("TetrisBlockPrefab").FromInstance(gameSettings.TetrisBlockPrefab);
        Container.Bind<TetrisBoard>().FromInstance(tetrisBoard).AsSingle();
        Container.BindFactory<TetrisPiece, TetrisPiece.Factory>();
        Container.Bind<float>().WithId("IterateTestWaitTime").FromInstance(0.0001f);

        PostInstall();

        GameObject gameBoard = new GameObject();
        var tetrisGameController = gameBoard.AddComponent<TetrisGameController>();

        Camera camera = Container.InstantiateComponentOnNewGameObject<Camera>();
        camera.transform.position = new Vector3(0, 1, -10);
        camera.orthographic = true;

        Light light = Container.InstantiateComponentOnNewGameObject<Light>();
        light.type = LightType.Directional;
    }

    [UnityTest]
    public IEnumerator LineDownTest()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        CommonInstall();
        var gameSettings = GameSettingsInstaller.InstallFromResource("GameSettingsInstaller");
        TetrisGameController ctr = Container.InstantiatePrefabForComponent<TetrisGameController>(gameSettings.TetrisGameControllerPrefab);
        ctr.isUpdatingFallingPiece = false;

        TetrisPiece piece = null;

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate();
        yield return ctr.IterateMoveCol(-5);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate();
        yield return ctr.IterateMoveCol(-4);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate();
        yield return ctr.IterateMoveCol(-3);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate();
        yield return ctr.IterateMoveCol(2);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate();
        yield return ctr.IterateMoveCol(3);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate();
        yield return ctr.IterateMoveCol(4);
        yield return ctr.IterateFall();

        Assert.AreEqual(
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "0000000000\n" +
                "1110000111\n" +
                "1110000111\n" +
                "1110000111",
                ctr.GetBoardString(),
                "Clear not right"
                );
    }

    [UnityTest]
    public IEnumerator ClearTest()
    {

        CommonInstall();
        var gameSettings = GameSettingsInstaller.InstallFromResource("GameSettingsInstaller");
        TetrisGameController ctr = Container.InstantiatePrefabForComponent<TetrisGameController>(gameSettings.TetrisGameControllerPrefab);
        ctr.isUpdatingFallingPiece = false;

        TetrisPiece piece = null;

        piece = ctr.CreatePiece(TetrisPiece.PieceType.O);
        yield return ctr.IterateMoveCol(-4);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.O);
        yield return ctr.IterateMoveCol(-1);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.O);
        yield return ctr.IterateMoveCol(1);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.O);
        yield return ctr.IterateMoveCol(3);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.O);
        yield return ctr.IterateMoveCol(5);
        yield return ctr.IterateFall();


        yield return null;
        Assert.AreEqual(
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000",
            ctr.GetBoardString(),
            "Clear not right"
            );
    }

    [UnityTest]
    public IEnumerator DifferentLineTest()
    {
        CommonInstall();
        var gameSettings = GameSettingsInstaller.InstallFromResource("GameSettingsInstaller");
        TetrisGameController ctr = Container.InstantiatePrefabForComponent<TetrisGameController>(gameSettings.TetrisGameControllerPrefab);
        ctr.isUpdatingFallingPiece = false;

        TetrisPiece piece = null;

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateMoveCol(-4);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.T);
        yield return ctr.IterateRotate(3);
        yield return ctr.IterateMoveCol(1);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateMoveCol(3);
        yield return ctr.IterateFall();


        piece = ctr.CreatePiece(TetrisPiece.PieceType.T);
        yield return ctr.IterateMoveCol(-2);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.T);
        yield return ctr.IterateRotate(1);
        yield return ctr.IterateMoveCol(-4);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.T);
        yield return ctr.IterateMoveCol(3);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateMoveCol(-2);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateMoveCol(2);
        yield return ctr.IterateFall();

        piece = ctr.CreatePiece(TetrisPiece.PieceType.I);
        yield return ctr.IterateRotate(1);
        yield return ctr.IterateMoveCol(4);
        yield return ctr.IterateFall();
        yield return null;

        Assert.AreEqual(
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000000\n" +
            "0000000001\n" +
            "1110010101\n" +
            "1111011111",
            ctr.GetBoardString(),
            "Clear not right"
            );
    }
}
