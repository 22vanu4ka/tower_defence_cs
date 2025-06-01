using System.Numerics;
using HelloWorld.Foes;
using Raylib_cs;

namespace HelloWorld;

class Program
{
    
    public static void Main()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(800, 600, "Hello World");
        const double deltaTime = 1 / 60.0;
        double[] passiveMoneyTimer = new double[2] {1, 1};
        double[] enemyTimer = new double[2] {1, 1};
        
        GameState.foes.Add(new Enemy(Vector2.Zero, GameState.p));
        //GameState.turrets.Add(new Turret(new Vector2(120, 120)));
        //GameState.turrets.Add(new Turret(new Vector2(520, 80)));
        //GameState.turrets.Add(new DoubleTurret(new Vector2(120, 120)));
        GameState.blanks.Add(new Blank(new Vector2(135, 279)));
        GameState.blanks.Add(new Blank(new Vector2(135, 400)));
        GameState.blanks.Add(new Blank(new Vector2(425, 479)));
        GameState.blanks.Add(new Blank(new Vector2(670, 300)));
        
        
        

        while (!Raylib.WindowShouldClose())
        {
            //Update
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
                Enemy newEnemy = null;
                if (GameState.foes.Count % 3 == 0)
                {
                    newEnemy = new StrongOne(Vector2.Zero, GameState.p);
                }
                else if (GameState.foes.Count % 4 == 0)
                {
                    newEnemy = new FastOne(Vector2.Zero, GameState.p);
                }
                else
                {
                    newEnemy = new Enemy(Vector2.Zero, GameState.p);
                }
                
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
                if (!t.alive)
                {
                    GameState.turrets.Remove(t);
                    break;
                }
            }

            foreach (Enemy e in GameState.foes)
            {
                e.Update((float)1 / 60);
                //12312312312312312312312312
                if (e.hp <= 0)
                {
                    Enemy.Info newinfo = new Enemy.Info(e.position);
                    GameState.etc.Add(newinfo);
                    GameState.foes.Remove(e);
                    GameState.money += e.reward + e.currentIdx;
                    break;
                }
            }
            
            foreach (Bullet b in GameState.bullets)
            {
                b.Update();
            }
            
            foreach (Ally e in GameState.allies)
            {
                e.Update((float)1 / 60);
                if (e.hp <= 0)
                {
                    GameState.allies.Remove(e);
                    break;
                }
            }


            //Draw
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText($"{GameState.money}$ + {GameState.passiveMoney}$/sec.", 12, 600-12*2, 20, Color.Black);

            for (int i = 1; i < GameState.p.Length; i++)
            {
                
                Raylib.DrawLine((int)GameState.p[i].X, (int)GameState.p[i].Y, (int)GameState.p[i-1].X, (int)GameState.p[i-1].Y, Color.Black);
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
                if (b.visible) b.Draw();
            }

            foreach (Blank b in GameState.blanks)
            {
                b.Draw();
            }

            foreach (Enemy.Info e in GameState.etc)
            {
                e.Draw();
            }

            foreach (Ally a in GameState.allies)
            {
                a.Draw();
            }
            
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}