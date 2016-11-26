﻿using UnityEngine;
using System.Collections;

public class Fireplace : MonoBehaviour
{
    public float fireParticleIntensity = 2;
    public float lightIntensity = 6.14f;
    public float minlightIntensity = 0.2f;
    
    public Light light;
    public GameObject fireParticle;

    // Use this for initialization
    void Start () {
        light.intensity = 0;
    }
	
	// Update is called once per frame
	void Update () {
        light.intensity -= Time.deltaTime * 0.1f;
	}
}
