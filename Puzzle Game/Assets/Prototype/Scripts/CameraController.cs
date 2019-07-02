using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Values")]
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private float smoothing;

    private Vector2 mouseLook = Vector2.zero;
    private Vector2 smoothV = Vector2.zero;

    private GameObject playerObj = null;

    // Start is called before the first frame update
    void Start()
    {
        //Get playerObj
        playerObj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseDir.x *= sensitivity * smoothing;
        mouseDir.y *= sensitivity * smoothing;

        smoothV.x = Mathf.Lerp(smoothV.x, mouseDir.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDir.y, 1f / smoothing);

        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        playerObj.transform.rotation = Quaternion.AngleAxis(mouseLook.x, playerObj.transform.up);
    }
}
