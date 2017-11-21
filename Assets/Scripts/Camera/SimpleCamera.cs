using UnityEngine;

namespace MCC
{ 
	public class SimpleCamera : Entity
	{
		private readonly Math.Interval allowedTiltAngles = new Math.Interval(15, 80);
		private readonly Math.Interval allowedFOVs = new Math.Interval(7, 65);
		private const float tiltSpeed = 16f;
		private const float zoomSpeed = 1000f;

		private void OnEnable()
		{
			EventManager.Instance.AddListener<Controller.TiltCommandIssued>(OnTiltCommand);
			EventManager.Instance.AddListener<Controller.ZoomCommandIssued>(OnZoomCommand);
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<Controller.TiltCommandIssued>(OnTiltCommand);
			EventManager.Instance.RemoveListener<Controller.ZoomCommandIssued>(OnZoomCommand);
		}

		private void OnTiltCommand(Controller.TiltCommandIssued tiltCommand)
		{
			PerformCameraTilt(tiltCommand.tiltDirection);
		}

		private void OnZoomCommand(Controller.ZoomCommandIssued zoomCommand)
		{
			PerformCameraZoom(zoomCommand.zoomDirection);
		}

		private void PerformCameraTilt(int screenBorderDirection)
		{
			Vector3 eulerRotation = new Vector3(0, 0, 0);
			eulerRotation.x += screenBorderDirection * tiltSpeed * Time.deltaTime;

			float newTiltAngle = Camera.main.transform.localEulerAngles.x + eulerRotation.x;
			if (allowedTiltAngles.Contains(newTiltAngle))
			{
				Camera.main.transform.Rotate(eulerRotation);
			}
		}

		private void PerformCameraZoom(float zoomDirection)
		{
			float FOVChange = zoomDirection * zoomSpeed * Time.deltaTime;
			float newFOV = Camera.main.fieldOfView - FOVChange;
			if (allowedFOVs.Contains(newFOV))
			{
				Camera.main.fieldOfView = newFOV;
			}
		}
	}
}