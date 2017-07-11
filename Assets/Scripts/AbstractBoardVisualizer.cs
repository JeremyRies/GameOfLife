using UnityEngine;

public abstract class AbstractBoardVisualizer : MonoBehaviour
{
    public abstract void Initialize(Board board);
    public abstract void UpdateVisualization(Board board);
}