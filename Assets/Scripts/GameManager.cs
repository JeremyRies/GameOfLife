using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Width;
    public int Height;

    public float TimeBetweenUpdates;
    public AbstractBoardVisualizer BoardVisualizer;
    public SimulationType SimulationType;

    public AbstractStartPositionProvider StartPositionProvider;

    private float _timeSinceLastUpdate;

    private Board _board;
    private Board _helperBoard;

    private IGameSimulation _simulation;

    private void Start()
    {
        RunGame();
    }

    private void RunGame()
    {
        _board = new Board(Width, Height);
        _helperBoard = new Board(Width, Height);

        StartPositionProvider.SetStartFields(_board);

        _simulation = ChooseSimulation(SimulationType);

        BoardVisualizer.Initialize(_board);
    }

    private IGameSimulation ChooseSimulation(SimulationType simulationType)
    {
        switch (simulationType)
        {
            case SimulationType.SingleThreaded:
                return new SingleThreadSimulation();
            case SimulationType.MultiThreaded:
                return new MultiThreadSimulation();
            default:
                throw new ArgumentOutOfRangeException("simulationType", simulationType, null);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RunGame();
        }
        if (_timeSinceLastUpdate > TimeBetweenUpdates)
        {
            UpdateHelperBoard();
             _board = _simulation.Simulate(_board,_helperBoard);
            BoardVisualizer.UpdateVisualization(_board);

            _timeSinceLastUpdate = 0;
        }
        else
        {
            _timeSinceLastUpdate += Time.deltaTime;
        }
    }

    private void UpdateHelperBoard()
    {
        foreach (var boardField in _board.Fields)
        {
            _helperBoard.Fields[boardField.X, boardField.Y].Alive = boardField.Alive;
        }
    }
}