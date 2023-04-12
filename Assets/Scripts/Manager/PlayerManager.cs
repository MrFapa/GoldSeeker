using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;

    private bool spawned = false;

    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerManager>();
            return _instance;
        }
    }
    void Start()
    {
        if (_instance != null)
        {
            if (_instance != this)
                Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (spawned) return;

        Vector3 spawn = MapManager.Instance.GetRandomSpawn();

        this.player.transform.position = spawn + new Vector3(0.5f, 0.2f, 0.5f);
        spawned = true;
    }
}
