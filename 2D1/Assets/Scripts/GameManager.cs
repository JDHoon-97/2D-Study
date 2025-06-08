using UnityEngine;

public class GameManager
{
    private static GameManager _instance = new GameManager();
    
    public static GameManager Instance { get { return _instance; } }

    public Player Player;
}
