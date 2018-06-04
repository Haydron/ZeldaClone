using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public static GameCamera Instance;
    public float MovementSpeedOnRoomChange;
    public RoomParent CurrentRoom;
    // Use this for initialization
    void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
	}

    void UpdatePosition()
    {
        if(CurrentRoom == null)
        {
            return;
        }
        Vector3 targerPosition = CurrentRoom.GetRoomCenter();

        transform.position = Vector3.MoveTowards(transform.position, targerPosition, Time.deltaTime * MovementSpeedOnRoomChange);
    }
}
