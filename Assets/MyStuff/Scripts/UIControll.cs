using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControll : MonoBehaviour
{
    public Text cubeSizeInputText;
    public Text marginInputText;
    public Text seedInputText;
    public Text speedInputText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpeedChangedOnRuntime();
    }

    public void OnClickRenewField()
    {
        PlayerInput.instance.cubeSize = float.Parse(cubeSizeInputText.text.Replace(".",","));
        PlayerInput.instance.margin = float.Parse(marginInputText.text.Replace(".", ","));
        PlayerInput.instance.seed = Convert.ToInt32(seedInputText.text);
        PlayerInput.instance.speed = float.Parse(speedInputText.text.Replace(".", ","));
        PlayerInput.instance.renewField = true;
    }

    public void OnClickStart()
    {
        if (PlayerInput.instance.start)
        {
            PlayerInput.instance.start = false;
        }
        else
        {
            PlayerInput.instance.start = true;
        }
    }

    public void OnClickQuit()
    {
        Application.Quit(0);
    }

    private void SpeedChangedOnRuntime()
    {
        string oldString = speedInputText.text;
        string newString = "";
        for (int i = 0; i < oldString.Length; i++)
        {
            if (oldString[i] == '-')
            {
                newString += "0";
            }
            else
            {
                newString += oldString[i];
            }
        }

        if (speedInputText.text == null || speedInputText.text == "" || float.Parse(newString.Replace('.', ',')) < 0)
        {
            speedInputText.text = "0";
        }
        else if (float.Parse(newString.Replace(".", ",")) > 60)
        {
            speedInputText.text = "60";
        }
        else
        {
            PlayerInput.instance.speed = float.Parse(newString.Replace(".", ","));
        }
    }
}
