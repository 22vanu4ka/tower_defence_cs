using System.Numerics;

namespace HelloWorld;

public class GameState
{
    public static List<Bullet> bullets = new List<Bullet>();
    public static List<Enemy> foes = new List<Enemy>();
    public static List<Turret> turrets = new List<Turret>();
    public static List<Blank> blanks = new List<Blank>();
    public static List<Enemy.Info> etc = new List<Enemy.Info>();
    public static int money = 500;
    public static int passiveMoney = 2;
    public static Vector2[] p =
    {
        new Vector2(70, 300), 
        new Vector2(200, 300), 
        new Vector2(200, 500),
        new Vector2(650, 500),
        new Vector2(400, 300),
        new Vector2(650, 300)
    };
    public static List<Ally> allies = new List<Ally>();
}