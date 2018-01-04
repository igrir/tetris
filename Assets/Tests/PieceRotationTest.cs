using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PieceRotationTest
{

    [Test]
    public void RotateIPieceCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.I);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "0010\n" +
            "0010\n" +
            "0010\n" +
            "0010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
           "0000\n" +
           "0000\n" +
           "1111\n" +
           "0000",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "0100\n" +
            "0100\n" +
            "0100\n" +
            "0100",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
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
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.I);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "0100\n" +
            "0100\n" +
            "0100\n" +
            "0100",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
           "0000\n" +
           "0000\n" +
           "1111\n" +
           "0000",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "0010\n" +
            "0010\n" +
            "0010\n" +
            "0010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
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
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.J);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "011\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "001",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "110",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "100\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateJPieceCCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.J);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "110",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "001",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "011\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "100\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateLPieceCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.L);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "011",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "100",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "110\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "001\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateLPieceCCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.L);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "110\n" +
            "010\n" +
            "010",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "100",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "010\n" +
            "011",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "001\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateOPieceCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.O);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "11\n" +
            "11",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateOPieceCCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.O);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "11\n" +
            "11",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "11\n" +
            "11",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateSPieceCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.S);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "001",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "000\n" +
            "011\n" +
            "110",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "100\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "011\n" +
            "110\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateSPieceCCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.S);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "100\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "000\n" +
            "011\n" +
            "110",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "001",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "011\n" +
            "110\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateTPieceCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.T);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "010",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateTPieceCCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.T);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "010",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "000\n" +
            "111\n" +
            "010",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "111\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

    [Test]
    public void RotateZPieceCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.Z);

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "001\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "000\n" +
            "110\n" +
            "011",
           piece.GetString(), "Failed CW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "100",
            piece.GetString(), "Failed CW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CW);
        Assert.AreEqual(
            "110\n" +
            "011\n" +
            "000",
            piece.GetString(), "Failed CW rotate four times");
    }

    [Test]
    public void RotateZPieceCCW()
    {
        // Use the Assert class to test conditions.
        TetrisPieceFactory factory = new TetrisPieceFactory();
        TetrisPiece piece = factory.Generate(TetrisPiece.PieceType.Z);

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "010\n" +
            "110\n" +
            "100",
            piece.GetString(), "Failed CCW rotate once");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "000\n" +
            "110\n" +
            "011",
           piece.GetString(), "Failed CCW rotate twice");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "001\n" +
            "011\n" +
            "010",
            piece.GetString(), "Failed CCW rotate three times");

        piece.Rotate(TetrisPiece.Rotation.CCW);
        Assert.AreEqual(
            "110\n" +
            "011\n" +
            "000",
            piece.GetString(), "Failed CCW rotate four times");
    }

}
