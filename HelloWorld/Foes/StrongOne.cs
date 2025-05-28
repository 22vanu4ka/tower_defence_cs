using System.Numerics;
using Raylib_cs;

namespace HelloWorld.Foes;

public class StrongOne : Enemy
{
    public override int hp { get; set; } = 25;
    public override int radius => 20;
    public override float speed => 1.25f;

    public StrongOne(Vector2 position, Vector2[] path) : base(position, path)
    {
        
    }

    public override void Draw()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, radius, Color.Green);
        Raylib.DrawText($"{hp}", (int)position.X - 10, (int)position.Y - 10, 15, Color.Black);
    }
}