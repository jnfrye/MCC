using UnityEngine;

namespace MCC
{ 
	public class RealCamera : Entity
	{		
		private Rigidbody rb;

		private void Start() { rb = GetComponent<Rigidbody>(); }

		public void OnTestEvent()
		{
			Debug.LogError("IT WORKED!");
		}

		public static void OnStringTestEvent(string arg)
		{
			Debug.LogError(arg);
		}

		private void OnEnable()
		{
			controller.TiltCommand += PerformCameraTilt;
			controller.ZoomCommand += PerformCameraZoom;
		}

		private void OnDisable()
		{
			controller.TiltCommand -= PerformCameraTilt;
			controller.ZoomCommand -= PerformCameraZoom;
		}

		private void PerformCameraZoom(float zoomDirection)
		{
			float FOVChange = zoomDirection * GlobalConstants.Camera.ZoomSpeed * Time.deltaTime;
			float newFOV = Camera.main.fieldOfView - FOVChange;
			if (GlobalConstants.Camera.AllowedFOVs.Contains(newFOV))
			{
				Camera.main.fieldOfView = newFOV;
			}
		}

		private void PerformCameraTilt(int screenBorderDirection)
		{
			Vector3 eulerRotation = new Vector3(0, 0, 0);
			eulerRotation.x += screenBorderDirection * GlobalConstants.Camera.TiltSpeed * Time.deltaTime;

			float newTiltAngle = Camera.main.transform.localEulerAngles.x + eulerRotation.x;
			if (GlobalConstants.Camera.AllowedTiltAngles.Contains(newTiltAngle))
			{
				Camera.main.transform.Rotate(eulerRotation);
			}
		}
	}
}