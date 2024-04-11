using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _Target;

    public float distance = 10.0f; // The initial distance from the target
    public float scrollSpeed = 1.0f; // The speed of changing the distance
    public float rotateSpeed = 5.0f; // Speed of camera rotation

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        // Rotate the camera to always look at the target
        transform.LookAt(_Target);

        // Change the distance based on the scroll wheel input
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 1.0f, 20.0f);

        // Rotate the camera around the target when the left mouse button is held down
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * rotateSpeed;
            y -= Input.GetAxis("Mouse Y") * rotateSpeed;
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + _Target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
    //void Update()
    //{
    //    transform.LookAt(_Target);
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        dragOrigin = Input.mousePosition;
    //        return;
    //    }

    //    if (!Input.GetMouseButton(1)) return;

    //    Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
    //    Vector3 move = new Vector3(pos.x * speed * Time.deltaTime, 0, pos.y * speed * Time.deltaTime);

    //    transform.Translate(move, Space.World);
    //}
}
