using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

public class BoardTests : MonoBehaviour
{

    private Board TestBoard(List<IntVector2> aliveFields = null)
    {
        var testBoard = new Board(3, 3);
        if (aliveFields != null)
        {
            foreach (var aliveField in aliveFields)
            {
                testBoard.Fields[aliveField.X, aliveField.Y].Alive = true;
            }
        }
        return testBoard;
    }

    [Test]
    [TestCase(-1, 0)]
    [TestCase(-1, -100)]
    [TestCase(3, 1)]
    [TestCase(1, 3)]
    [TestCase(5, 5)]
    public void IsInBoard_False(int x, int y)
    {
        var board = TestBoard();
        var isInBoard = board.IsInBoard(x, y);

        Assert.AreEqual(false, isInBoard);
    }

    //
    // xa
    // xxx
    [Test]
    public void SurroundingFields_CorrectAmount()
    {
        var aliveFields = new List<IntVector2>();
        aliveFields.Add(new IntVector2(0, 0));
        aliveFields.Add(new IntVector2(1, 0));
        aliveFields.Add(new IntVector2(2, 0));
        aliveFields.Add(new IntVector2(0, 1));

        aliveFields.Add(new IntVector2(1, 1));
        var board = TestBoard(aliveFields);

        var aliveFieldsCount = board.GetSurroundingAliveFieldsCount(board.Fields[1, 1]);

        Assert.AreEqual(4, aliveFieldsCount);
    }

    //
    //  a
    // 
    [Test]
    public void StaysAliveNextRound_NoSurroundingAlive_false()
    {
        var aliveFields = new List<IntVector2>();

        aliveFields.Add(new IntVector2(1, 1));
        var board = TestBoard(aliveFields);

        var shouldLive = board.ShouldLiveNextTurn(board.Fields[1, 1]);

        Assert.AreEqual(false, shouldLive);
    }

    //
    //  a
    // xxx
    [Test]
    public void StaysAliveNextRound_3SurroundingAlive_true()
    {
        var aliveFields = new List<IntVector2>();
        aliveFields.Add(new IntVector2(0, 0));
        aliveFields.Add(new IntVector2(1, 0));
        aliveFields.Add(new IntVector2(2, 0));

        aliveFields.Add(new IntVector2(1, 1));
        var board = TestBoard(aliveFields);

        var shouldLive = board.ShouldLiveNextTurn(board.Fields[1, 1]);

        Assert.AreEqual(true, shouldLive);
    }

    //
    // xa
    // xxx
    [Test]
    public void StaysAliveNextRound_4SurroundingAlive_false()
    {
        var aliveFields = new List<IntVector2>();
        aliveFields.Add(new IntVector2(0, 0));
        aliveFields.Add(new IntVector2(1, 0));
        aliveFields.Add(new IntVector2(2, 0));
        aliveFields.Add(new IntVector2(0, 1));

        aliveFields.Add(new IntVector2(1, 1));
        var board = TestBoard(aliveFields);

        var shouldLive = board.ShouldLiveNextTurn(board.Fields[1, 1]);

        Assert.AreEqual(false, shouldLive);
    }

    //
    //  
    // xxx
    [Test]
    public void GetsCreatedNextRound_3SurroundingAlive_true()
    {
        var aliveFields = new List<IntVector2>();
        aliveFields.Add(new IntVector2(0, 0));
        aliveFields.Add(new IntVector2(1, 0));
        aliveFields.Add(new IntVector2(2, 0));

        var board = TestBoard(aliveFields);

        var shouldLive = board.ShouldLiveNextTurn(board.Fields[1, 1]);

        Assert.AreEqual(true, shouldLive);
    }

}
