using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace JobSystem
{
    [BurstCompile]
    public struct GameOfLifeJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> Cells;
        [WriteOnly] public NativeArray<int> CellsNextGeneration;
        [ReadOnly] public int XyDimensions;
    
        // index = posY * XYDimensions + posX
        // posX = index % XYDimensions 
        // posY = index / XYDimensions
        
        public void Execute(int index)
        {
            var posX = index % XyDimensions;
            var posY = index / XyDimensions;

            var aliveFields = GetSurroundingAliveFieldsCount(posX, posY);
            var isAlive = Cells[index] == 1;
            
            if (aliveFields > 3)
            {
                CellsNextGeneration[index] = 0;
                return;
            }
            if (aliveFields < 2 && isAlive)
            {
                CellsNextGeneration[index] = 0;
                return;
            }
            if(aliveFields < 4 && isAlive)
            {
                CellsNextGeneration[index] = 1;
                return;
            }
            if(aliveFields == 3)
            {
                CellsNextGeneration[index] = 1;
                return;
            }
            
            CellsNextGeneration[index] = 0;
        }

        private int GetSurroundingAliveFieldsCount(int xpos, int ypos)
        {
            var sum = 0;
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;

                    var posX = Mod((xpos + x), XyDimensions);
                    var posY = Mod((ypos + y), XyDimensions);

                    if (Cells[posY * XyDimensions + posX] == 1)
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
        
        private int Mod(int x, int m) {
            return (x%m + m)%m;
        }
    }
}