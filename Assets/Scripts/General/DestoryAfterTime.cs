using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterTime : MonoBehaviour {

    public float TimeUntilDestroy=1;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, TimeUntilDestroy);
	}
	
}
