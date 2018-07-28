using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Sell value define;
/// </summary>
public sealed class SellDefine
{
    float mSoldValue;    //sold value;
    bool mCanBeSold;     //can be sold;

    public SellDefine(float soldValue, bool canBeSold)
    {
        mCanBeSold = canBeSold;
        mSoldValue = soldValue;
    }

    public float Sold()
    {
        if (mCanBeSold)
            return mSoldValue;
        else
            return 0;
    }
}

/// <summary>
/// Level exp
/// </summary>
public sealed class LevelExp : IEntity
{
    public UInt32 mLevel;
    public UInt32 mExp;

    public LevelExp(UInt32 level, UInt32 exp)
    {
        mLevel = level;
        mExp = exp;
    }
}

/// <summary>
/// SkillAbility
/// </summary>
public sealed class SkillAbility : IEntity
{
    //store skills;
    public Dictionary<SkillEnum, SkillValue> mSkillAbilities = new Dictionary<SkillEnum, SkillValue>();

    //delegate for skill callback
    public delegate void SkillAbilityCallBack();
    public delegate void SkillAbilityCallBack<T>(T arg);
    
    public enum SkillEnum{
        NULL = -1,
        Leadership = 1,
        Driver,
        Fight,
        Defence,
        ExploreView,
    }

    public sealed class SkillValue
    {
        public SkillEnum ID = SkillEnum.NULL;
        public float value = float.NaN;
        public SkillAbilityCallBack callback = null;
        public float callbackArg = float.NaN;

        public SkillValue(SkillEnum se, float v, SkillAbilityCallBack cb)
        {
            ID = se;
            value = v;
            callback = cb;
        } 

        public override string ToString()
        {
            string res = ID.ToString();
            res += " = ";
            res += value;
            res += " --> callback : ";
            if(callback != null)
                res += callback.Method.Name;
            return res;
        }

        public void OnCallback(float data)
        {
            callbackArg = data;
            callback();
        }
    }

    
    //add skill;
    public void AddSkill(SkillValue skillValue)
    {
        if (mSkillAbilities.ContainsKey(skillValue.ID))
            mSkillAbilities[skillValue.ID] = skillValue;
        else
            mSkillAbilities.Add(skillValue.ID, skillValue);        
    }

    public void AddSkill(SkillEnum skill, float value, SkillAbilityCallBack callback)
    {
        SkillValue skillValue = new SkillValue(skill, value, callback);
        AddSkill(skillValue);
    }
    
    //apply skill;
    public void ApplySkills()
    {
        foreach (KeyValuePair<SkillEnum, SkillValue> pair in mSkillAbilities)
        {
            SkillValue skillValue = pair.Value;
            skillValue.callback();

            Debug.Log("Apply skill --> " +skillValue.ToString());
        }
    }

    //apply a skill;
    public bool ApplySkill(SkillEnum skill)
    {
        if (mSkillAbilities.ContainsKey(skill))
        {
            mSkillAbilities[skill].callback();
            return true;
        }
        {
            Debug.LogWarning("No this skill: " + skill.ToString());
            return false;
        }
    }
    public bool ApplySkill(SkillEnum skill, float data)
    {
        if (mSkillAbilities.ContainsKey(skill))
        {
            mSkillAbilities[skill].OnCallback(data);
            return true;
        }
        {
            Debug.LogWarning("No this skill: " + skill.ToString());
            return false;
        }
    }
    
    /*
    #region SkillValueT
    public Dictionary<SkillEnum, object> mSkillTAbilities = new Dictionary<SkillEnum, object>();

    public sealed struct SkillValueT<T>
    {
        public SkillEnum ID = SkillEnum.NULL;
        public float value = float.NaN;
        public SkillAbilityCallBack<T> callback = null;
        public SkillValueT(SkillEnum se, float v, SkillAbilityCallBack<T> cb)
        {
            ID = se;
            value = v;
            callback = cb;
        }

        public override string ToString()
        {
            string res = ID.ToString();
            res += " = ";
            res += value;
            res += " --> callback : ";
            if (callback != null)
                res += callback.Method.Name;
            return res;
        }
    }
    public void AddSkill<T>(SkillEnum skill, float value, SkillAbilityCallBack<T> callback)
    {
        SkillValueT<T> skillValueT = new SkillValueT<T>(skill, value, callback);
        AddSkill<T>(skillValueT);
    }
    public void AddSkill<T>(SkillValueT<T> skillValueT)
    {
        if (mSkillAbilities.ContainsKey(skillValueT.ID))
            mSkillTAbilities[skillValueT.ID] = skillValueT;
        else
            mSkillTAbilities.Add(skillValueT.ID, skillValueT);
    }
    public bool ApplySkill<T>(SkillEnum skill, T data)
    {
        if (mSkillTAbilities.ContainsKey(skill))
        {
            object value = mSkillTAbilities[skill];
            ((SkillValueT<T>) value).callback(data);
            return true;
        }
        else
        {
            Debug.LogWarning("No this skill: " + skill.ToString());
            return false;
        }
    }
    #endregion
     * */
}
