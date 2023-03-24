using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Cubelet : MonoBehaviour
{
    [SerializeField] List<TextMeshPro> _texts;

    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    [field: SerializeField] public Vector3 Position { get; private set; }
    [field: SerializeField] public Vector3Int Index { get; private set; }
    [field: SerializeField] public Node<Cubelet> Node { get; private set; }

    MaterialPropertyBlock propertyBlock;
    int size;


    public void SetText(string text)
    {
        foreach (var item in _texts)
        {
            item.text = text;
        }
    }

    public void SetPosition(float x, float y, float z)
    {
        Position = new Vector3(x, y, z);
    }
    public void SetIndex(int x, int y, int z, int size)
    {
        this.size = size;
        Index = new Vector3Int(x, y, z);
    }
    public void SetNode(Node<Cubelet> node)
    {
        Node = node;
    }
    public void SetMaterial()
    {
        propertyBlock ??= new MaterialPropertyBlock();
        propertyBlock.SetVector(ShaderManager.CUBE_Index, new Vector3(Index.x, Index.y, Index.z));
        propertyBlock.SetInt(ShaderManager.IS_SELECTED, 0);
        Renderer.SetPropertyBlock(propertyBlock);
    }


    public override string ToString()
    {
        return (Index.x + (Index.y * size) + (Index.z * size * size)).ToString("000");
    }
    
    /**/
    private void OnMouseEnter()
    {
        propertyBlock ??= new MaterialPropertyBlock();
        propertyBlock.SetInt(ShaderManager.IS_SELECTED, 1);
        Renderer.SetPropertyBlock(propertyBlock);
    }
    private void OnMouseExit()
    {
        propertyBlock ??= new MaterialPropertyBlock();
        propertyBlock.SetInt(ShaderManager.IS_SELECTED, 0);
        Renderer.SetPropertyBlock(propertyBlock);
    }
    /**/
}
