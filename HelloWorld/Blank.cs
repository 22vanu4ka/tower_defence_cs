using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

public class Blank
{
    private Vector2 position;
    public bool alive = true;
    public int price = 100;
    
    private int alphaText = 0;
    private int radius = 10;
    
    public Blank(Vector2 position)
    {
        this.position = position;
    }

    public void Update(double delta)
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left) && GameState.money >= price && Utils.Distance(position, Raylib.GetMousePosition()) < radius)
        {
            Turret newTurret = new Turret(position);
            GameState.turrets.Add(newTurret);
            GameState.money -= price;
            alive = false;
        }
    }

    public void Draw()
    {
        Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, Color.Black);
        Raylib.DrawLine((int)position.X - radius, (int)position.Y, (int)position.X + radius, (int)position.Y, Color.Green);
        Raylib.DrawLine((int)position.X, (int)position.Y + radius, (int)position.X, (int)position.Y - radius, Color.Green);
        Raylib.DrawText($"{100}$", (int)position.X - radius*2, (int)position.Y - radius*3, 18, new Color(0, 0, 0, alphaText));

        if (Utils.Distance(position, Raylib.GetMousePosition()) < radius)
        {
            alphaText = Math.Min(255, alphaText + 10);
        }
        else
        {
            alphaText = Math.Max(0, alphaText - 10);
        }

    }
}