using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class Monster : MonoBehaviour
{
    public enum MonsterActionState { Spawn, Persecution, Rest };
    public MonsterActionState currActionState;

    static Monster instance;

    public NavMeshAgent mosterNV;

    public AudioClip tensionAudio;
    public AudioClip babyAudio;

    bool attackTime = false;
    float attackTimeTick;
    float counterAttackTime;

    public bool IsVisibleByPlayer { get; set; }
    public Collider soundAreaCollider;

    public static Monster GetInstance()
    {
        return instance;
    }

    public static AudioSource GetInstanceAudioSource()
    {
        return GameObject.FindGameObjectWithTag("BabyMonster").GetComponent<AudioSource>();
    }

    void Start()
    {
        instance = this;
        SetVisibility(false);
        currActionState = MonsterActionState.Rest;
        //SetVisibility(true);
    }

    void Update()
    {

        switch(currActionState)
        {
            case MonsterActionState.Spawn:
                transform.LookAt(Player.GetTransform().position);

                Vector3 playerPos = Player.GetTransform().position;
                float volumeReduction = (GetDistance(playerPos) / soundAreaCollider.bounds.extents.x);
                //float volumeReduction = (GetDistance(playerPos) / 6) * 0.6f;
                //Debug.Log(GetDistance(playerPos));

                GetInstanceAudioSource().volume = (1 - volumeReduction) * .3f;

                if (attackTime)
                {
                    if (InScreen())
                    {
                        counterAttackTime -= Time.deltaTime;
                        Player.GetMotionBlur().blurAmount = 0.6f;
                        //Player.GetShake().shakeDuration = 5;

                        if (counterAttackTime < 0)
                        {
                            Debug.Log("Matou player");
                            Main.GetInstance().GameOver();
                            //Player.GetInstance().Die();
                        }
                    }
                    else
                    {
                        //Player.GetShake().shakeDuration = 0;
                        counterAttackTime = attackTimeTick;
                    }
                }

                break;
            case MonsterActionState.Persecution:

                SetVisibility(true);
                mosterNV.destination = Player.GetTransform().position;

                break;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player" && currActionState == MonsterActionState.Persecution)
        {
            Main.GetInstance().GameOver();
        }
    }

    public void PreSpawn(Vector3 pos)
    {
        //SetVisibility(true);
        gameObject.transform.position = pos;
    }

    public void Spawn()
    {
        /*
            Renderer r = gameObject.GetComponent<Renderer>();
            r.enabled = true;

            mesh.enabled = true;*/

        SetVisibility(true);

        SetNewPosition();
        //mosterNV.destination = Player.GetInstance().transform.position;
    }

    public void PersecutionMode()
    {
        currActionState = MonsterActionState.Persecution;
    }

    public float GetDistance(Vector3 distance)
    {
        return Vector3.Distance(transform.position, distance);
    }

    public void InitAttackTime()
    {
        attackTime = true;
        attackTimeTick = Random.Range(1, 4);

        //Invoke("Attack", 5);
        Invoke("CancelAttack", 5);
    }

    public void Attack()
    {
        if (attackTime)
        {
            //mosterNV.destination = Player.GetInstance().transform.position;
            //Player.GetInstance().Die();
        }
    }

    public void CancelAttack()
    {
        attackTime = false;
        GetInstanceAudioSource().Stop();
        Player.GetInstance().DesactivateGrainCamera();
        SetVisibility(false);
        MonsterSpawnManager.GetInstance().SpawnMonster();
        counterAttackTime = attackTimeTick;
        //Player.GetShake().shakeDuration = 0;
        Player.GetMotionBlur().blurAmount = 0;
    }

    public bool InScreen()
    {
        bool inScreen = false;

        Renderer view = gameObject.GetComponent<Renderer>();

        if (view.isVisible)
        {
            inScreen = true;
        }

        return inScreen;
    }

    void SetNewPosition()
    {
        float spawnDistance = 1;

        Vector3 playerPos = Player.GetInstance().transform.position;
        Vector3 playerDirection = Player.GetInstance().transform.forward;
        Quaternion playerRotation = Player.GetInstance().transform.rotation;
        Vector3 spawnPos = playerPos - playerDirection * spawnDistance;

        gameObject.transform.position = spawnPos;
        gameObject.transform.rotation = playerRotation;
    }

    void SetVisibility(bool visibility)
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in rs)
        {
            r.enabled = visibility;
        }

        GameObject[] monsterCollisionArea = GameObject.FindGameObjectsWithTag("MonsterCollisionArea");

        foreach (GameObject go in monsterCollisionArea)
        {
            go.GetComponent<Renderer>().enabled = false;
        }

    }

    public bool TimeAttack
    {
        get { return attackTime; }
    }
}
