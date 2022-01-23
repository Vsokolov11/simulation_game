using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject entity;
    public entity genePass;
    public Light lt;
    public Light env;
    public float dayDuration = 20;
    public float nightDuration = 5;
    public float night;
    public int day = 0;
    public float timeLeft = 1;
    public float statringEntities = 3;
    [HideInInspector]
    public float maxZ = 19.5f;
    [HideInInspector]
    public float minZ = -19.5f;
    [HideInInspector]
    public float maxX = 19.5f;
    [HideInInspector]
    public float minX = -19.5f;
    
    public GameObject house;
    public GameObject food;
    public float foodCount;
    public Vector3 pos;
    public int entityCount = 0;
    float timeToSpawn = 0.5f;
    public float temp;
    //Variables for spawning the houses
    public float distanceBetween = 5f;
    public float startingPosX = -15;
    public int numberOfHouses = 7;
    Vector3[] spawns;

    bool count = false;
    [HideInInspector]
    public bool isNight = false;
    void Start()
    {
        Application.targetFrameRate = 80;
        temp = timeToSpawn;
        //Debug.Log("Started!");
        lt.color = Color.white;
        lt.intensity = 0.6f;
        env.intensity = 0.8f;
        env.color = Color.white;
        night = nightDuration;
        timeLeft = dayDuration;
        CreateSpawns();
        for (int i = 0; i < statringEntities; i++)
        {
            Spawn();
        }
        foodCount = statringEntities * 2;
        GiveFood();
    }
    public void Spawn()
    {
        entityCount++;
        int index = Random.Range(0, spawns.Length - 1);
        pos = spawns[index];
        Debug.Log(pos);
        Debug.Log(spawns[index]);
        Instantiate(entity, pos, Quaternion.identity);
        //Debug.Log("SPAWNED");
        genePass.speed = Random.Range(8, 12);       
    }
    public void GiveFood()
    {
        float m = 1.5f;//Random.Range(1.3f, 2);
        foodCount = Mathf.CeilToInt(entityCount * m);
        for (int i = 0; i < foodCount; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            Vector3 foodPoint = new Vector3(x, 1, z);
            Instantiate(food, foodPoint, Quaternion.identity);
        }
        
    }
    public void ClearFood()
    {
        GameObject[] leftovers = GameObject.FindGameObjectsWithTag("food");
        for (int i = 0; i < leftovers.Length; i++)
        {
            Destroy(leftovers[i]); 
            env.colorTemperature = 1200f;
        }
    }
    public void CreateSpawns()
    {
        spawns = new Vector3[numberOfHouses];
        Vector3 currentPos = new Vector3(startingPosX, 1, 18);
        for(int i = 0; i < numberOfHouses; i++)
        {
            
            GameObject current = Instantiate(house, currentPos, Quaternion.Euler (-90, 0, 0));
            spawns[i] = current.transform.position;
            //Debug.Log(spawns[i]);
            currentPos.x += distanceBetween;
        }
    }
    void Update()
    {
        timeLeft -= 1 * Time.deltaTime;
        

        if (timeLeft <= 0)
        {
            
            isNight = true;
            lt.intensity = 0.5f;
            lt.color = Color.gray;
            env.color = Color.gray;
            night -= 1 * Time.deltaTime;
            ClearFood();
            if (night <= 0)
            {
                isNight = false;
                count = true;
                day++;
                lt.color = Color.white;
                lt.intensity = 1f;
                env.color = Color.white;
                timeLeft = dayDuration;               
                night = nightDuration;                            
            }
        }
        if (count)
        {
            //Debug.Log("Count");
            temp -= 1 * Time.deltaTime;
        }
        if (temp <= 0)
        {
            //Debug.Log("Feed");
            GiveFood();
            count = false;
            temp = timeToSpawn;

        }
    }
}
