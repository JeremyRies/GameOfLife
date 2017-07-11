using UnityEngine;

class LoadFromFileStartPositionProvider : AbstractStartPositionProvider
{
    public TextAsset TextAsset;
    public override void SetStartFields(Board board)
    {
        var loadedBoard = JsonUtility.FromJson<SerializedBoard>(TextAsset.text);
        foreach (var loadedField in loadedBoard.Fields)
        {
            board.Fields[loadedField.X+100, loadedField.Y+100].Alive = loadedField.Alive;
        }
    }
}