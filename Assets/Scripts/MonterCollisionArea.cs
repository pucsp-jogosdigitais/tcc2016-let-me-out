using UnityEngine;
using System.Collections;

public class MonterCollisionArea : MonoBehaviour
{

    public enum TypeCollisionArea { Sound, Spawn };

    public TypeCollisionArea currType;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter()
    {
        switch (currType)
        {
            case TypeCollisionArea.Sound:

                if (Monster.GetInstance().currActionState != Monster.MonsterActionState.Rest)
                {
                    AudioClip tensionAudio = Monster.GetInstance().tensionAudio;

                    Monster.GetInstanceAudioSource().GetComponent<AudioSource>().clip = tensionAudio;
                    Monster.GetInstanceAudioSource().Play();
                }

                break;
            case TypeCollisionArea.Spawn:

                if (!Monster.GetInstance().TimeAttack && Monster.GetInstance().currActionState == Monster.MonsterActionState.Spawn)
                {
                    AudioClip babyAudio = Monster.GetInstance().babyAudio;

                    Player.GetInstance().ActivateGrainCamera();

                    Monster.GetInstanceAudioSource().Stop();
                    Monster.GetInstanceAudioSource().GetComponent<AudioSource>().clip = babyAudio;
                    Monster.GetInstanceAudioSource().Play();
                    Monster.GetInstance().Spawn();
                    Monster.GetInstance().InitAttackTime();
                }
                break;
        }
    }

    void OnTriggerExit()
    {
        switch (currType)
        {
            case TypeCollisionArea.Sound:
                Monster.GetInstanceAudioSource().Stop();
                Player.GetInstance().DesactivateGrainCamera();
                break;
        }
    }
}
