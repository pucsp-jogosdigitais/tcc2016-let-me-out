using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public enum MonsterState { Persecution, Rest, Stopped };
    MonsterState currMonsterState;

    public float spawnTimeMinutes = 2;
    float counterSpawnTimeMinutes = 0;

    public float persecutionTimeSeconds = 30;
    //float counterPersecutionTimeSeconds = 0;

    public float counterMinutes = 0;
    public float counterSeconds = 0;

    public Text txtTime;

    public GameObject babyMonster;
    GameObject currGameMonster;

    static Main thatMain;

    public static Main getInstance()
    {
        return thatMain;
    }

    // Use this for initialization
    void Start()
    {
        thatMain = this;
        currMonsterState = MonsterState.Stopped;
    }

    // Update is called once per frame
    void Update()
    {
        txtTime.text = "Seconds: " + Mathf.Floor(counterSeconds) + "\n" +
            "Minutes: " + counterMinutes + "\n" +
            "Spaw Minutes: " + spawnTimeMinutes + "\n";


        IncrementTime();

        if (counterMinutes >= spawnTimeMinutes)
        {
            //Debug.Log("spawn time");
            txtTime.text += "Spawn!!!";

            counterMinutes = 0;
            Vector3 playerPos = Player.getInstance().transform.position;
            Vector3 playerDirection = Player.getInstance().transform.forward;
            Quaternion playerRotation = Player.getInstance().transform.rotation;
            float spawnDistance = 10;

            Vector3 spawnPos = playerPos - playerDirection * spawnDistance;

            //currMonsterState = MonsterState.Rest;
            if (currGameMonster == null)
            {
                currGameMonster = (GameObject)Instantiate(babyMonster, spawnPos, playerRotation);
            }
        }

        /*
        switch(currMonsterState)
        {
            case MonsterState.Rest:

            break;
            case MonsterState.Persecution:

                            

            break;
        }*/

        /*
        if(Input.GetKeyDown(KeyCode.B))
        {
            Vector3 playerPos = Player.getInstance().transform.position;
            Vector3 playerDirection = Player.getInstance().transform.forward;
            Quaternion playerRotation = Player.getInstance().transform.rotation;
            float spawnDistance = 2;

            Vector3 spawnPos = playerPos - playerDirection * spawnDistance;

            Instantiate(babyMonster, spawnPos, playerRotation);
        }*/

    }

    void IncrementTime()
    {
        counterSeconds += Time.deltaTime;

        if (counterSeconds >= 60)
        {
            counterMinutes += 1;
            counterSeconds = 0;

            Debug.Log("Minutos" + counterMinutes);
        }

        //Debug.Log("seconds" + counterSeconds);
        //Debug.Log("minf" + counterMin);
    }
}
