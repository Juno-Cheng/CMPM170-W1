using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DonationManager : MonoBehaviour
{
    [SerializeField] GameObject dollarPrefab;
    [SerializeField] GameObject moneyPanel;
    [SerializeField] bool useCoins = false;
    [SerializeField] RectTransform donationBowl;

    public bool gamePlaying = false;

    HorizontalLayoutGroup hlg;

    float currentDonation = 0f;

    Dictionary<string, float> Money = new Dictionary<string, float>{
        { "Penny", 0.01f },
        { "Nickel", 0.05f },
        { "Dime", 0.10f },
        { "Quarter", 0.25f },
        { "One", 1f },
        { "Two", 2f },
        { "Five", 5f },
        { "Ten", 10f },
        { "Twenty", 20f },
        { "Fifty", 50f },
        { "Hundred", 100f }
    };
    public static DonationManager _instance;
    public static DonationManager Instance {get{return _instance;}}
    private void Awake(){
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance =  this;
        }
        hlg = moneyPanel.GetComponent<HorizontalLayoutGroup>();
    }
    List<string> denominations;
    List<string> coins = new List<string>();
    List<string> paper = new List<string>();
    void Start()
    {
        denominations = new List<string>(Money.Keys);
        for(int i=0;i<denominations.Count;i++){
            if(i<4){
                coins.Add(denominations[i]);
            }
            else{
                paper.Add(denominations[i]);
            }
        }
    }

    public void StartGame(){
        Debug.Log("Starting Donation Game");
        StartCoroutine(RandomStart());
    }

    IEnumerator RandomStart(){
        System.Random rand = new System.Random();
        int delay = rand.Next(5, 10);
        yield return new WaitForSeconds((float)delay);
        RandomDenominations();
        while(true){
            delay = rand.Next(15, 20);
            yield return new WaitForSeconds((float)delay);
            RandomDenominations();
        }
    }
    
    void RandomDenominations(){
        if(gamePlaying){return;}
        gamePlaying = true;
        StartCoroutine(MoveBowl(2600,400));
        StartCoroutine(MovePanel(200,400));
        List<string> randDenoms;
        if(useCoins){
            randDenoms = denominations;
        }
        else{
            randDenoms = paper;
        }
        List<string> solution = new List<string>();
        System.Random rand = new System.Random();
        float total = 0f;
        while(solution.Count<5){
            int index = rand.Next(0, randDenoms.Count);
            solution.Add(randDenoms[index]);

            total += Money[randDenoms[index]];
            SpawnDollar(Money[randDenoms[index]]);
        }
        List<string> inHand = new List<string>(solution);
        //int others = rand.Next(2, 5);
        int others = 2;
        for(int i = 0; i<others;i++){
            int index = rand.Next(0, randDenoms.Count);
            inHand.Add(randDenoms[index]);
            SpawnDollar(Money[randDenoms[index]]);
        }
        Debug.Log("You need to donate: $"+ total);
        Debug.Log(string.Format("You have: ({0}).", string.Join(", ", inHand)));
        Debug.Log(string.Format("The solution is: ({0}).", string.Join(", ", solution)));
        //hlg.enabled = false;
    }

    void SpawnDollar(float value){
        Vector3 targetPos = moneyPanel.transform.position;
        GameObject dollarObj = Instantiate(dollarPrefab, targetPos, Quaternion.identity);
        dollarObj.transform.SetParent(moneyPanel.transform);
        dollarObj.GetComponent<Money>().value = value;

    }

    IEnumerator MoveBowl(float distance, float speed){
        yield return new WaitForSeconds(1f);
        Vector3 pos = donationBowl.position;
        float moved = 0f;
        float percentage = 0f;
        while(percentage<1f){
            moved += speed * Time.deltaTime;
            percentage = moved/distance;
            donationBowl.position = Vector3.Lerp(pos, pos+Vector3.left*distance, percentage);
            yield return null;
        }
        gamePlaying = false;
    }
    IEnumerator MovePanel(float distance, float speed){
        Vector3 pos = moneyPanel.transform.position;
        float moved = 0f;
        float percentage = 0f;
        while(percentage<1f){
            moved += speed * Time.deltaTime;
            percentage = moved/distance;
            moneyPanel.transform.position = Vector3.Lerp(pos, pos+Vector3.up*distance, percentage);
            yield return null;
        }
        gamePlaying = false;
    }

    public RectTransform getDonationBowl(){
        return donationBowl;
    }
    public RectTransform getMoneyPanel(){
        return moneyPanel.GetComponent<RectTransform>();
    }

    public void addCurrentDonation(float value){
        currentDonation += value;
        Debug.Log(currentDonation);
    }
}
