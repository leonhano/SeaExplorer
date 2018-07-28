using UnityEngine;
using System.Collections;

public class HumanUnit : Entity
{
#region Serializable Properties
    public Vector2 mHealth = new Vector2(100, 100); //(current health, maximum health)
    public LevelExp mLevelExp = new LevelExp(0, 0);     //level exp; 

    public SkillAbility mSkillAbilities;      //skills abilities
#endregion


	// Use this for initialization
	void Start () {
        InitSkills();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected virtual void InitSkills(){}
}
