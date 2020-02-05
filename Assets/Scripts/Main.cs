using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    private List<GameObject> _sprites = new List<GameObject>(); // Store sprite item
    private string jsonpath = "assets/json/data.json";
    public Text countdown_box;
    public float countdown_time = 30; // Countdown timer
    private float time = 0;
    private float spawn_time = 0; // Time until spawn
    private FoodItem[] items;
    private List<FoodItem> items_purchased = new List<FoodItem>();
    private void Start(){
        StreamReader stream = new StreamReader(jsonpath);
        string json = stream.ReadToEnd();
        items = JsonUtility.FromJson<FoodItems>(json).items;
        Time.timeScale = 0.0f;
    }
    private void Spawn(){ // Item spawner
        GameObject a = FoodSprite.Create("Apple", new Vector3(-10.0f,-2,0), Vector3.one);
        _sprites.Add(a);
    }
    private void Countdown(){ // Writes countdown text in screen
        countdown_box.text = countdown_time.ToString("0") + " Seconds";
    }
    private FoodItem GetItem(string name){
        return Array.Find(items, ele=>Equals(ele.name, name));
    }
    private void Update(){
        if(Time.timeScale == 0){
            return;
        }
        time+= Time.deltaTime;
        if(countdown_time > 0){ // Countdown till game is finished
            countdown_time-= Time.deltaTime;
        }
        if((spawn_time == 0 || spawn_time < time) && countdown_time > 0){ // Spawn time always update when spawn time is hit
            spawn_time+= UnityEngine.Random.Range(.3f, 1) + Time.deltaTime;
            Spawn();
        }

        // Move items 

        for(int i = 0; i < _sprites.Count; i+=1){
            GameObject _sprite = _sprites[i];
            FoodSprite sprite = _sprites[i].GetComponent<FoodSprite>();
            if(_sprite.transform.position.x >= 10){
                if(sprite.status == FoodSprite.FoodStatus.None){
                    sprite.Purchase();
                    items_purchased.Add(GetItem(_sprite.name));
                }
            }
            else{
                if(sprite.status == FoodSprite.FoodStatus.None){
                    _sprite.transform.Translate(new Vector3(.05f,0,0));
                }
                else{
                    if(sprite.opacity <= 0 || _sprite.transform.localScale[0] <= 0){
                        sprite.DestoryFood(_sprites, _sprite);
                    }else{

                        sprite.opacity-=.02f;

                        _sprite.transform.localScale += new Vector3(-.05f, -.05f, 0);
                        _sprite.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,sprite.opacity);
                        _sprite.transform.Translate(new Vector3(-.05f, .1f * sprite.flight_direction,0));
                    }
                }                
            }
        }
        Countdown();
    }
}