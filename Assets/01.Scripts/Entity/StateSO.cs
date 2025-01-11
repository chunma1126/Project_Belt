using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "SO/StateSO")]
public class StateSO : ScriptableObject
{
    public string stateName;
    public string className;
    
    public AnimationParamSO stateAnimationParam;
    
}
