using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Camera settings
    public float panSpeed = 3f;
    public float turnSpeed = 3f;
    public float scrollSpeed = 10f;

    public Vector2 panLimit;
    public float minY = 0f;
    public float maxY = 50f;

    void Update() {
        //Movement (WASD)
        if (Input.GetKey("w")) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.Self);
        } else if (Input.GetKey("a")) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.Self);
        } else if (Input.GetKey("s")) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.Self);
        } else if (Input.GetKey("d")) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.Self);
        }

        //Rotation (Right mouse button)
        if (Input.GetKey(KeyCode.Mouse1)) {
            Vector2 mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            transform.Rotate(Vector3.up, mouseAxis.x * turnSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, -mouseAxis.y * turnSpeed * Time.deltaTime, Space.World);
            //Prevent Z-rotation
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;
        }

        Vector3 pos = transform.position;

        //Zoom (Scrollwheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        //Movement boundaries
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        //Set final frame position according to zoom input and movement boundaries
        transform.position = pos;
    }
}
