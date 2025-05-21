using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

class Program
{
    
    public static void Main()
    {
        const double deltaTime = 1 / 60.0;
        double[] passiveMoneyTimer = new double[2] {1, 1};
        double[] enemyTimer = new double[2] {5, 5};
        
        Vector2[] p =
        {
            new Vector2(100, 100), 
            new Vector2(500, 100), 
            new Vector2(500, 500),
        };
        
        GameState.foes.Add(new Enemy(Vector2.Zero, p));
        //GameState.turrets.Add(new Turret(new Vector2(120, 120)));
        //GameState.turrets.Add(new Turret(new Vector2(520, 80)));
        GameState.blanks.Add(new Blank(new Vector2(470, 480)));
        GameState.blanks.Add(new Blank(new Vector2(520, 80)));
        GameState.blanks.Add(new Blank(new Vector2(120, 120)));
        GameState.blanks.Add(new Blank(new Vector2(280, 120)));
        GameState.blanks.Add(new Blank(new Vector2(320, 100)));
        
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(800, 600, "Hello World");

        while (!Raylib.WindowShouldClose())
        {
            passiveMoneyTimer[0] -= deltaTime;
            enemyTimer[0] -= deltaTime;
            
            foreach (Blank b in GameState.blanks)
            {
                b.Update(1 / 60);
                if (b.alive == false)
                {
                    GameState.blanks.Remove(b);
                    break;
                }
            }

            if (enemyTimer[0] <= 0)
            {
                Enemy newEnemy = new Enemy(Vector2.Zero, p);
                GameState.foes.Add(newEnemy);
                enemyTimer[0] = enemyTimer[1];
            }
            
            if (passiveMoneyTimer[0] <= 0)
            {
                GameState.money += GameState.passiveMoney;
                passiveMoneyTimer[0] = passiveMoneyTimer[1];
            }

            foreach (Turret t in GameState.turrets)
            {
                t.Update(deltaTime);
            }

            foreach (Enemy e in GameState.foes)
            {
                e.Update((float)1 / 60);
                if (e.hp <= 0)
                {
                    GameState.foes.Remove(e);
                    GameState.money += e.reward + e.currentIdx;
                    break;
                }
            }
            
            foreach (Bullet b in GameState.bullets)
            {
                b.Update();
            }


            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText($"{GameState.money}$ + {GameState.passiveMoney}$/sec.", 12, 600-12*2, 20, Color.Black);

            for (int i = 1; i < p.Length; i++)
            {
                Raylib.DrawLine((int)p[i].X, (int)p[i].Y, (int)p[i-1].X, (int)p[i-1].Y, Color.Black);
            }
            
            foreach (Turret t in GameState.turrets)
            {
                t.Draw();
            }

            foreach (Enemy e in GameState.foes)
            {
                e.Draw();
            }
            
            foreach (Bullet b in GameState.bullets)
            {
                b.Draw();
            }

            foreach (Blank b in GameState.blanks)
            {
                b.Draw();
            }
            
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}