using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
    public static MousePosition3D Instance { get; private set; }

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Ray currRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(currRay, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

    private Vector3 GetMouseWorldPosition_Instance()
    {
        Ray currRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(currRay, out RaycastHit raycastHit))
        if (Physics.Raycast(currRay, out RaycastHit raycastHit, float.MaxValue, layerMask)) //with mask
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
