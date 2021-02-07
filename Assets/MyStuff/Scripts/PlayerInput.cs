using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public bool renewField = false;
    public float cubeSize = 2.5f;
    public float margin = 0.25f;
    public int seed = 350;
    public float speed = 15;
    public bool start = false;

    public GameObject UI;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        OpenCloseUI();
        cubeSize = CheckValueInRange(cubeSize, 2.5f, 50f);
        margin = CheckValueInRange(margin, 0f, 20f);
        seed = CheckValueInRange(seed, 1, 1000);
    }

    private float CheckValueInRange(float number, float minValue, float maxValue)
    {
        if (number < minValue)
        {
            number = minValue;
        }
        else if (number > maxValue)
        {
            number = maxValue;
        }
        return number;
    }
    private int CheckValueInRange(int number, int minValue, int maxValue)
    {
        if (number < minValue)
        {
            number = minValue;
        }
        else if (number > maxValue)
        {
            number = maxValue;
        }
        return number;
    }

    private void OpenCloseUI()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (UI.activeInHierarchy == true)
            {
                UI.SetActive(false);
            }
            else
            {
                UI.SetActive(true);
            }
        }
    }
}
