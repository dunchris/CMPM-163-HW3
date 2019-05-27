using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactionScript : MonoBehaviour {

    ParticleSystem snow;
    //const float PI = 3.14159f;
    void Start () 
    {
        if (gameObject.name == "Snow")
        {
            // Get Particle system component
            snow = gameObject.GetComponent<ParticleSystem>();
            // Call particle play function
            snow.Play();
            var velocityOverLifetime = snow.velocityOverLifetime;
            velocityOverLifetime.enabled = true;
        }
	}

    void Update()
    {

        int numPartitions = 3;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2;

        for (int i = 0; i < numDisplayedBins; i++)
        {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions) {
                aveMag[(int)partitionIndx] += MyAudioPeer._samples[i] / (512 / numPartitions);
                //aveMag[(int)partitionIndx] = 0;
            }
            else {
                partitionIndx++;
                i--;
            }
        }

        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100) {
                aveMag[i] = 100;
            }
        }

        float snowMag = aveMag[0];
        float deerMag = aveMag[1];
        float particleMag = aveMag[2];
        particleMag = (particleMag - 0.5f) * 100.0f;

        float simSpeedInc = 2.5f;
        float particleSizeInc = 4;
        //double angle = aveMag[0] % (2 * PI) * (180/PI);


        if (gameObject.name == "Snowman/default" || gameObject.name == "Snowman")
        {
            transform.localScale = new Vector3(snowMag / 2, snowMag / 2, snowMag / 2);
        }
        else if (gameObject.name == "Deer" || gameObject.name == "Deer/default")
        {
            transform.localScale = new Vector3(deerMag, deerMag, deerMag);
        }
        else if (gameObject.name == "Snow")
        {
            int stopInd = 0;
            int colorInd = 2;
            int playInd = 1;
            var main = snow.main;
            var velocityOverLifetime = snow.velocityOverLifetime;
            velocityOverLifetime.orbitalZMultiplier = particleMag;

            /*The indices for the snow are independent of the fountain*/
            if (Time.time >= FountainBehavior.playTimes[playInd])
            {
                snow.Play();
                playInd++;
            }
            else if (Time.time >= FountainBehavior.stopTimes[stopInd])
            {
                
                snow.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                stopInd++;
            }
            if (Time.time >= FountainBehavior.colorTimes[colorInd])
            {
                main.simulationSpeed = 1.0f + simSpeedInc + particleMag;
                main.startSize = 4.8f + particleSizeInc + particleMag;
                particleSizeInc += 10.0f;
                simSpeedInc += 5.0f;
                colorInd++;
            }
        }
    }


}

