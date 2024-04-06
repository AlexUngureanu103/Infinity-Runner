using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _Target;

    private Vector3 _Offset;

    void Start()
    {
        _Offset = transform.position - _Target.position;
    }

    void Update()
    {
        Vector3 targetPos = _Target.position + _Offset;
        targetPos.x = 0;
        transform.position = targetPos;
    }
}
