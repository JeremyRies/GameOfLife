using System.Globalization;
using System.Text;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    private StringBuilder _stringBuilder;
    private GUIStyle _style;
    private int _width;
    private int _height;
    private Rect _position;

    private void Start()
    {
        _style = new GUIStyle
        {
            normal = {textColor = Color.green}, alignment = TextAnchor.UpperLeft
        };

        _width = Screen.width;
        _height = Screen.height;
        
        _style.fontSize = _height * 2 / 100;
        
        var cornerOffset = _height * 2 / 200;
        _position = new Rect(cornerOffset, cornerOffset, _width, _height * 2f / 100);
    }

    private void OnGUI()
    {
        GUI.Label(_position, ((int)(1/Time.unscaledDeltaTime)).ToString(), _style);
    }
}