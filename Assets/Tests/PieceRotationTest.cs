using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Zenject;
using System;

[TestFixture]
public class PieceRotationTest : ZenjectIntegrationTestFixture
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

        PostInstall();
    }


    [Test]
    public void RotateIPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.I);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "0010\n" +
            "0010\n" +
            "0010\n" +
            "0010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
           "0000\n" +
           "0000\n" +
           "1111\n" +
           "0000",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "0100\n" +
            "0100\n" +
            "0100\n" +
            "0100",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "0000\n" +
            "1111\n" +
            "0000\n" +
            "0000",
            piece.GetString(), "Failed CW rotate four times");

    }

    [Test]
    public void RotateIPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.I);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "0100\n" +
            "0100\n" +
            "0100\n" +
            "0100",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
           "0000\n" +
           "0000\n" +
           "1111\n" +
           "0000",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "0010\n" +
            "0010\n" +
            "0010\n" +
            "0010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "0000\n" +
            "1111\n" +
            "0000\n" +
            "0000",
            piece.GetString(), "Failed CCW rotate four times");

    }

    [Test]
    public void RotateJPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.J);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "011\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "001",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "110",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "100\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateJPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.J);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "110",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "001",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "011\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "100\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateLPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.L);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "011",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "100",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "110\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "001\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateLPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.L);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "110\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "100",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "011",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "001\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateOPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.O);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateOPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.O);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateSPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.S);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "001",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "011\n" +
            "110",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "100\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "011\n" +
            "110\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateSPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.S);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "100\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "011\n" +
            "110",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "001",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "011\n" +
            "110\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateTPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.T);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "010",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateTPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.T);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "010",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateZPieceCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.Z);

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "001\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "110\n" +
            "011",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "100",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        piece.ApplyChange();
        Assert.AreEqual(
            "110\n" +
            "011\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateZPieceCCW()
    {
        CommonInstall();
        // Use the Assert class to test conditions.

        TetrisPiece piece = factory.Create();
        piece.Init(TetrisPiece.PieceType.Z);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "100",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "000\n" +
            "110\n" +
            "011",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "001\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        piece.ApplyChange();
        Assert.AreEqual(
            "110\n" +
            "011\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

}
