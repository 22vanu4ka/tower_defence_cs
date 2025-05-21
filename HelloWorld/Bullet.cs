using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

public class Bullet
{

    public Vector2 position;
    public Vector2 velocity;
    public int speed = 8;

    public Bullet(Vector2 position, Vector2 velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }

    public void Update()
    {
        position += velocity * speed;
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, 4, Color.Orange);
    }
}