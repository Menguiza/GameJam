using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
   
    public GameObject fallingRocks;
    public float respawnTime = 5.0f;
    private Vector2 screenbounds;
    public float time;
    public bool activate;

    // Start is called before the first frame update
    void Start()
    {
       
        activate = false;
    }
    private void Update()
    {
        if (activate)
        {
            StartCoroutine(pilarWave());
        }
        screenbounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    private void spawnPilar()
    {
        GameObject a = Instantiate(fallingRocks) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenbounds.x + 7, screenbounds.x + 9), screenbounds.y);
    }
    // Update is called once per frame
    IEnumerator pilarWave()
    {

        while (activate)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPilar();
        }
    }
    public void Activate(bool confirm)
    {
        print("yeh");
        activate = confirm;

    }
}
