using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    private List<GameObject> _sprites = new List<GameObject>(); // Store sprite item
    public Text countdown_box;
    public float countdown_time = 30; // Countdown timer
    private float time = 0;
    private float spawn_time = 0; // Time until spawn
    private void Start(){
        
    }
    private void Spawn(){ // Item spawner
        GameObject a = FoodSprite.Create("Apple", Vector3.one, Vector3.one);
        _sprites.Add(a);
    }
    private void Countdown(){ // Writes countdown text in screen
        countdown_box.text = countdown_time.ToString("0") + " Seconds";
    }
    private void Update(){
        time+= Time.deltaTime;
        if(countdown_time > 0){ // Countdown till game is finished
            countdown_time-= Time.deltaTime;
        }
        if((spawn_time == 0 || spawn_time < time) && countdown_time > 0){ // Spawn time always update when spawn time is hit
            spawn_time+= Random.Range(.3f, 1) + Time.deltaTime;

            Spawn();
        }

        // Move items 

        for(int i = 0; i < _sprites.Count; i+=1){
            GameObject _sprite = _sprites[i];
            FoodSprite sprite = _sprites[i].GetComponent<FoodSprite>();
            if(_sprite.transform.position.x >= 9.6){
                if(sprite.status == FoodSprite.FoodStatus.None){
                    sprite.Purchase();
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
                        _sprite.transform.Translate(new Vector3(-.05f, .06f * sprite.flight_direction,0));
                    }
                }                
            }
        }
        Countdown();
    }
}