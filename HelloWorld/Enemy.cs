using System.Numerics;
using System.Runtime.CompilerServices;
using HelloWorld;
using Raylib_cs;

namespace HelloWorld;

public class Enemy
{
    public Vector2 position;

    public virtual int hp { get; set; } = 11;
    public virtual int reward => 13;
    public virtual int radius => 14;

    public virtual float speed => 1.5f;
    protected Vector2[] path;
    public int currentIdx = 0;
    public Vector2 target;
    
    public Enemy(Vector2 position, Vector2[] path)
    {
        this.position = position;
        this.path = path;
    }

    public virtual void Update(float delta)
    {
        target = Utils.DirectionTo(position, path[currentIdx]);
        position += target * speed;

        if (Utils.Distance(path[currentIdx], position) < 5.0f)
        {
            currentIdx = Math.Min(currentIdx + 1, path.Length - 1);
        }

        foreach (Bullet bullet in GameState.bullets)
        {
            if (Utils.Distance(position, bullet.position) < radius + 5.0f)
            {
                hp -= bullet.damage;
                if (!bullet.pierce)
                {
                    GameState.bullets.Remove(bullet);
                    break;
                }

            }
        }
        
    }

    public virtual void Draw()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, 14, Color.Beige);
        Raylib.DrawText($"{hp}", (int)position.X - 10, (int)position.Y - 10, 15, Color.Black);
    }

    public class Info
    {
        private Vector2 position;
        private int alpha = 255;

        public Info(Vector2 position)
        {
            this.position = position;
        }

        public void Draw()
        {
            position.Y -= 2;
            alpha = Math.Max(0, alpha - 10);
            
            Raylib.DrawText("+$", (int)position.X, (int)position.Y, 12, new Color(0, 0, 0, alpha));
        }

    }
}