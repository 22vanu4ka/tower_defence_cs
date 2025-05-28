using System.Numerics;
using Raylib_cs;

namespace HelloWorld.Foes;

public class FastOne : Enemy
{
    public override int hp { get; set; } = 7;
    public override int radius => 12;
    public override float speed => 2f;

    public FastOne(Vector2 position, Vector2[] path) : base(position, path)
    {
        
    }

    public override void Draw()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, radius, Color.Blue);
        Raylib.DrawText($"{hp}", (int)position.X - radius / 2, (int)position.Y - radius / 2, 15, Color.Black);

    }
}