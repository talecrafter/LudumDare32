using UnityEngine;
public enum Direction
{
	Left,
	Right,
	None
}

public static class DirectionStatic
{
	public static Direction Flip(this Direction direction)
	{
		if (direction == Direction.Right)
		{
			return Direction.Left;
		}
		else
		{
			return Direction.Right;
		}
	}

	public static Direction GetRandom()
	{
		if (Random.Range(0, 2) == 0)
			return Direction.Left;
		else
			return Direction.Right;
	}
}