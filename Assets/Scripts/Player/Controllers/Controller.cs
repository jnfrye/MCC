using UnityEngine;

namespace MCC
{ 
	public class Controller : MonoBehaviour
	{
		public class YawCommandIssued : GameEvent
		{
			public float yawDirection;

			public YawCommandIssued(float yawDirection)
			{
				this.yawDirection = yawDirection;
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