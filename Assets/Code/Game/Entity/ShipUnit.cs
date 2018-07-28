using UnityEngine; 
using System.Collections.Generic;

public class ShipUnit : Entity
{
    #region serializa properites
    //
    public Vector2 mHealth = new Vector2(100, 100);  // Current and maximum hull health
    public LevelExp mLevelExp = new LevelExp(0, 0);     //level exp; 
    

    public float maxMovementSpeed = 7f;     // Units per second
    public float movementSpeed { get { return maxMovementSpeed; } }

    public float maxTurningSpeed = 60f; // Degrees per second
    public float turningSpeed { get { return maxTurningSpeed; } }

    public Dictionary<int, Entity> m_container;     //container which key = id, value = entity;

    public float mDamage = 0;       //damage
    public float mDamageReduction = 0;       //armor ,  In percent: 0.1 means that only 90% damage will be applied

    public float mConsumeRate = 1;  //consume rate;
    public float mLuckyRate = 0;        //lucky rate
    public SellDefine mSellValue = new SellDefine(1000, false);
    #endregion

    #region properties which do not need save
    public Vector3 mVelocity;

    protected Vector3 mLastPos;
    // Cache the transform for speed
    protected Transform mTrans;

    // Flag gets set to 'true' once the unit gets destroyed. Used to despawn the unit.
    bool mDestroyed = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        mTrans = transform;
        OnStart();

    }

    // Update is called once per frame
    void Update()
    {

        //if (mDestroyed && (destroyAnimation == null || !destroyAnimation.isPlaying))
        if (mDestroyed)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Calculate the ship's velocity.
    /// </summary>
    void LateUpdate()
    {
        Vector3 pos = mTrans.position;
        mVelocity = (pos - mLastPos) * (1.0f / Time.deltaTime);
        mLastPos = pos;
    }

    /// <summary>
    /// Apply the specified amount of damage to the unit. 
    /// Returns the actual amount of damage inflicted.
    /// </summary>
    public virtual float ApplyDamage(float val, GameObject go)
    {
        if (mDestroyed) return 0f;
        if (val < 0f) val = 0f;
        val *= (1.0f - mDamageReduction);
        val = Mathf.Min(mHealth.x, val);
        mHealth.x -= val;

        if (mHealth.x == 0f)
        {
            // The ship can now be considered destroyed
            mDestroyed = true;

            // Play the death animation, if any
            //if (destroyAnimation != null) destroyAnimation.Play();

            // Notify all attached scripts that the ship has been destroyed by something
            gameObject.SendMessage("OnDestroyedBy", go, SendMessageOptions.DontRequireReceiver);
        }
        return val;
    }

    /// <summary>
    /// Cache some values.
    /// </summary>

    protected void OnStart()
    {
        mLastPos = mTrans.position;
    } 
}
