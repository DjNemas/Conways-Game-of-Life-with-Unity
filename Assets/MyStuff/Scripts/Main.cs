using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private float fps;
    private float fpsCounter;
    private int currentField = 1;

    // Start is called before the first frame update
    void Start()
    {
        InitPlayfield();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.instance.renewField)
        {
            PlayField.instance.DestroyListsObj();
            InitPlayfield();
            PlayerInput.instance.renewField = false;
        }
        // Run Game by UserInput Start
        if (PlayerInput.instance.start)
        {
            fpsCounter -= Time.deltaTime;
            if (fpsCounter <= 0)
            {
                if (currentField == 1)
                {
                    GameLogik.instance.CheckNeightboursFields(PlayField.instance.cubeList1, PlayField.instance.cubeList2);
                    PlayField.instance.HideCubes(PlayField.instance.cubeList1);
                    PlayField.instance.ShowCubes(PlayField.instance.cubeList2);
                    GameLogik.instance.CopyField(PlayField.instance.cubeList2, PlayField.instance.cubeList1);
                    currentField = 2;

                }
                else if (currentField == 2)
                {
                    GameLogik.instance.CheckNeightboursFields(PlayField.instance.cubeList2, PlayField.instance.cubeList1);
                    PlayField.instance.HideCubes(PlayField.instance.cubeList2);
                    PlayField.instance.ShowCubes(PlayField.instance.cubeList1);
                    GameLogik.instance.CopyField(PlayField.instance.cubeList1, PlayField.instance.cubeList2);
                    currentField = 1;
                }
                fpsCounter = fps;
            }
        }
        if ((float)PlayerInput.instance.speed == 0)
        {
            fps = 0;
        }
        else
        {
            fps = 1 / (float)PlayerInput.instance.speed;
        }
        
    }

    private void InitPlayfield()
    {
        PlayField.instance.InitPlayField(
            PlayerInput.instance.cubeSize,
            PlayerInput.instance.margin,
            PlayerInput.instance.seed);
    }
}
