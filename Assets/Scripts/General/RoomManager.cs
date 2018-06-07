using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomManager : MonoBehaviour {
    public static RoomManager Instance;
    RoomData m_CurrentLoadRoomData;
    Queue<RoomData> m_LoadRoomQueue = new Queue<RoomData>();
    bool m_IsLoading = false;
    List<RoomParent> m_LoadedRooms = new List<RoomParent>();
    public string CurrentWorld = "Overworld";

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        LoadRoom(CurrentWorld+"Start", 0, 0);
        LoadRoom(CurrentWorld + "End", 3, -2);
    }

    void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if (m_IsLoading)
        {
            return;
        }
        
        if(m_LoadRoomQueue.Count == 0)
        {
            return;
        }

        m_CurrentLoadRoomData = m_LoadRoomQueue.Dequeue();
        m_IsLoading = true;

        Debug.Log("Loading new room: "
                + CurrentWorld + " - " 
                + m_CurrentLoadRoomData.Name+" at " 
                + m_CurrentLoadRoomData.X + ", " + m_CurrentLoadRoomData.Y);

        StartCoroutine(LoadRoomRoutine(m_CurrentLoadRoomData));
    }
    void LoadRoom(string name,int x, int y) {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomData newRoomData = new RoomData();
        newRoomData.Name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        m_LoadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomData data)
    {
        string levelName = data.Name;
        AsyncOperation loadlevel = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        while(loadlevel.isDone == false)
        {
            Debug.Log("Loading " + levelName + ": " + Mathf.Round(loadlevel.progress*100)+"%");
            yield return null;
        }

    }

    public void RegisterRoom(RoomParent roomParent)
    {
        roomParent.transform.position = new Vector3(m_CurrentLoadRoomData.X * roomParent.Width, m_CurrentLoadRoomData.Y * roomParent.Heigth);
        roomParent.X = m_CurrentLoadRoomData.X;
        roomParent.Y = m_CurrentLoadRoomData.Y;
        roomParent.name = (m_CurrentLoadRoomData.Name + " " + roomParent.X + ", " + roomParent.Y);
        m_IsLoading = false;

        if(m_LoadedRooms.Count == 0)
        {
            GameCamera.Instance.CurrentRoom = roomParent;
        }
        m_LoadedRooms.Add(roomParent);
    }

    bool DoesRoomExist(int X, int Y)
    {
        return m_LoadedRooms.Find(item => item.X == X && item.Y == Y) != null;
    }

    public void OnPlayerEnterRoom(RoomParent roomParent)
    {
        GameCamera.Instance.CurrentRoom = roomParent;

        LoadRoom(CurrentWorld + GetRandomRegularRoomName(), roomParent.X + 1, roomParent.Y);
        LoadRoom(CurrentWorld + GetRandomRegularRoomName(), roomParent.X - 1, roomParent.Y);
        LoadRoom(CurrentWorld + GetRandomRegularRoomName(), roomParent.X, roomParent.Y+1);
        LoadRoom(CurrentWorld + GetRandomRegularRoomName(), roomParent.X, roomParent.Y-1);

    }

    string GetRandomRegularRoomName()
    {
        string[] possibleRooms = new string[] { "Regular00", "Regular01", "Regular02" };
        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
}

public class RoomData{
    public string Name;
    public int X;
    public int Y;
}
