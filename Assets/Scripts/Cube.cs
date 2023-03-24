using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubePlane = System.Collections.Generic.List<Node<Cubelet>>;

public enum Axis { X, Y, Z }

public class Cube : IEnumerable//, IEnumerable<T>//, ICollection, ICollection<T>
{
    public List<CubePlane> XPlane { get; private set; } = new List<CubePlane>();
    public List<CubePlane> YPlane { get; private set; } = new List<CubePlane>();
    public List<CubePlane> ZPlane { get; private set; } = new List<CubePlane>();

    public int Size { get; private set; }
    public int SizeSq { get; private set; }


    public Cube(int divisions)
    {
        Size = divisions;
        SizeSq = divisions * divisions;

        for (int i = 0; i < divisions; i++)
        {
            XPlane.Add(new CubePlane());
            YPlane.Add(new CubePlane());
            ZPlane.Add(new CubePlane());
        }
    }

    public void PrintPlane(CubePlane list)
    {
        foreach (var cubelet in list)
        {
            Debug.Log($"({cubelet.Value.name}) -> ({cubelet.Index})");
            //Debug.Log($"({cubelet.Value.name}) -> ({cubelet.X0},{cubelet.Y0},{cubelet.Z0})");
        }
    }

    private void RotatePlane(CubePlane list, int amount)
    {
        PrintPlane(list);
        Debug.Log("--------------------------------------");

        for (int i = 0; i < Mathf.Abs(amount / 90); i++)
        {
            Rotate(list, amount > 0, list.Count == SizeSq);
        }

        Debug.Log("--------------------------------------");
        PrintPlane(list);
    }
    public void RotateXPlane(int index, int amount)
    {
        CubePlane list = XPlane[index];

        RotatePlane(list, amount);
    }
    public void RotateYPlane(int index, int amount)
    {
        CubePlane list = YPlane[index];

        RotatePlane(list, amount);
    }
    public void RotateZPlane(int index, int amount)
    {
        CubePlane list = ZPlane[index];

        RotatePlane(list, amount);
    }

    private int GetNextIndex(int index)
    {
        return (Size - 1) + (Size * (index % Size)) - Mathf.FloorToInt(index / Size);
    }
    private int GetPreviousIndex(int index)
    {
        return (SizeSq - 1) - ((Size - 1) + (Size * (index % Size)) - Mathf.FloorToInt(index / Size));
    }

    private void Rotate(CubePlane cubePlane, bool isClockwise, bool isFace)
    {
        int layerCount = isFace ? Size / 2 : 1;
        Debug.LogError($"{layerCount}");

        //SwapCubelets
        for (int layer = 0; layer < layerCount; layer++)
        {
            int first = (Size + 1) * layer;
            int last = (Size - 2) + (Size * layer) - layer;
            Debug.LogError($"{first}\n{last}");

            //Swap
            for (int index = first; index <= last; index++)
            {
                //CCW BL->BR->TR->TL
                //CW  BL->TL->TR->BR
                Cubelet bottomLeft, bottomRight, topRight, topLeft;
                int bottomLeftIndex, bottomRightIndex, topRightIndex, topLeftIndex;

                if (isFace)
                {
                    bottomLeftIndex  = index;
                    bottomRightIndex = GetNextIndex(bottomLeftIndex);
                    topRightIndex    = GetNextIndex(bottomRightIndex);
                    topLeftIndex     = GetNextIndex(topRightIndex);

                    Debug.LogWarning($"{index}\n{bottomRightIndex}\n{topRightIndex}\n{topLeftIndex}");
                }
                else
                {
                    bottomLeftIndex  = index;
                    bottomRightIndex = (Size - 1) + (index * 2);
                    topRightIndex    = SizeSq - (int)Mathf.Pow(Size - 2, 2) - (index + 1);
                    topLeftIndex     = SizeSq - (int)Mathf.Pow(Size - 2, 2) - ((Size - 1) + (index * 2)) - 1;

                    Debug.LogWarning($"{index}\n{bottomRightIndex}\n{topRightIndex}\n{topLeftIndex}");
                }

                bottomLeft  = cubePlane[bottomLeftIndex ].Value;
                bottomRight = cubePlane[bottomRightIndex].Value;
                topRight    = cubePlane[topRightIndex   ].Value;
                topLeft     = cubePlane[topLeftIndex    ].Value;

                cubePlane[bottomLeftIndex ].SetValue(isClockwise ? topLeft     : bottomRight);
                cubePlane[bottomRightIndex].SetValue(isClockwise ? bottomLeft  : topRight);
                cubePlane[topRightIndex   ].SetValue(isClockwise ? bottomRight : topLeft);
                cubePlane[topLeftIndex    ].SetValue(isClockwise ? topRight    : bottomLeft);

                cubePlane[bottomLeftIndex].Value.SetNode(cubePlane[bottomLeftIndex]);
                cubePlane[bottomRightIndex].Value.SetNode(cubePlane[bottomRightIndex]);
                cubePlane[topRightIndex].Value.SetNode(cubePlane[topRightIndex]);
                cubePlane[topLeftIndex].Value.SetNode(cubePlane[topLeftIndex]);
            }
        }
    }

    private void Rotate2D(int[,] matrix)
    {
        int size = matrix.Length;
        int layer_count = size / 2;

        //SwapCubelets
        for (int layer = 0; layer < layer_count; layer++)
        {
            int first = layer;
            int last = size - first - 1; //(size - 1) - first

            //Swap
            for (int element = first; element < last; element++)
            {
                int offset = last - element - first;

                int bottomLeft = matrix[first, element];
                int bottomRight = matrix[element, last];
                int topRight = matrix[last, offset];
                int topLeft = matrix[offset, first];

                matrix[first, element] = topLeft;
                matrix[element, last] = bottomLeft;
                matrix[last, offset] = bottomRight;
                matrix[offset, first] = topRight;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}
