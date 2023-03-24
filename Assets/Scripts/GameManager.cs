using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Cubelet _cubeletPrefab;
    [SerializeField] ArrayCubelet _arrayCubeletPrefab;
    [Header("Tranforms")]
    [SerializeField] Transform _arrayCube;
    [SerializeField] Transform _pivot;
    [Header("Settings")]
    [SerializeField] Material _cubeletMaterial;
    [SerializeField] int _size;
    [SerializeField] Vector3Int _rotateAmount;
    [SerializeField] List<Transform> _pivots;

    [field: SerializeField] public Cubelet CurrentCubelet { get; set; }

    Cube cube;


    private void Start()
    {
        cube = new Cube(_size);

        _cubeletMaterial.SetFloat(ShaderManager.SIZE, _size - 1);
        
        BuildCube();
    }

    private void BuildCube()
    {
        float size = (_size * 0.5f) - 0.5f;

        for (float z = -size; z <= size; z++)
            for (float y = -size; y <= size; y++)
                for (float x = -size; x <= size; x++)
                {
                    Cubelet cubelet;
                    ArrayCubelet arrayCube;
                    Vector3Int index = new Vector3Int((int)(x + size), (int)(y + size), (int)(z + size));
                    bool x0 = Mathf.Abs(x) == size;
                    bool y0 = Mathf.Abs(y) == size;
                    bool z0 = Mathf.Abs(z) == size;

                    //0 are zero
                    if (!x0 && !y0 && !z0) continue;

                    //3 are zero
                    if (x0 && y0 && z0)
                    {
                        //Corner
                        cubelet = Instantiate(_cubeletPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                        arrayCube = Instantiate(_arrayCubeletPrefab, new Vector3(x, y, z), Quaternion.identity, _arrayCube);
                    }
                    //2 are zero
                    else if ((x0 && y0) || (x0 && z0) || (y0 && z0))
                    {
                        //Edge
                        cubelet = Instantiate(_cubeletPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                        arrayCube = Instantiate(_arrayCubeletPrefab, new Vector3(x, y, z), Quaternion.identity, _arrayCube);
                    }
                    //1 are zero
                    else
                    {
                        //Face
                        cubelet = Instantiate(_cubeletPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                        arrayCube = Instantiate(_arrayCubeletPrefab, new Vector3(x, y, z), Quaternion.identity, _arrayCube);
                    }

                    Node<Cubelet> node = new Node<Cubelet>(cubelet, index, _size);

                    cubelet.SetNode(node);
                    arrayCube.SetNode(node);
                    arrayCube.SetText((index.x + (index.y * _size) + (index.z * _size * _size)).ToString());

                    cube.XPlane[index.x].Add(node);
                    cube.YPlane[index.y].Add(node);
                    cube.ZPlane[index.z].Add(node);

                    cubelet.gameObject.name = $"({index.x + (index.y * _size) + (index.z * _size * _size)})";
                    cubelet.SetText($"{index.x + (index.y * _size) + (index.z * _size * _size)}");

                    cubelet.SetIndex(index.x, index.y, index.z, _size);
                    cubelet.SetPosition(x, y, z);
                    cubelet.SetMaterial();
                }

        _arrayCube.Translate(Vector3Int.up * (_size + 3));
    }

    public void SelectPlane(int axis)
    {
        List<Node<Cubelet>> list;
        switch ((Axis)axis)
        {
            case Axis.X:
                list = cube.XPlane[CurrentCubelet.Node.X];
                break;
            case Axis.Y:
                list = cube.YPlane[CurrentCubelet.Node.Y];
                break;
            case Axis.Z:
                list = cube.ZPlane[CurrentCubelet.Node.Z];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        foreach (var node in list)
        {
            _pivots.Add(node.Value.transform);
            node.Value.transform.SetParent(_pivot);
        }
    }
    
    public void SelectX()
    {
        List<Node<Cubelet>> list = cube.XPlane[CurrentCubelet.Node.X];
        foreach (var node in list)
        {
            node.Value.transform.SetParent(_pivot);
        }
    }
    public void SelectY()
    {
        List<Node<Cubelet>> list = cube.YPlane[CurrentCubelet.Node.Y];
        foreach (var node in list)
        {
            node.Value.transform.SetParent(_pivot);
        }
    }
    public void SelectZ()
    {
        List<Node<Cubelet>> list = cube.ZPlane[CurrentCubelet.Node.Z];
        foreach (var node in list)
        {
            node.Value.transform.SetParent(_pivot);
        }
    }

    public void RotatePlane(int axis)
    {
        _pivot.Rotate(_rotateAmount);
        foreach (Transform child in _pivots)
        {
            child.SetParent(transform);
        }

        _pivot.localRotation = Quaternion.identity;
        _pivots.Clear();

        switch ((Axis)axis)
        {
            case Axis.X:
                cube.RotateXPlane(CurrentCubelet.Node.X, _rotateAmount.x);
                break;
            case Axis.Y:
                cube.RotateYPlane(CurrentCubelet.Node.Y, -_rotateAmount.y);
                break;
            case Axis.Z:
                cube.RotateZPlane(CurrentCubelet.Node.Z, _rotateAmount.z);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

