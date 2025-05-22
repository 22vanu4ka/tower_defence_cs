using System.IO.Compression;
using System.Numerics;
using System.Runtime.InteropServices;
using Raylib_cs;

namespace HelloWorld;

public class Turret
{
    public virtual string name => "Turret";
    public virtual int price => 75;
    public virtual float radius => 100.0f;
    public virtual bool bulletVsibility => true;
    public virtual bool bulletPiercing => false;
    public virtual int damage => 2;
    
    protected Vector2 position;
    public bool alive = true;
    protected double[] timer = new double[2] {0.3, 0.3};
    protected Enemy target = null;
    
    protected int redAspect = 0;
    

    public Turret(Vector2 position)
    {
        this.position = position;
    }

    public virtual void Update(double delta)
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
            newBullet.visible = bulletVsibility;
            newBullet.pierce = bulletPiercing;
            newBullet.damage = damage;
            GameState.bullets.Add(newBullet);
            timer[0] = timer[1];
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Right) &&
            Utils.Distance(position, Raylib.GetMousePosition()) <= radius)
        {
            Blank newBlank = new Blank(position);
            GameState.blanks.Add(newBlank);
            GameState.money += price / 3;
            alive = false;
        }
    }

    public virtual void Draw()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, 10, new Color(redAspect, 0, 255-redAspect, 255));
        Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, Color.Green);

        if (target != null)
        {
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)target.position.X, (int)target.position.Y, Color.Red);
            redAspect = Math.Min(255, redAspect + 10);
        }
        else
        {
            redAspect = Math.Max(0, redAspect - 10);
        }
        
    }
    
}