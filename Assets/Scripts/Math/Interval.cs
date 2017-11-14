namespace MCC.Math 
{
	public struct Interval
	{
		private float min;
		private float max;

		public Interval(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		public bool Contains(float x)
		{
			return x >= min && x <= max;
		}
	}
}