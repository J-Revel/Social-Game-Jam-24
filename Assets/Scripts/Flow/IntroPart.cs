using UnityEngine;
using TMPro;

public class IntroPart : GamePart
{
    public override void EndPart()
    {
        this.gameObject.SetActive(false);
    }   

    public override void StartPart()
    {
        HelpManager.Reset();
        this.gameObject.SetActive(true);
    }

    public void Next()
    {
        Global.GoToState(GameState.EventSelect);
    }  
}