using System.Collections;
using System.Collections.Generic;

public class CubeNode<T> : CubeNode
{
    List<CubeNode<T>> adjacentNodes;

    private T item;
    public T Item => item;

    public CubeNode(T item)
    {
        adjacentNodes = new List<CubeNode<T>>();
        this.item = item;
    }
    public CubeNode()
    {
        adjacentNodes = new List<CubeNode<T>>();
        this.item = default;
    }
   
    private void MakeAdjacent(CubeNode<T> cubeNode)
    {
        AddAdjacent(cubeNode);
        cubeNode.AddAdjacent(this);
    }
    private void AddAdjacent(CubeNode<T> cubeNode)
    {
        adjacentNodes.Add(cubeNode);
    }
}

public class CubeNode
{
    public string Name;
    protected CubeNode
        Up,
        Down,
        Left,
        Right,
        Front,
        Back;

    public CubeNode()
    {

    }
    public void MakeAdjacentX(CubeNode cubeNode, bool isRight = false)
    {
        if (isRight)
        {
            Right = cubeNode;
            cubeNode.Left = this;
        }
        else
        {
            Left = cubeNode;
            cubeNode.Right = this;
        }
    }
    public void MakeAdjacentY(CubeNode cubeNode, bool isDown = false)
    {
        if (isDown)
        {
            Down = cubeNode;
            cubeNode.Up = this;
        }
        else
        {
            Up = cubeNode;
            cubeNode.Down = this;
        }
    }
    public void MakeAdjacentZ(CubeNode cubeNode, bool isBack = false)
    {
        if (isBack)
        {
            Back = cubeNode;
            cubeNode.Front = this;
        }
        else
        {
            Front = cubeNode;
            cubeNode.Back = this;
        }
    }
    public void DebugPrintAdjacent()
    {
        string adjacentNames = string.Empty;

        if (!Left.IsNull())
            adjacentNames += ($"Left: {Left.Name}, ");
        if (!Right.IsNull())
            adjacentNames += ($"Right: {Right.Name}, ");
        if (!Up.IsNull())
            adjacentNames += ($"Up: {Up.Name}, ");
        if (!Down.IsNull())
            adjacentNames += ($"Down: {Down.Name}, ");
        if (!Front.IsNull())
            adjacentNames += ($"Front: {Front.Name}, ");
        if (!Back.IsNull())
            adjacentNames += ($"Back: {Back.Name}, ");

        UnityEngine.Debug.Log($"{Name}: {{{adjacentNames.Remove(adjacentNames.Length-2)}}}");
    }
}
public static class CubeNodeExt
{
    public static bool IsNull(this CubeNode node)
    {
        return node == null;
    }
}
