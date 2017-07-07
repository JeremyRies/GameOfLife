using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int Width;
    public int Height;

    public float TimeBetweenUpdates;
    public BoardVisualizer BoardVisualizer;
    public SimulationType SimulationType;

    private float _timeSinceLastUpdate;

    private Board _board;
    private Board _helperBoard;

    private IGameSimulation _simulation;

    private void Start()
    {
        _board = new Board(Width, Height);
        _helperBoard = new Board(Width, Height);

        _simulation = ChooseSimulation(SimulationType);

        Randomize(_board.Fields);
       
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

    private void Randomize(Field[,] fields)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var cell = fields[x, y];
                cell.Alive = Random.Range(0, 2) == 0;
            }
        }
    }

    private void Update()
    {   
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
            _helperBoard.Fields[boardField.Position.X, boardField.Position.Y].Alive = boardField.Alive;
        }
    }
}

