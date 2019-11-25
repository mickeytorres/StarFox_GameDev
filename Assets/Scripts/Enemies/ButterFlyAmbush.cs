using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyAmbush : MonoBehaviour
{
    //INTENT: SpawnsFourWaves of ButterFly

    public GameObject TwoButterflyFormation;

    void Start()
    {
        StartCoroutine(MyFirstCoroutine());
    }

    IEnumerator MyFirstCoroutine()
    {
        Instantiate(TwoButterflyFormation,this.transform);
        yield return new WaitForSeconds(1.5f);
        Instantiate(TwoButterflyFormation, this.transform);
        yield return new WaitForSeconds(1.5f);
        Instantiate(TwoButterflyFormation, this.transform);
        yield return new WaitForSeconds(1.5f);
        Instantiate(TwoButterflyFormation, this.transform);
    }
}
