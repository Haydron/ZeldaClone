using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoorBlockingObject : MonoBehaviour {

    public float DestroyProbability;
    private void Start()
    {
        float randValue = Random.Range(0f, 1f);
        if(randValue < DestroyProbability)
        {
            Destroy(this.gameObject);
        }
    }
}
