using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobSystem
{
    public class JobSystemManager : MonoBehaviour
    {
        private int _xyDimensions = 1024;
        private NativeArray<int> _cells;
        private NativeArray<int> _cellsNextGeneration;

        [SerializeField] private Visualizer _visualizer;

        private void Start()
        {
            _visualizer.Initialize(_xyDimensions);
            
            _cells = new NativeArray<int>(_xyDimensions * _xyDimensions, Allocator.Persistent);
            _cellsNextGeneration = new NativeArray<int>(_xyDimensions * _xyDimensions, Allocator.Persistent);
            
            for (int posX = 0; posX < _xyDimensions; posX++)
            {
                for (int posY = 0; posY < _xyDimensions; posY++)
                {
                    _cells[_xyDimensions * posY + posX] =  Random.Range(0, 2) == 1 ? 1 : 0;
                }    
            }
            
            _visualizer.UpdateVisualization(_cells,_xyDimensions);
        }

        private void Update()
        {
            var job = new GameOfLifeJob();
            job.Cells = _cells;
            job.CellsNextGeneration = _cellsNextGeneration;
            job.XyDimensions = _xyDimensions;


            var handle = job.Schedule(_xyDimensions* _xyDimensions, 4096);
            handle.Complete();

            for (int index = 0; index < _xyDimensions * _xyDimensions; index++)
            {
                _cells[index] = _cellsNextGeneration[index];
            }

            _visualizer.UpdateVisualization(_cells, _xyDimensions);
        }

        private void OnDestroy()
        {
            _cells.Dispose();
            _cellsNextGeneration.Dispose();
        }
    }
}