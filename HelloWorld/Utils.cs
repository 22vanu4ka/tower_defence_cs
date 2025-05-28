using System.Numerics;

namespace HelloWorld;

public static class Utils
{
    public static Vector2 DirectionTo(Vector2 a, Vector2 b)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;
        float dist = MathF.Sqrt(dx * dx + dy * dy);
        
        if (dist == 0) return Vector2.Zero;
        
        return new Vector2(dx / dist, dy / dist);
    }

    public static float Distance(Vector2 a, Vector2 b)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    public static Vector2 LerpV(Vector2 start, Vector2 end, float t)
    {
        return start + (end - start) * t;
    }
}