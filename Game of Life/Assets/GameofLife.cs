using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameofLife : MonoBehaviour
{
    GameObject[,] grid;
    int[,] nextGen;
    int size;
    public GameObject cell;
    float withd;
    void Start()
    {
        size = 100;
        withd = 0.1f;
        grid = new GameObject[size, size];
        nextGen = new int[size, size];
        for(int i=0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                grid[i, j] = Instantiate(cell, new Vector3(withd * i, withd * j, 0f), Quaternion.identity);
                if (Random.Range(0, 2) == 0)
                {
                    grid[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                }
                else
                {
                    grid[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
                nextGen[i, j] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        nextCycle();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Delete();
            StartCreating();
        }
    }
    void StartCreating()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = Instantiate(cell, new Vector3(withd * i, withd * j, 0f), Quaternion.identity);
                if (Random.Range(0, 2) == 0)
                {
                    grid[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                }
                else
                {
                    grid[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
                nextGen[i, j] = 0;
            }
        }
    }

    void Delete()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Destroy(grid[i, j].gameObject);
            }
        }
    }

    void implementCycle()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (nextGen[i, j] == 0)
                {
                    grid[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                }else if(nextGen[i, j] == 1)
                {
                    grid[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
    void nextCycle()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                 if(grid[i, j].GetComponent<SpriteRenderer>().color == Color.black)
                {
                    if(countNeighbours(i, j) == 3)
                    {
                        nextGen[i, j] = 1;
                    }
                    else
                    {
                        nextGen[i, j] = 0;   
                    }
                }
                else if(grid[i, j].GetComponent<SpriteRenderer>().color == Color.white)
                {
                    if (countNeighbours(i, j) < 2 || countNeighbours(i, j) > 3)
                    {
                         
                        
                            nextGen[i, j] = 0;
                        
                    }
                    else
                    {
                        nextGen[i, j] = 1;
                    }
                }
            }
        }
        implementCycle();
    }

    int countNeighbours(int x, int y)
    {
        int counted = 0;
        for(int i= -1; i < 2; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                int nx = (x + i + size) % size;
                int ny = (y + j + size) % size;
                if(grid[nx,ny].GetComponent<SpriteRenderer>().color == Color.white)
                {
                    counted++;
                }
            }
        }
        if(grid[x,y].GetComponent<SpriteRenderer>().color == Color.white)
        {
            counted--;
        }
        return counted;
    }
}
