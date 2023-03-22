using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float rotationDamping = 3f;
    [SerializeField] private float heightDamping = 2f;

    private float currentRotationAngle;
    private float currentHeight;

    void LateUpdate()
    {
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 targetPosition = target.position - currentRotation * Vector3.forward * distance;
        targetPosition.y = currentHeight;

        transform.position = targetPosition;
        transform.LookAt(target.position + Vector3.up * height);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
