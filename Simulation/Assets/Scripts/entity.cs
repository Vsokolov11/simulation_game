using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class entity : MonoBehaviour
{
    private World world;
    public Slider slider;
    public Text speedText;
    public float life = 100;
    public float lifespan;
    public float speed;
    public Vector3 moveSpot;
    public float maxZ = 19.5f;
    public float minZ = -19.5f;
    public float maxX = 19.5f;
    public float minX = -19.5f;
    public float x = 0;
    public float z = 0;
    bool isChasing = false;
    [HideInInspector]public GameObject target;
    float distance = 1;
    public int eatenToday = 0;
    public Renderer color;
    int currentDay = 0;
    int worldDay = 0;
    public Vector3 spawner = Vector3.zero;
    void Start()
    {
        float temp = life * 10;
        life = temp / speed;
        //Debug.Log(life);
        spawner = transform.position;
        world = GameObject.FindObjectOfType<World>();
        worldDay = world.day;
        currentDay = worldDay;
        x = Random.Range(minX, maxX);
        z = Random.Range(minZ, maxZ);
        lifespan = life;
        GenerateLocation();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!isChasing)
        {
            target = collision.collider.gameObject;
            moveSpot = target.transform.position;
            isChasing = true;
        }
    }
    public void ResetDay()
    {
        if (eatenToday >= 2)
        {
            world.Spawn();
        }
        eatenToday = 0;
        currentDay = worldDay;
        lifespan = life;
        color.material.color = new Color32(159,137,175,100);       
    }
    void Eat()
    {
        isChasing = false;
        lifespan = life;
        eatenToday += 1;
        Destroy(target);       
    }
    void Lifespan()
    {
        if (lifespan <= 0)
        {
            world.entityCount--;
            Destroy(gameObject);
        }
    }
    void EatCheck()
    {
        if (eatenToday >= 1)
        {
            color.material.color = new Color32(109, 167, 222,100);
        }
        if (eatenToday >= 2)
        {
            color.material.color = new Color32(222, 109, 137,100);
        }
    }
    void SetValues()
    {
        slider.value = lifespan;
        slider.maxValue = life;
        speedText.text = "Speed: " + speed;
    }
    void GenerateLocation()
    {
        x = Random.Range(transform.position.x - 10, transform.position.x + 10);
        z = Random.Range(transform.position.z - 10, transform.position.z + 10);
        moveSpot = new Vector3(x, 0, z);
        if(moveSpot.x > maxX)
        {
            moveSpot.x = maxX;
        }
        if(moveSpot.x < minX)
        {
            moveSpot.x = minX;
        }
        if(moveSpot.z > maxZ)
        {
            moveSpot.z = maxZ;
        }
        if(moveSpot.z < minZ)
        {
            moveSpot.z = minZ;
        }
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
        transform.LookAt(moveSpot);
        if(transform.position == moveSpot)
        {
            GenerateLocation();
        }
    }
    void Survive()
    {
        SetValues();
        if (Vector3.Distance(transform.position, moveSpot) <= 0.3 && !isChasing)
        {
            GenerateLocation();
        }
        else if (isChasing)
        {
            if (target != null)
            {
                distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < 0.2)
                {
                    Eat();
                    GenerateLocation();
                }
            }
            else
            {
                isChasing = false;
                GenerateLocation();
            }
        }  
        Lifespan();
        EatCheck();
        if (eatenToday == 2)
        {
            moveSpot = spawner;
            lifespan = life;
        }
        if (world.isNight)
        {
            lifespan = life;
            moveSpot = spawner;
        }
        if (currentDay < worldDay)
        {            
            ResetDay();
        }
    }
    void Update()
    {
        Survive();
        Move();
        lifespan -= 1 * Time.deltaTime;
        worldDay = world.day;   
    }
}
