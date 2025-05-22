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
    public override int damage => 3;

    public LaserTurret(Vector2 position) : base(position)
    {
        
    }

    public override void Draw()
    {
        base.Draw();
        float t = MathF.Sin((float)Raylib.GetTime() * 5) * 0.25f;
    
        if (target != null)
        {
            Vector2 tVector = Utils.DirectionTo(position, target.position);
            Vector2 end = position + tVector * 210.0f;
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)end.X, (int)end.Y, Color.Purple);
            Raylib.DrawCircle((int)target.position.X, (int)target.position.Y, 17 + 4 * t, Color.Purple);
        }
    }

}