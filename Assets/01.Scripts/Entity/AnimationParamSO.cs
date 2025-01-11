using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AnimParam" , fileName = "_AnimationParam")]
public class AnimationParamSO : ScriptableObject
{
    public string animationName;
    public int hashValue; 
    
    private void OnValidate()
    {
        if(animationName != null)
            hashValue = Animator.StringToHash(animationName);
    }
}
