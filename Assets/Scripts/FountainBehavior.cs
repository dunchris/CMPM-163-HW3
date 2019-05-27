using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainBehavior : MonoBehaviour
{
    ParticleSystem fountain;
    
    public static float[] playTimes = new float[4];
    public static float[] stopTimes = new float[4];
    public static float[] colorTimes = new float[5];
    public Color[] colors = new Color[5];
    public float[] speeds = new float[5];

    public static int playInd;
    public static int colorInd;
    public static int stopInd;

    
    // Start is called before the first frame update
    void Start()
    {
        fountain = gameObject.GetComponent<ParticleSystem>();

        playTimes[0] = 91.1f;
        playTimes[1] = 167.1f;
        playTimes[2] = 999.0f;

        stopTimes[0] = 123.1f;
        stopTimes[1] = 265.5f; //end of song
        stopTimes[2] = 999.0f;

        colorTimes[0] = 91.1f;
        colorTimes[1] = 167.0f;
        colorTimes[2] = 200.0f;
        colorTimes[3] = 232.5f;
        colorTimes[4] = 999.0f;

        colors[0] = new Color(0.0f, 0.1359539f, 1.0f);
        colors[1] = new Color(255, 0, 64);
        colors[2] = new Color(1, 0.5306742f, 0);
        colors[3] = new Color(1, 1, 0);
        //colors[4] = new Color(1, 1, 1);

        speeds[0] = 46.6f;
        speeds[1] = 59.6f;
        speeds[2] = 76.7f;
        speeds[3] = 90.6f;

        playInd = 0;
        stopInd = 0;
        colorInd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.name == "Fountain")
        {
            var main = fountain.main;
            //Other checkpoints in the song
            //fountain speeds: 46.6, 59.6, 76.7, 90.6
            if (Time.time >= colorTimes[colorInd])
            {
                main.startColor = colors[colorInd];
                main.startSpeed = speeds[colorInd];
                colorInd++;
            }


            if (Time.time >= playTimes[playInd])
            {
                fountain.Play();
                playInd++;
            }
            else if (Time.time >= stopTimes[stopInd])
            {
                fountain.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                stopInd++;

            }
        }
        if (gameObject.name == "IglooFountain1" || gameObject.name == "IglooFountain2")
        {
            var main = fountain.main;
            if (colorInd >= 4)
            {
                main.startColor = colors[1];
                main.startSpeed = speeds[0];
                fountain.Play();
            }
            if(Time.time >= stopTimes[1])
            {
                fountain.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                stopInd++;

            }
        }
        
    }
}
