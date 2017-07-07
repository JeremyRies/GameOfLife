using UnityEngine;

class Texture2DVisualizer : BoardVisualizer
{
    public MeshRenderer MeshRenderer;
    private Texture2D _texture;

    private readonly Color32 _white = Color.white;
    private readonly Color32 _black = Color.black;

    public override void Initialize(Board board)
    {
        _texture = new Texture2D(board.Width,board.Height);
        _texture.filterMode = FilterMode.Point;
        MeshRenderer.material.mainTexture = _texture;
        UpdateVisualization(board);
    }

    public override void UpdateVisualization(Board board)
    {
        var boardWidth = board.Width;
        var colors = new Color32[boardWidth*board.Height];
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                var boardField = board.Fields[x, y];
                var color = boardField.Alive ? _black : _white;
                colors[boardWidth * y + x] = color;
            }
        }
       
        
        _texture.SetPixels32(colors);
        _texture.Apply();

    }
}