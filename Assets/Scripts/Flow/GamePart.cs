using UnityEngine;

public enum GameState {None, Intro, Presentation, Play, Conclusion, EventSelect};
public abstract class GamePart : MonoBehaviour
{
    public GameState gameState;
    public abstract void StartPart();
    public abstract void EndPart();
}
