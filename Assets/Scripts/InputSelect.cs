using UnityEngine;

public class InputSelect : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameManager test;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetInput();
        }
    }
    void GetInput()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out Cubelet cubelet))
            {
                test.CurrentCubelet = cubelet;
                cubelet.Renderer.material.SetInt("_IsSelected", 1);
            }
        }
    }
}

