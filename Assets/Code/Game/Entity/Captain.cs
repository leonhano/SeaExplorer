using UnityEngine;
using System.Collections;
using System;

public class Captain : HumanUnit
{
    SkillAbility.SkillValue mleadershipSkill = null;       

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //init skills
    protected override void InitSkills()
    {
        //add leadership skill
        float leadshipRate = 0.1f;//leadership , In percent: 0.1 means that earn extra 10% exp
        mleadershipSkill = new SkillAbility.SkillValue(SkillAbility.SkillEnum.Leadership, leadshipRate, ApplyLeadershipCallback);
        mSkillAbilities.AddSkill(mleadershipSkill);

    }

    //leadership call back;
    void ApplyLeadershipCallback()
    {
        int extraExp = Mathf.FloorToInt(((float)mleadershipSkill.callbackArg) * mleadershipSkill.value);
        mLevelExp.mExp += Convert.ToUInt32(extraExp);
    }

    //earn exp
    void EarnExp(UInt32 exp)
    {
        mLevelExp.mExp += exp;
        mSkillAbilities.ApplySkill(SkillAbility.SkillEnum.Leadership, exp);//Apply leadership skill ability
    }
}
