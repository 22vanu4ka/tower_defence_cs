using Raylib_cs;
using System.Numerics;

namespace HelloWorld;

public class DoubleTurret : Turret
{
    public override string name => "x2Turret";
    public override int price => 225;
    
    private Enemy secondTarget = null;
    private double[] secondTimer = new double[2] {0.3, 0.3};
    public DoubleTurret(Vector2 position) : base(position)
    {
        
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        secondTimer[0] -= delta;
        float closestDistance = radius;
        Enemy closestEnemy = null;

        foreach (Enemy e in GameState.foes)
        {
            if (e.hp <= 0) continue;

            float dist = Utils.Distance(position, e.position);
            if (dist <= radius && dist <= closestDistance && e != target)
            {
                closestDistance = dist;
                closestEnemy = e;
            }
        }

        secondTarget = closestEnemy;
    
        if (secondTarget != null && secondTimer[0] <= 0)
        {
            Vector2 tVector = Utils.DirectionTo(position, secondTarget.position);
            Bullet newBullet = new Bullet(position, tVector);
            GameState.bullets.Add(newBullet);
            secondTimer[0] = secondTimer[1];
        }
    }

    public override void Draw()
    {
        base.Draw();
        
        if (secondTarget != null)
        {
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)secondTarget.position.X, (int)secondTarget.position.Y, Color.Red);
            redAspect = Math.Min(255, redAspect + 10);
        }
        else
        {
            redAspect = Math.Max(0, redAspect - 10);
        }
        
        Raylib.DrawText("x2", (int)position.X - 5, (int)position.Y - 5, 10, Color.Black);
    }
}