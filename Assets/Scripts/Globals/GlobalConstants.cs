namespace MCC
{ 
	public static class GlobalConstants
	{	// TODO: The goal should always be to keep this as small as possible!
		public static class Screen
		{
			public const float BorderThickness = 0.13f;

			public static readonly Math.Interval TopBorder = new Math.Interval(0, BorderThickness);
			public static readonly Math.Interval BottomBorder = new Math.Interval(1 - BorderThickness, 1);
		}
	}
}