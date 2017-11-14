namespace MCC
{ 
	public static class GlobalConstants
	{	// TODO: The goal should always be to keep this as small as possible!
		public static class Camera
		{
			public const float TiltSpeed = 16f;
			public const float ZoomSpeed = 1000f;
			public const float RotationSpeed = 1000f;

			public static readonly Math.Interval AllowedTiltAngles = new Math.Interval(15, 80);
			public static readonly Math.Interval AllowedFOVs = new Math.Interval(7, 65);

			// TODO Gotta clean up this crap.
			// TODO I want to make a class that represents floats with values between 0 and 1
			public const float ScreenBorderThickness = 0.13f;

			public static readonly Math.Interval TopOfScreen = new Math.Interval(0, ScreenBorderThickness);
			public static readonly Math.Interval BottomOfScreen = new Math.Interval(1 - ScreenBorderThickness, 1);
		}
	}
}