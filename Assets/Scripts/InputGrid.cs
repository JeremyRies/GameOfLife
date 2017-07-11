using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputGrid : MonoBehaviour
{
    public GridButton GridButtonPrefab;
    public GridButton[,] GridButtons;

    public Button SerializeButton; 

    public int Width;
    public int Height;

    public float CellSize;
    public Field[] Fields;
	// Use this for initialization
    void Start()
    {
        GridButtons = new GridButton[Width, Height];
        Fields = new Field[Width*Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var offset = 3;
                var pos = new IntVector2(x+offset, y+offset);
                Fields[y *Width+x] = new Field(x,y);
                var button = Instantiate(GridButtonPrefab, (Vector2) pos* CellSize, transform.rotation,transform);
                GridButtons[x, y] = button;
            }
        }
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var pos = new IntVector2(x, y);
                GridButtons[x, y].InputButton.onClick.AddListener(Clicked(pos));
            }

        }
        SerializeButton.onClick.AddListener(Serialize);
    }

    private UnityAction Clicked(IntVector2 pos)
    {
        return () =>
        {
            var field = Fields[pos.Y * Width + pos.X];
            field.Alive = !field.Alive;
            UpdateBoard();
        };
    }

    private void UpdateBoard()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var field = Fields[y* Width +x];
                GridButtons[x, y].Image.color = field.Alive ? Color.black : Color.white;
            }
        }
    }

    private void Serialize()
    {
        var board = new SerializedBoard();
        board.Fields = Fields.ToList();
         var field = new Field(1,1,true);
        var fieldsJson = JsonUtility.ToJson(board);
        var utcNowTicks = DateTime.UtcNow.Ticks;
        File.WriteAllText("Assets/Resources/"+ "GliderSpawner" + ".json", fieldsJson);
    }
    // Update is called once per frame
    void Update () {
		
	}
}