﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Manager del juego. Aquí podemos guardar los progresos que se van haciendo e ir compronaod con los objetos.
public class GameManager : SerializedMonoBehaviour
{
    private GameManager _instance;

    public bool pause = false;


    public GameObject actualMap;

    public PlayerController[] players;

    public Dictionary<string, bool> fixedObjects;

    public Dictionary<string, GameObject> maps;


    public GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    void Start()
    {

    }


    public void LoadMap(string map)
    {
        this.pause = true;
        foreach (var player in players)
        {
            player.gameObject.SetActive(false);
        }
        StartCoroutine(loadMap(map));

    }


    public IEnumerator loadMap(string map)
    {
        yield return null;
        this.actualMap.SetActive(false);

        if (maps.ContainsKey(map))
        {
            this.actualMap = this.maps[map];
            this.actualMap.SetActive(true);
        }
        else
        {
            Debug.Log("Mapa no encontrado");
            this.actualMap.SetActive(true);

        }

        var objects = FindObjectsOfType<MapObject>();

        foreach (var mapObj in objects)
        {
            mapObj.OnMapLoad();
        }

        var spawn = FindObjectOfType<Spawner>();


        foreach (var player in players)
        {
            player.transform.position = spawn.transform.position;
            player.gameObject.SetActive(true);
        }


    }

    public void SetFix(string key, bool isFixed)
    {
        if (fixedObjects.ContainsKey(key))
        {
            fixedObjects[key] = isFixed;
        }
    }

    public bool getStateFixed(string key)
    {
        if (fixedObjects.ContainsKey(key))
        {
            return fixedObjects[key];
        }
        Debug.Log("Key: " + key + " No existe, devolviendo false");
        return false;
    }
}
