using UnityEngine;

namespace MCC
{ 
	public class Controller : MonoBehaviour
	{
		public delegate void YawAction(float yawDirection);
		public event YawAction YawCommand = (_) => { };
		protected void OnYawCommand(float yawDirection) { YawCommand(yawDirection); }

		public class ZoomCommandIssued : GameEvent
		{
			public float zoomDirection;

			public ZoomCommandIssued(float zoomDirection)
			{
				this.zoomDirection = zoomDirection;
			}
		}

		public class TiltCommandIssued : GameEvent
		{
			public int tiltDirection;

			public TiltCommandIssued(int tiltDirection)
			{
				this.tiltDirection = tiltDirection;
			}
		}

		public class MovementCommandIssued : GameEvent
		{
			public Vector3 movementDirection;

			public MovementCommandIssued(Vector3 movementDirection)
			{
				this.movementDirection = movementDirection;
			}
		}
	}
}