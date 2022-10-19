using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowSpotty : MonoBehaviour
{
    public Light spotLight;
    public Color32[] colors;
    public bool rainbowLight = true;

    public void Start()
    {
        this.enabled = MenuManager.rainbowOn;
        spotLight = transform.GetComponent<Light>();
        colors = new Color32[7]
        {
            new Color32(255, 0, 0, 255), //red
            new Color32(255, 165, 0, 255), //orange
            new Color32(255, 255, 0, 255), //yellow
            new Color32(0, 255, 0, 255), //green
            new Color32(0, 0, 255, 255), //blue
            new Color32(75, 0, 130, 255), //indigo
            new Color32(238, 130, 238, 255), //violet
        };
        //StartCoroutine(Cycle());
    }

    public void Update()
    {
        StartCoroutine(Cycle());
    }

    public IEnumerator Cycle()
    {
        int startColor = 0;
        int endColor = 0;
        startColor = Random.Range(0, colors.Length);
        endColor = Random.Range(0, colors.Length);

        
        while (rainbowLight)
        {
            for (float interpolant = 0f; interpolant < 1f; interpolant += 0.02f)
            {
                spotLight.color = Color.Lerp(colors[startColor], colors[endColor], interpolant);
                yield return null;
            }
            startColor = endColor;
            endColor = Random.Range(0, colors.Length);
        }
    }
}
