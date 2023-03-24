using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cubelets = CubeNode<UnityEngine.GameObject>;

public class Main : MonoBehaviour
{
    [SerializeField] List<GameObject> Cubes = new List<GameObject>();
    List<Cubelets> Cube = new List<Cubelets>();



    private void LoadCube(int size)
    {
        int sizeSqIndex = (size * size);
        int sizeIndex = size;
        for (int z = 0; z < size; z++)
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    Cubelets Cubelets = new Cubelets(Cubes[(z * sizeSqIndex) + (y * sizeIndex) + (x)]);
                    Cubelets.Name = Cubelets.Item.name;
                    Cube.Add(Cubelets);

                    if (z > 0)
                    {
                        int index = ((z - 1) * sizeSqIndex) + (y * sizeIndex) + x;
                        Cubelets.MakeAdjacentZ(Cube[index]);
                    }
                    if (y > 0)
                    {
                        int index = (z * sizeSqIndex) + ((y - 1) * sizeIndex) + x;
                        Cubelets.MakeAdjacentY(Cube[index]);
                    }
                    if (x > 0)
                    {
                        int index = (z * sizeSqIndex) + (y * sizeIndex) + (x - 1);
                        Cubelets.MakeAdjacentX(Cube[index]);
                    }
                }
    }

    private void Start()
    {
        LoadCube(3);
        foreach (var cubeNode in Cube)
        {
            cubeNode.DebugPrintAdjacent();
        }
    }
}
