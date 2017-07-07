using UnityEngine;

public abstract class BoardVisualizer : MonoBehaviour
{
    public abstract void Initialize(Board board);
    public abstract void UpdateVisualization(Board board);
}