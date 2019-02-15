using Unity.Collections;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace JobSystem
{
    public class Visualizer : MonoBehaviour
    {
        public MeshRenderer MeshRenderer;
        private Texture2D _texture;

        private readonly Color32 _white = Color.white;
        private readonly Color32 _black = Color.black;
        private Color32[] _colors;

        public void Initialize(int size)
        {
            _texture = new Texture2D(size, size);
            _texture.filterMode = FilterMode.Point;
            MeshRenderer.material.mainTexture = _texture;
            
            _colors = new Color32[size*size];
        }

        public void UpdateVisualization(NativeArray<int> cells, int size)
        {
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var color = cells[y * size + x] == 1 ? _black : _white;
                    _colors[y * size + x] = color;
                }
            }

            _texture.SetPixels32(_colors);
            _texture.Apply();
        }
    }
}