using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MonsterSpawnManager : MonoBehaviour
{
    static MonsterSpawnManager instance;

    public float minTime = 5;
    public float maxTime = 10;

    public Vector3 maxDistance = new Vector3(5, 5, 5);

    public static MonsterSpawnManager GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        SpawnMonster();
        RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnMonster();
        }
    }

    void RandomSpawn()
    {
        float time = Random.Range(minTime, maxTime);

        if (!Monster.GetInstance().TimeAttack)
        {
            SpawnMonster();
            Debug.Log("Trocou pos");
        }

        Invoke("RandomSpawn", time);
    }

    public void SpawnMonster()
    {
        List<GameObject> spawnPoints = GetNextSpawnPoints();

        int rndIndex = Random.Range(0, spawnPoints.Count);

        Vector3 pos = new Vector3(
            spawnPoints[rndIndex].transform.position.x,
            spawnPoints[rndIndex].transform.position.y,
            spawnPoints[rndIndex].transform.position.z
            );

        float rndX = Random.Range(-maxDistance.x, maxDistance.x) + pos.x;
        //float rndY = Random.Range(-maxDistance.y, maxDistance.y) + pos.y;
        float rndZ = Random.Range(-maxDistance.z, maxDistance.z) + pos.z;

        pos = new Vector3(rndX, transform.position.y, rndZ);

        Monster.GetInstance().PreSpawn(pos);

        Debug.Log("Spawn Monster" + Time.deltaTime);
    }

    List<GameObject> GetNextSpawnPoints()
    {
        IEnumerable<GameObject> tempSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        List<GameObject> spawnPoints;

        Vector3 posPlayer = Player.GetInstance().transform.position;

        tempSpawnPoints = tempSpawnPoints.OrderBy(o => Vector3.Distance(o.transform.position, posPlayer));

        spawnPoints = tempSpawnPoints.ToList();
        //spawnPoints.RemoveAt(0);
        spawnPoints.RemoveRange(0, (spawnPoints.Count) / 2);

        Debug.Log(spawnPoints.Count);

        /*
        foreach(GameObject item in spawnPoints)
        {
            Debug.Log(item.name);
            Debug.Log(Vector3.Distance(item.transform.position, posPlayer));
        }*/

        return spawnPoints;
    }

    void GetSpawnPoints()
    {

    }
}
