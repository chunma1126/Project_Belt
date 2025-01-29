using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class KnightUnit : Unit
{
    //기사는 공속이 빨라지면서 대미지가 쎄질거임
    [Header("ActiveSkill Info")] 
   
    public float period = 7;
    public StatSO activeSkillAttackSpeedStat;
    public StatSO activeSkillAttackStat;

    private EntityStatController _statController;
    
    private void Start()
    {
        _statController = GetCompo<EntityStatController>();
    }
    
    protected override void Update()
    {
        base.Update();
    }

    protected override void ActiveSkill()
    {
        StartCoroutine(IActiveSkillRoutine());
    }

    private IEnumerator IActiveSkillRoutine()
    {
        ActiveSkillOnEvent?.Invoke();
        
        _statController.AddValue(activeSkillAttackSpeedStat.StatType , activeSkillAttackSpeedStat.GetValue());  
        _statController.AddValue(activeSkillAttackStat.StatType , activeSkillAttackStat.GetValue());  
        
        yield return new WaitForSeconds(period);
        ActiveSkillOffEvent?.Invoke();
        
        _statController.RemoveValue(activeSkillAttackSpeedStat.StatType , activeSkillAttackSpeedStat.GetValue());  
        _statController.RemoveValue(activeSkillAttackStat.StatType , activeSkillAttackStat.GetValue()); 
    }
    
    
    
    
}
