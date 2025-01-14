using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "SO/State")]
public class StateSO : ScriptableObject
{
    public string stateName;
    public string className;
    
    public AnimationParamSO stateAnimationParam;
    
}
