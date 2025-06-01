using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

public class Ally : Enemy
{
    public override int hp { get; set; } = 20;
    private Texture2D texture = Raylib.LoadTexture("Sprites/robot.png");
    public Ally(Vector2 position, Vector2[] path) : base(position, path)
    {
        this.path = (Vector2[])path.Clone();
        Array.Reverse(this.path);
        
        float minDist = float.MaxValue;
        int closestIdx = 0;
        
        for (int i = 0; i < this.path.Length; i++)
        {
            if (Utils.Distance(position, this.path[i]) <= minDist)
            {
                minDist = Utils.Distance(position, this.path[i]);
                closestIdx = i;
            }
        }
        
        currentIdx = closestIdx;
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        
        foreach (Enemy e in GameState.foes)
        {
            if (Utils.Distance(position, e.position) < radius + 1.5f)
            {
                e.hp -= 1;
                hp -= 1;
                position -= target * 15;
                e.position -= e.target * 15;
            }
        }
    }

    public override void Draw()
    {
        Raylib.DrawTextureEx(texture, position - new Vector2(radius / 2, radius / 2), 0.0f, 0.25f, Color.White);
    }
}