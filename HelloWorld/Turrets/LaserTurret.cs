using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

public class LaserTurret : Turret
{
    public override int price => 200;
    public override float radius => 210.0f;
    public override string name => "Laser T";
    public override bool bulletPiercing => true;
    public override bool bulletVsibility => false;
    public override int damage => 1;
    public override double coolDown => 0.9;
    public override float rotateSpeed => 0.0f;
    private double[] secondTimer = new double[2] {0.3, 0.3};

    private Enemy secondTarget = null;

    public LaserTurret(Vector2 position) : base(position)
    {
        
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        
        //Вынеси ради бога в отдельную функцию
        secondTimer[0] -= delta;

        float closestDistance = radius;
        Enemy closestEnemy = null;
        
        foreach (Enemy e in GameState.foes)
        {
            if (e.hp <= 0 || e == target) continue;

            float dist = Utils.Distance(position, e.position);
            if (dist <= radius && dist <= closestDistance)
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
            newBullet.visible = bulletVsibility;
            newBullet.pierce = bulletPiercing;
            newBullet.damage = damage;
            GameState.bullets.Add(newBullet);
            secondTimer[0] = secondTimer[1];
        }
    }

    public override void Draw()
    {
        base.Draw();
        float t = MathF.Sin((float)Raylib.GetTime() * 5) * 0.25f;
    
        if (target != null)
        {
            Vector2 tVector = Utils.DirectionTo(position, targetPos);
            float dist = Utils.Distance(position, targetPos);
            Vector2 end = position + tVector * dist;
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)end.X, (int)end.Y, Color.Purple);
            Raylib.DrawCircle((int)target.position.X, (int)target.position.Y, target.radius + 10 * t, Color.Purple);
        }

        if (secondTarget != null)
        {
            Vector2 tVector = Utils.DirectionTo(position, secondTarget.position);
            float dist = Utils.Distance(position, secondTarget.position);
            Vector2 end = position + tVector * dist;
            Raylib.DrawLine((int)target.position.X, (int)target.position.Y, (int)end.X, (int)end.Y, Color.Purple);
            Raylib.DrawCircle((int)secondTarget.position.X, (int)secondTarget.position.Y, secondTarget.radius + 10 * t, Color.Purple);
        }
    }

}