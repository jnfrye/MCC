using UnityEngine;

public class Controller : MonoBehaviour
{
	public delegate void MovementAction(Vector3 movementDirection);
	public event MovementAction MovementCommand = (_) => { };
	protected void OnMovementCommand(Vector3 movementDirection) { MovementCommand(movementDirection); }

	public delegate void YawAction(float yawDirection);
	public event YawAction YawCommand = (_) => { };
	protected void OnYawCommand(float yawDirection) { YawCommand(yawDirection); }

	public delegate void ZoomAction(float zoomDirection);
	public event ZoomAction ZoomCommand = (_) => { };
	protected void OnZoomCommand(float zoomDirection) { ZoomCommand(zoomDirection); }

	public delegate void TiltAction(int tiltDirection);
	public event TiltAction TiltCommand = (_) => { };
	protected void OnTiltCommand(int tiltDirection) { TiltCommand(tiltDirection); }
}
