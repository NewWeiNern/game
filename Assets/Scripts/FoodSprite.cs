using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FoodSprite : MonoBehaviour {
    private Sprite sprite;
    private string jsonpath = "assets/json/data.json";
    public enum FoodStatus{
        None,
        Purchased,
        Discarded
    }
    public FoodStatus status = FoodStatus.None;
    public float opacity = 1f;
    public int flight_direction = 0; // Flight direction when clicked.
    void Start(){ 
        sprite = Resources.Load<Sprite>("Sprites/" + name);
        if(sprite){
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
        }
    }
    private void OnMouseDown(){
        Discard();
        GetItem();
    }
    /**
    * Set status of the food item to purchased
    */
    public void Purchase(){ 
        if(status == FoodStatus.None)
            status = FoodStatus.Purchased;
    }
    /**
    * Set status of the food item to discarded
    */
    public void Discard(){
        if(status == FoodStatus.None)
            status = FoodStatus.Discarded;
    }
    /**
    * Generate FoodSprite Object
    */
    public static GameObject Create(string type, Vector3 pos = default, Vector3 scale = default){
        GameObject go = new GameObject(type);
        BoxCollider2D collider = go.AddComponent<BoxCollider2D>();
        FoodSprite sprite = go.AddComponent<FoodSprite>();

        go.transform.position = pos;
        go.transform.localScale += scale;

        collider.isTrigger = true;
        sprite.flight_direction = Random.Range(0,2) * 2 - 1;

        return go;
    }
    public void DestoryFood(List<GameObject> list, GameObject obj){
        Resources.UnloadAsset(sprite);
        list.Remove(obj);
        Destroy(obj);
    }

    private void GetItem(){
        using(StreamReader stream = new StreamReader(jsonpath)){
            string json = stream.ReadToEnd();
            FoodItems util = JsonUtility.FromJson<FoodItems>(json);
        }
    }
}