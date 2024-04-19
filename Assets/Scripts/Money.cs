using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Money : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float value = 0f;
    public Sprite[] moneySpriteList;
    Image currentDollar;
    RectTransform goal;
    RectTransform panel;
    bool isMouseOver;
    Vector3 originalPos;
    Vector3 offset;
    bool moving = false;
    public void SetMouseOver(bool state){
        isMouseOver = state;
        if(isMouseOver){
            transform.localScale = Vector3.one*6f;
        }
        else{
            transform.localScale = Vector3.one*4f;
        }
    }
    void Awake(){
        currentDollar = GetComponent<Image>();
    }

    void Start(){
        goal = DonationManager.Instance.getDonationBowl();
        panel = DonationManager.Instance.getMoneyPanel();
        transform.Rotate(Vector3.forward, 90f);
    }
    public void SetDollarValue(float x){
        value = x;
        if(x==1){currentDollar.sprite=moneySpriteList[0];}
        else if(x==2){currentDollar.sprite=moneySpriteList[1];}
        else if(x==5){currentDollar.sprite=moneySpriteList[2];}
        else if(x==10){currentDollar.sprite=moneySpriteList[3];}
        else if(x==20){currentDollar.sprite=moneySpriteList[4];}
        else if(x==50){currentDollar.sprite=moneySpriteList[5];}
        else if(x==100){currentDollar.sprite=moneySpriteList[6];}
    }

    void Update(){
        
    }

    public void OnPointerEnter(PointerEventData eventData){
        SetMouseOver(true);
    }
    public void OnPointerExit(PointerEventData eventData){
        SetMouseOver(false);
    }
    //https://www.youtube.com/watch?v=kWRyZ3hb1Vc&ab_channel=CocoCode
    public void OnBeginDrag(PointerEventData eventData){
        if(moving){return;}
        originalPos = transform.position;
        offset = transform.position - Input.mousePosition;
    }
    public void OnDrag(PointerEventData eventData){
        if(moving){return;}
        SetMouseOver(true);
        transform.position = Input.mousePosition+offset;
    }
    public void OnEndDrag(PointerEventData eventData){
        if(moving){return;}
        if(CheckOverlap(goal)){
            DonationManager.Instance.addCurrentDonation(value);
            Destroy(gameObject);
        }
        // else if (!CheckOverlap(panel)){
        //     StartCoroutine(MoveBack());
        // }
        StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack(){
        moving = true;
        Vector3 pos = transform.position;
        float percentage = 0f;
        while(transform.position!=originalPos){
            percentage += 5*Time.deltaTime;
            transform.position = Vector3.Lerp(pos, originalPos, percentage);
            yield return null;
        }
        moving = false;
    }

    bool CheckOverlap(RectTransform other){
        RectTransform rt = GetComponent<RectTransform>();
        //https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping
        Rect rect1 = RectTransToRect(rt);
        Rect rect2 = RectTransToRect(other);
        return rect1.Overlaps(rect2);
    }

    Rect RectTransToRect(RectTransform rt){
        Vector2 position = rt.position;
        Vector2 size = rt.sizeDelta;

        Vector2 min = position - size / 2f;
        return new Rect(min.x, min.y, size.x, size.y);
    }
}
