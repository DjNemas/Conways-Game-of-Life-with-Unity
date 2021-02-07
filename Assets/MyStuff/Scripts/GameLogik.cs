using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogik : MonoBehaviour
{
    public static GameLogik instance;
    private void Awake()
    {
        instance = this;
    }

    public void CheckNeightboursFields(List<List<CubeInfo>> cubeCurrentList, List<List<CubeInfo>> cubeOtherList)
    {
        for (int x = 1; x < cubeCurrentList.Count - 1; x++)
        {
            for (int y = 1; y < cubeCurrentList[x].Count - 1; y++)
            {
                int count = 0;
                // Top
                if (cubeCurrentList[x - 1 ][y + 1].isAlive == true)
                {
                    count++;
                }
                if (cubeCurrentList[x][y + 1].isAlive == true)
                {
                    count++;
                }
                if (cubeCurrentList[x + 1][y + 1].isAlive == true)
                {
                    count++;
                }
                // Middle
                if (cubeCurrentList[x - 1][y].isAlive == true)
                {
                    count++;
                }
                if (cubeCurrentList[x + 1][y].isAlive == true)
                {
                    count++;
                }
                // Bottom
                if (cubeCurrentList[x - 1][y - 1].isAlive == true)
                {
                    count++;
                }
                if (cubeCurrentList[x][y - 1].isAlive == true)
                {
                    count++;
                }
                if (cubeCurrentList[x + 1][y - 1].isAlive == true)
                {
                    count++;
                }

                if (cubeOtherList[x][y].isAlive == false && count == 3)
                {
                    cubeOtherList[x][y].SetIsAlive(true);
                    PlayField.instance.SetCubeColor(cubeOtherList[x][y].gameOBJ, PlayField.orange);
                }
                else if(cubeOtherList[x][y].isAlive && count < 2)
                {
                    cubeOtherList[x][y].SetIsAlive(false);
                    PlayField.instance.SetCubeColor(cubeOtherList[x][y].gameOBJ, PlayField.black);
                }
                else if (cubeOtherList[x][y].isAlive && count == 2 || count == 3)
                {
                    continue;
                }
                else if (cubeOtherList[x][y].isAlive && count > 3)
                {
                    cubeOtherList[x][y].SetIsAlive(false);
                    PlayField.instance.SetCubeColor(cubeOtherList[x][y].gameOBJ, PlayField.black);
                }
            }
        }
    }

    public void CopyField(List<List<CubeInfo>> cubeCurrentList, List<List<CubeInfo>> cubeOtherList)
    {
        for (int x = 0; x < cubeCurrentList.Count; x++)
        {
            for (int y = 0; y < cubeCurrentList[x].Count; y++)
            {
                cubeOtherList[x][y].SetIsAlive(cubeCurrentList[x][y].isAlive);
                cubeOtherList[x][y].gameOBJ.GetComponent<MeshRenderer>().material.color = cubeCurrentList[x][y].gameOBJ.GetComponent<MeshRenderer>().material.color;
            }
        }
    }
}
