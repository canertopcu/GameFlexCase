using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private float _ratio = 0;
    private float ratio
    {
        get => _ratio;
        set
        {
            if (_ratio != value)
            {
                _ratio = value;
                SetHeightByRatio(value);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ratio = (float)Screen.width / (float)Screen.height;




    }

    void SetHeightByRatio(float ratio)
    {
        float minFoV = 40;
        float maxFoV = 127;

        float diffHeight = maxFoV - minFoV;

        float minRatio = 0.165f;
        float maxRatio =1f;
        if (ratio > maxRatio)
        {
            ratio = maxRatio;
        }
        if (ratio < minRatio)
        {
            ratio = minRatio;
        }
        float normalizedRatio = (ratio - minRatio) / (maxRatio - minRatio);
        float fov = Mathf.Lerp(minFoV, maxFoV, Mathf.Pow(1 - normalizedRatio, 3));
        Camera.main.fieldOfView = fov;

    }

}
