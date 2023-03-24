using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrayCubelet : MonoBehaviour
{
    [SerializeField] List<TextMeshPro> _texts;

    public Vector3 Position { get; private set; }
    public Vector3Int Index { get; private set; }
    [field: SerializeField] public Node<Cubelet> Node { get; private set; }


    private void Start()
    {
        Node.OnValueChanged += SetText;
    }

    public void SetText(string text)
    {
        foreach (var item in _texts)
        {
            item.text = text;
        }
    }

    public void SetIndex(int x, int y, int z)
    {
        Index.Set(x, y, z);
    }
    public void SetPosition(float x, float y, float z)
    {
        Position.Set(x, y, z);
    }

    public void SetNode(Node<Cubelet> node)
    {
        Node = node;
    }
}
