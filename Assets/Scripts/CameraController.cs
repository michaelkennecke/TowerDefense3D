using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float currentZoom = 15f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 25f;
    [SerializeField] private float pitch = 2f;

    [SerializeField] private bool isXAxisClamp = false;
    [SerializeField] private float axisSpeed = 100F;
    private float currentX = 0f;
    [SerializeField] private float xMin = -80f;
    [SerializeField] private float xMax = 80f;

    private float currentY = 0f;

    

    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            currentX -= Input.GetAxis("Mouse X") * axisSpeed * Time.deltaTime;
            if (isXAxisClamp)
            {
                currentX = Mathf.Clamp(currentX, xMin, xMax);
            }
            currentY -= Input.GetAxis("Mouse Y") * axisSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target.GetComponent<PlayerController>().Move(hit.point);
            }
        }
    }

    private void LateUpdate()
    {
        this.UpdateCameraPosition();
        this.UpdateCameraRotation();
    }

    private void UpdateCameraPosition()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }

    private void UpdateCameraRotation()
    {
        transform.RotateAround(target.position, Vector3.up, currentX);
        transform.RotateAround(target.position, Vector3.left, currentY);
        Quaternion axisFix = transform.rotation;
        axisFix.eulerAngles = new Vector3(axisFix.eulerAngles.x, axisFix.eulerAngles.y, 0.0f);
        transform.rotation = axisFix;
    } 
}
