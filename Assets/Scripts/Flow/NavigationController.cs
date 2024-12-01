using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class NavigationController: MonoBehaviour
{
    private GameState currentState = GameState.Intro;
    [SerializeField] private NavigationData[] _dataDictionnary;
    private Dictionary<GameState, NavigationData> dataDictionnary;

    [Serializable]
    public class NavigationData
    {
        public GamePart Part;
    }

    public void GoToState(GameState nextState)
    {
        dataDictionnary[currentState].Part.EndPart();
        dataDictionnary[nextState].Part.StartPart();
        currentState = nextState;
    }

    public void Init()
    {
        currentState = GameState.Intro;
        dataDictionnary[GameState.Intro].Part.StartPart();
    }

    private void Awake()
    {
        dataDictionnary = _dataDictionnary.ToDictionary(data => data.Part.gameState, data => data);
    }
}