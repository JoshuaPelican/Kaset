using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton

    public static SpawnManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public GameObject[] Enemies;
    public Vector3 bounds;
    public float offset = 3;

    public GameObject spawnEffect;

    private void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.scaledPixelWidth, Camera.main.pixelHeight));
    }

    public void SpawnEnemy(int enemyIndex, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            int roll = Random.Range(0, 3);

            float xPos = 0;
            float yPos = 0;

            switch (roll)
            {
                //Top Edge
                case 0:
                    xPos = Random.Range(-bounds.x + offset, bounds.x - offset);
                    yPos = bounds.y - offset;
                    break;
                //Left Edge
                case 1:
                    xPos = -bounds.x + offset;
                    yPos = Random.Range(-bounds.y + offset, bounds.y - offset);
                    break;
                //Right Edge
                case 2:
                    xPos = bounds.x - offset;
                    yPos = Random.Range(-bounds.y + offset, bounds.y - offset);
                    break;
            }
            if(Physics2D.OverlapCircle(new Vector2(xPos, yPos), .5f))
            {
                SpawnEnemy(enemyIndex, amount);
            }
            else
            {
                Vector2 spawnPos = new Vector2(xPos, yPos);
                Instantiate(spawnEffect, spawnPos, Quaternion.identity);
                Instantiate(Enemies[enemyIndex], new Vector2(xPos, yPos), Quaternion.identity);
            }
        }

    }
}
