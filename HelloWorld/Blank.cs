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
    private Func<Vector2, Turret>[] options = new Func<Vector2, Turret>[]
    {
        pos => new Turret(pos),
        pos => new DoubleTurret(pos),
        pos => new LaserTurret(pos),
        pos => new Forge(pos)
    };

    private Option[] optionBubbles;
    
    public Blank(Vector2 position)
    {
        this.position = position;
        optionBubbles = new Option[options.Length];
        for (int i = 0; i < optionBubbles.Length; i++)
        {
            Turret bubble = options[i](new Vector2(-200, -200));
            int angle = 360 / options.Length;
            Vector2 bubblePos = new Vector2((int)position.X + 30*MathF.Cos(angle*i*Raylib.DEG2RAD), (int)position.Y + 30*MathF.Sin(angle*i*Raylib.DEG2RAD));
            Option newOption = new Option(new Vector2(bubblePos.X, bubblePos.Y), bubble.name, i, bubble.price+"");
            optionBubbles[i] = newOption;
        }
    }

    public void Update(double delta)
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left) && GameState.money >= price && Utils.Distance(position, Raylib.GetMousePosition()) < radius)
        {
            //Turret newTurret = new Turret(position);
            //GameState.turrets.Add(newTurret);
            //GameState.money -= price;
            //alive = false;
        }

        foreach (Option o in optionBubbles)
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left) && Utils.Distance(o.pos, Raylib.GetMousePosition()) < 15 && o.alpha >= 125 && Convert.ToInt32(o.bottomText) <= GameState.money)
            {
                Turret newTurret = options[o.optionIdx](position);
                GameState.turrets.Add(newTurret);
                alive = false;
                GameState.money -= Convert.ToInt32(o.bottomText);
            }
        }
        
    }

    public void Draw()
    {
        Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, Color.Black);
        Raylib.DrawLine((int)position.X - radius, (int)position.Y, (int)position.X + radius, (int)position.Y, Color.Green);
        Raylib.DrawLine((int)position.X, (int)position.Y + radius, (int)position.X, (int)position.Y - radius, Color.Green);
        //Raylib.DrawText($"{100}$", (int)position.X - radius*2, (int)position.Y - radius*3, 18, new Color(0, 0, 0, alphaText));

        if (Utils.Distance(position, Raylib.GetMousePosition()) < radius*4)
        {
            alphaText = Math.Min(255, alphaText + 10);
        }
        else
        {
            alphaText = Math.Max(0, alphaText - 10);
        }

        for (int i = 0; i < optionBubbles.Length; i++)
        {
            optionBubbles[i].alpha = alphaText;
            optionBubbles[i].Draw();
        }

    }
    
    private class Option
    {
        public Vector2 pos;
        public int alpha = 0;
        public string text;
        public string bottomText;
        public int optionIdx;

        public Option(Vector2 pos, string text, int optionIdx, string bottomText)
        {
            this.pos = pos;
            this.text = text;
            this.optionIdx = optionIdx;
            this.bottomText = bottomText;
        }

        public void Update(double delta)
        {

        }

        public void Draw()
        {
            Raylib.DrawCircle((int)pos.X, (int)pos.Y, 15, new Color(255, 150, 0, alpha));
            Raylib.DrawText(text, (int)pos.X - 20, (int)pos.Y - 10, 10, new Color(0, 0, 0, alpha));
            Raylib.DrawText($"{bottomText}$", (int)pos.X - 10, (int)pos.Y - 2, 10, new Color(0, 0, 0, alpha));
        }
    }
}