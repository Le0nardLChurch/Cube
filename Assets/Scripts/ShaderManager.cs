using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public const string IS_SELECTED = "_IsSelected";
    public const string SELECTED_COLOR = "_SelectedColor";
    public const string BLUE_COLOR = "_BlueColor";
    public const string GREEN_COLOR = "_GreenColor";
    public const string WHITE_COLOR = "_WhiteColor";
    public const string YELLOW_COLOR = "_YellowColor";
    public const string RED_COLOR = "_RedColor";
    public const string ORANGE_COLOR = "_OrangeColor";
    public const string SIZE = "_Size";
    public const string CUBE_Index = "_CubeIndex";

    [SerializeField] Material _material;
    [SerializeField]
    Color[] _colors = new Color[]
    {
        new Color(0f, 0.2705882f, 6784314f, 1f),
        new Color(0f, 0.6078432f, 0.282353f, 1f),
        new Color(1f, 1f, 1f, 1f),
        new Color(1f, 0.8352942f, 0f, 1f),
        new Color(0.7254902f, 0f, 0f, 1f),
        new Color(1f, 0.3490196f, 0f, 1f)
    };

    public void UpdateCubeShader()
    {
        _material.SetColor(BLUE_COLOR, _colors[0]);
        _material.SetColor(GREEN_COLOR, _colors[1]);
        _material.SetColor(WHITE_COLOR, _colors[2]);
        _material.SetColor(YELLOW_COLOR, _colors[3]);
        _material.SetColor(RED_COLOR, _colors[4]);
        _material.SetColor(ORANGE_COLOR, _colors[5]);
    }
}
