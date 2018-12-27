using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraOrbit : MonoBehaviour {

	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float smoothing = 1.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = .5f;
	public float distanceMax = 15f;

	public bool activeOnClick = false;
	private bool _canOrbit = false;

	private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;

	// Use this for initialization
	void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}

		if (!activeOnClick) {
			_canOrbit = true;
		}
	}

	void LateUpdate () 
	{
		if (activeOnClick) {
			if (Input.GetMouseButtonDown (0)) {
				_canOrbit = true;
			}
			else if(Input.GetMouseButtonUp (0)){
				_canOrbit = false;
			}
		}

		if (target) 
		{
			
			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
			Quaternion rotation = transform.localRotation;
			if (_canOrbit) {
				x += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.02f;
				y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;

				y = ClampAngle (y, yMinLimit, yMaxLimit);

				rotation = Quaternion.Slerp(rotation, Quaternion.Euler (y, x, 0), smoothing);

				RaycastHit hit;
				if (Physics.Linecast (target.position, transform.position, out hit)) {
					distance -= hit.distance;
				}

			}

			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			Vector3 position = Vector3.Slerp(transform.localPosition, rotation * negDistance + target.position, smoothing);

			transform.rotation = rotation;
			transform.position = position;


		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}