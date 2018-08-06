using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChests : MonoBehaviour {
    
    public List<GameObject> m_chestList;
    public float m_spawnMiniDistance = 30f;
    public float m_spawnMaxDistance = 50f;
    public float m_spawnTimeStamp;

    public Dictionary<GameObject, float> m_spawnedChests = new Dictionary<GameObject, float>();
    public int SpawnCount = 3;
    
    public bool m_autoSpawn = true;

    // Use this for initialization
    void Start () {
    }

    Vector3 getSpawnPos()
    {
        Vector3 distance = new Vector3(Random.Range(m_spawnMiniDistance, m_spawnMaxDistance), 0, Random.Range(m_spawnMiniDistance, m_spawnMaxDistance));
        Vector3 direction = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        Transform transform = gameObject.transform;
        transform.Translate(distance);
        //new Vector3(curChest.transform.position.x, gameObject.transform.position.y, curChest.transform.position.z);
        return transform.position;
    }

    void SpawnAutoChest() {
        if (!m_autoSpawn || m_chestList.Count <= 0 || m_spawnedChests.Count >= SpawnCount)
            return;


        int index = Random.Range(0, (int)m_chestList.Count);
        GameObject curChest = Instantiate(m_chestList[index]);
       
        curChest.transform.position = gameObject.transform.position;

        Vector3 distance = new Vector3(Random.Range(m_spawnMiniDistance, m_spawnMaxDistance), 0, Random.Range(m_spawnMiniDistance, m_spawnMaxDistance));
        curChest.transform.Translate(distance);
        curChest.transform.position = new Vector3(curChest.transform.position.x, gameObject.transform.position.y, curChest.transform.position.z);
        //Debug.Log("Direction = " + direction.ToString() + "   ||  distance = " + distance.ToString() + "  ->  pos = " + curChest.transform.position);

        curChest.SetActive(true);
        
        m_spawnedChests.Add(curChest, Time.time);

        /*
        if (distance >= m_spawnMiniDistance && distance <= m_spwanMaxDistance && Timer < Time.time)
        {

            m_curChest.gameObject.transform.position = this.gameObject.transform.position;
            Instantiate(objectToSpawn, , transform.rotation);
            Timer = Time.time + 1;
        }
        */
    }

    // Update is called once per frame
    void Update () {
        CleanFadeoutChest();
        SpawnAutoChest();
    }
    
    void CleanFadeoutChest() {
        List<GameObject> fadeoutChests = new List<GameObject>();
        foreach (KeyValuePair<GameObject, float> spawnChest in m_spawnedChests)
        {

            if (spawnChest.Key.activeSelf == false)
            {
                fadeoutChests.Add(spawnChest.Key);                
            }
        }

        for(int j = 0; j < fadeoutChests.Count; j++)
        {
            GameObject fadeoutChest = fadeoutChests[j];
            m_spawnedChests.Remove(fadeoutChest);             
        }

        for (int j = 0; j < fadeoutChests.Count; j++)
        {
            Destroy(fadeoutChests[j]);
        }
    }

    void BeginColliderWithChest(GameObject chest)
    {
        //collider with player, need respawn another chest;
        Debug.Log("BeginColliderWithChest with player, need respawn another chest");
    }

    //another way to do chase, by receive cmd;
    //trigger is collider collision;
    void FinishColliderWithChest(GameObject chest)
    {
        //collider with player, need respawn another chest;
        Debug.Log("FinishColliderWithChest with player, need respawn another chest");
        m_spawnedChests.Remove(chest);
    }
}
