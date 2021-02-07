using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour
{
    public static PlayField instance;

    private const float resolutionWidth = 7680;
    private const float resolutionHeight = 4320;

    private float cubeSize;
    private float margin;
    private float cubeUnitSize;
    private float marginUnitSize;
    private int seed;

    private float posX, posY;
    private float cubesAmountX, cubesAmountY;

    public GameObject cubes;
    public List<List<CubeInfo>> cubeList1;
    public List<List<CubeInfo>> cubeList2;

    public static Color orange;
    public static Color white;
    public static Color black;

    private void Awake()
    {
        instance = this;
        orange = new Color(1f / 255f * 255f, 1f / 255f * 125f, 1f / 255f * 0f, 1f / 255f * 255f);
        white = new Color(1f / 255f * 255f, 1f / 255f * 255f, 1f / 255f * 255f, 1f / 255f * 255f);
        black = new Color(0f, 0f, 0f, 1f);
    }

    private void InitParameter()
    {
        cubeUnitSize = cubeSize * 40f;
        marginUnitSize = margin * 40f;

        posX = cubeUnitSize / 2 + marginUnitSize / 2;
        posY = cubeUnitSize / 2 + marginUnitSize / 2;

        cubesAmountX = resolutionWidth / (cubeUnitSize + marginUnitSize);
        cubesAmountY = resolutionHeight / (cubeUnitSize + marginUnitSize);
    }

    public void InitPlayField(float cubeSize, float margin, int seed)
    {
        this.cubeSize = cubeSize;
        this.margin = margin;
        this.seed = seed;
        InitParameter();
        cubeList1 = BuildCubes();
        cubeList2 = BuildCubes();
        ShowCubes(cubeList1);
        HideCubes(cubeList2);
    }

    public void DestroyListsObj()
    {
        foreach (List<CubeInfo> list in cubeList1)
        {
            foreach (CubeInfo cubeObj in list)
            {
                Destroy(cubeObj.gameOBJ);
            }
        }
        cubeList1.Clear();
        cubeList1 = new List<List<CubeInfo>>();

        foreach (List<CubeInfo> list in cubeList2)
        {
            foreach (CubeInfo cubeObj in list)
            {
                Destroy(cubeObj.gameOBJ);
            }
        }
        cubeList2.Clear();
        cubeList2 = new List<List<CubeInfo>>();
    }

    public List<List<CubeInfo>> BuildCubes()
    {
        List<List<CubeInfo>> currentCubes = new List<List<CubeInfo>>();
        for (int h = 1; h < cubesAmountY + 1; h++)
        {
            if ((h * posY) + ((h - 1) * posY) + cubeUnitSize - posY + marginUnitSize / 2 > resolutionHeight) 
                continue;

            currentCubes.Add(new List<CubeInfo>());

            for (int w = 1; w < cubesAmountX + 1; w++)
            {

                if ((w * posX) + ((w - 1) * posX) + cubeUnitSize - posX + marginUnitSize / 2 > resolutionWidth)
                    continue;

                GameObject cube = Instantiate(
                    cubes,
                    new Vector3(transform.position.x + (w * posX) + ((w-1) * posX), transform.position.x + (h * posY) + ((h-1) * posY), cubeUnitSize / 2),
                    cubes.transform.rotation
                    );

                cube.transform.localScale = new Vector3(cubeUnitSize,cubeUnitSize, cubeUnitSize);
                cube.SetActive(false);
                CubeInfo cubeInfo = new CubeInfo();
                cubeInfo.SetGameOBJ(cube);
                cubeInfo.SetIsAlive(false);
                currentCubes[h-1].Add(cubeInfo);
            }
        }

        for (int x = 0; x < currentCubes.Count; x++)
        {
            for (int y = 0; y < currentCubes[x].Count; y++)
            {
                if (x == 0 || y == 0 || x == currentCubes.Count - 1 || y == currentCubes[x].Count - 1)
                {
                    SetCubeColor(currentCubes[x][y].gameOBJ, Color.black);
                }
                else
                {
                    if (Random.Range(0,1000) < seed)
                    {
                        currentCubes[x][y].SetIsAlive(true);
                        SetCubeColor(currentCubes[x][y].gameOBJ, orange);
                    }
                    else
                    {
                        SetCubeColor(currentCubes[x][y].gameOBJ, black);
                    }
                    
                }
            }
        }
        return currentCubes;
    }

    public void SetCubeColor(GameObject cube, Color color)
    {
        cube.GetComponent<MeshRenderer>().material.color = color;
    }

    public void ShowCubes(List<List<CubeInfo>> cubesList)
    {
        foreach (var cubes in cubesList)
        {
            foreach (var cube in cubes)
            {
                cube.gameOBJ.SetActive(true);
            }
        }
    }

    public void HideCubes(List<List<CubeInfo>> cubesList)
    {
        foreach (var cubes in cubesList)
        {
            foreach (var cube in cubes)
            {
                cube.gameOBJ.SetActive(false);
            }
        }
    }

    public void SetCubeSize(float _cubeSize)
    {
        cubeSize = _cubeSize;
    }
    public void SetMargin(float _margin)
    {
        margin = _margin;
    }
}

public class CubeInfo
{
    public GameObject gameOBJ;
    public bool isAlive;

    public void SetGameOBJ(GameObject gameOBJ)
    {
        this.gameOBJ = gameOBJ;
    }

    public void SetIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
