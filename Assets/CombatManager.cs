using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{

    public CombatState[] possibleStates;
    public int stateIndex;

    // Start is called before the first frame update
    void Start()
    {
        possibleStates[stateIndex].OnEnter.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValidate()
    {
        foreach (var state in possibleStates) { state.Name = state.AssociatedState.ToString(); }
    }

    public void EndState(CombatStateType combatStateType)
    {
        if(possibleStates[stateIndex].AssociatedState == combatStateType)
        {
            var newStateIndex = (stateIndex + 1) % possibleStates.Length;
            possibleStates[stateIndex].OnExit.Invoke();
            possibleStates[newStateIndex].OnEnter.Invoke();
            stateIndex = newStateIndex;
        }
    }

    public void LastState(CombatStateType combatStateType)
    {
        if (possibleStates[stateIndex].AssociatedState == combatStateType)
        {
            var newStateIndex = (stateIndex + possibleStates.Length - 1) % possibleStates.Length;
            possibleStates[stateIndex].OnExit.Invoke();
            possibleStates[newStateIndex].OnEnter.Invoke();
            stateIndex = newStateIndex;
        }
    }

}

[System.Serializable]
public class CombatState
{
    [HideInInspector] public string Name;
    public CombatStateType AssociatedState;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
}

public enum CombatStateType
{
    RollTheDice, LetTheDiceSettle, ReadySteadyAim, FireAway,EnemyApproach
}