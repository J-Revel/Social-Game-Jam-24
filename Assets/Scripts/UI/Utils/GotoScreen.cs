using UnityEngine;

public class GotoScreen : MonoBehaviour
{
    public GameState target;

    public void Goto()
    {
        
        Global.GoToState(target);
    }
}
