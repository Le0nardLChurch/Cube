using System;
using UnityEngine;

[Serializable]
public class Node<T>
{
    public Action<string> OnValueChanged;

    public T Value { get; private set; }
    public int Index { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public Node(T value, Vector3Int index, int size)
    {
        Value = value;

        Index = index.x + (index.y * size) + (index.z * size * size);
        X = index.x;
        Y = index.y;
        Z = index.z;
    }

    public void SetValue(T value)
    {
        Value = value;
        OnValueChanged?.Invoke(Value.ToString());
    }
}