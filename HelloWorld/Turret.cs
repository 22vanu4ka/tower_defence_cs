using System.IO.Compression;
using System.Numerics;
using System.Runtime.InteropServices;
using Raylib_cs;

namespace HelloWorld;

public class Turret
{
    private Vector2 position;
    private double[] timer = new double[2] {0.3, 0.3};
    private Enemy target = null;
    private float radius = 100.0f;
    //private List<Bullet> bullets = new List<Bullet>();
    //private List<Enemy> currEnemy;

    public Turret(Vector2 position)
    {
        this.position = position;
    }

    public void Update(double delta)
    {
        timer[0] -= delta;
        
        float closestDistance = radius;
        Enemy closestEnemy = null;

        foreach (Enemy e in GameState.foes)
        {
            if (e.hp <= 0) continue;

            float dist = Utils.Distance(position, e.position);
            if (dist <= radius && dist <= closestDistance)
            {
                closestDistance = dist;
                closestEnemy = e;
            }
        }

        target = closestEnemy;
    
        if (target != null && timer[0] <= 0)
        {
            Vector2 tVector = Utils.DirectionTo(position, target.position);
            Bullet newBullet = new Bullet(position, tVector);
            GameState.bullets.Add(newBullet);
            timer[0] = timer[1];
        }
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, 10, Color.DarkBlue);
        Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, Color.Green);

        if (target != null)
        {
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)target.position.X, (int)target.position.Y, Color.Red);
            Raylib.DrawCircle((int)position.X, (int)position.Y, 10, Color.Red);
        }
        
    }
    
}