using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject canvas;

    void Start(){
        Vector3 prevVec = Vector3.zero;
        for(int i = 1; i<=10; i++){
            prevVec = SpawnButton(i,prevVec);
        }
    }

    Vector3 SpawnButton(int value, Vector3 prevVec){
        Vector3 randomDir = Random.insideUnitCircle.normalized;
        float distance = Random.Range(100,500);
        Vector3 randomPos = (randomDir*distance)+prevVec;
        // while(randomPos!=Vector3.zero&&randomPos.x>100&&randomPos.x<1900&&randomPos.y>100&&randomPos.y<1000){
        //     distance = Random.Range(100,500);
        //     randomPos = (randomDir*distance)+prevVec;
        // }
        GameObject buttonObj = Instantiate(buttonPrefab, randomPos, Quaternion.identity);
        buttonObj.transform.SetParent(canvas.transform);
        buttonObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = value.ToString();
        return buttonObj.transform.position;
    }
}
