using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private float normalDistance = 10.0f;
    [SerializeField] private float zoomDistance = 7.0f;
    [SerializeField] private float normalHeight = 5.0f;
    [SerializeField] private float zoomHeight = 3.0f;
    [SerializeField] private float heightDamping = 2.0f;
    [SerializeField] private float rotationDamping = 3.0f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private Transform target;

    public bool IsZoomBoosted { get; set; }
    private float currentDistance;
    private float currentHeight;

	private void Awake()
	{
        IsZoomBoosted = false;
        currentDistance = normalDistance;
        currentHeight = normalHeight;
	}
	private void LateUpdate()
    {
        SelectCameraZoom();
    }
    private void SelectCameraZoom()
	{
       
        if(IsZoomBoosted == false)
		{
            ChangeParametrs(normalDistance, normalHeight);
            CameraZoom(currentDistance, currentHeight);
		}
        else
		{
            ChangeParametrs(zoomDistance, zoomHeight);
            CameraZoom(currentDistance, currentHeight);
        }
	}
    private void ChangeParametrs(float distance,float height)
	{
        currentDistance = Mathf.Lerp(currentDistance, distance, Time.deltaTime * zoomSpeed);
        currentHeight = Mathf.Lerp(currentHeight, height, Time.deltaTime * zoomSpeed);
	}
   
    private void CameraZoom(float distance,float height)
	{
        // Early out if we don't have a target
        if (!target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
    }
}