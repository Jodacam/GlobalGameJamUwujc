using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Doozy.Engine.UI;

//Manager del juego. Aquí podemos guardar los progresos que se van haciendo e ir compronaod con los objetos.
public class GameManager : SerializedMonoBehaviour
{
    private static GameManager _instance;

    public bool pause = false;

    public UIView pauseMenu;
    public UIView load;

    public UIView startUi;

    public GameObject actualMap;

    public PlayerController[] players;

    public Dictionary<string, bool> fixedObjects;

    public Dictionary<string, GameObject> maps;

    public Sound loadSound;


    public static GameManager Instance
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
        this.startUi.Hide();
    }


    public void Pause()
    {
        this.pause = true;
        this.pauseMenu.Show();
    }

    public void UnPause()
    {
        this.pause = false;
        this.pauseMenu.Hide();
    }


    public void LoadMap(string map)
    {
        this.pause = true;
        loadSound.Play(transform);
        foreach (var player in players)
        {
            player.gameObject.SetActive(false);
        }
        StartCoroutine(loadMap(map));

    }


    public IEnumerator loadMap(string map)
    {
        load.Show();
        yield return new WaitForSeconds(1);

        var bullets = FindObjectsOfType<SiliconeBullet>();

        foreach (var bull in bullets)
        {
            bull.Reset();
        }

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

        pause = false;
        load.Hide();

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
