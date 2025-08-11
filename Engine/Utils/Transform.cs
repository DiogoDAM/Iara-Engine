using Microsoft.Xna.Framework;

namespace IaraEngine;

public class Transform
{
	public Vector2 Position;
	public float Rotation;
	public Vector2 Scale = Vector2.One;

	public Transform Parent;

	public Vector2 GlobalPosition => (Parent != null) ? Parent.Position + Position : Position;
	public float GlobalRotation => (Parent != null) ? Parent.Rotation + Rotation : Rotation;
	public Vector2 GlobalScale => (Parent != null) ? Parent.Scale + Scale : Scale;

	public Transform() { }

	public Transform(Transform parent) { Parent = parent; }

	public Transform(Vector2 pos) { Position = pos; }

	public Transform(Vector2 pos, float rotation, Vector2 scale) { Position = pos; Rotation = rotation; Scale = scale; }

	public static Vector2 MoveTowards(Vector2 start, Vector2 target, float speed)
	{
		Vector2 dir = target - start;

		if(dir.Length() <= speed) return target;

		dir.Normalize();

		return start + dir * speed;
	}

	public void MoveTowards(Vector2 target, float speed)
	{
		Position = MoveTowards(GlobalPosition, target, speed);
	}

	public static float Distance(Vector2 start, Vector2 target)
	{
		float distance = Vector2.Distance(start, target);
		return distance;
	}

	public void LookAt(Vector2 target)
	{
		Vector2 dir = target - GlobalPosition;
		Rotation = (float) System.Math.Atan2(dir.Y, dir.X);
	}

	public override string ToString() => $"(Position: {GlobalPosition}, Rotation: {GlobalRotation}, Scale: {GlobalScale})";
	
}
