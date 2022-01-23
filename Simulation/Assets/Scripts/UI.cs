using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public World world;
    public Text dayCount;
    public Text dayLeft;
    public Text entityCount;
    public Text foodCount;
    public Text speedCount;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindObjectOfType<World>();
    }

    // Update is called once per frame
    void Update()
    {
        dayCount.text = "Day: " + world.day;
        dayLeft.text = "Time until night: " + Mathf.CeilToInt(world.timeLeft);
        entityCount.text = "Entities count: " + world.entityCount;
        foodCount.text = "Food count: " + world.foodCount;
    }
}
