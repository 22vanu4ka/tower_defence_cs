using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

public class Forge : Turret
{
    public override double coolDown => 4;
    public override string name => "Forge";
    public override Texture2D texture => Raylib.LoadTexture("Sprites/forge.png");

    public Forge(Vector2 position) : base(position)
    {
        
    }

    public override void Update(double delta)
    {
        timer[0] -= delta;

        if (timer[0] <= 0)
        {
            Ally newAlly = new Ally(position, GameState.p);
            GameState.allies.Add(newAlly);
            Console.WriteLine("New ally");
            timer[0] = timer[1];
        }
    }

    
}