using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject barrelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameOverCoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameOverCoolDown()
    {
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        Debug.Log("Heitetään Barrel");
        //Barrelin heittokoodi
        Instantiate(barrelPrefab, throwPoint.position, throwPoint.rotation);
        StartCoroutine(GameOverCoolDown());

    }
}
