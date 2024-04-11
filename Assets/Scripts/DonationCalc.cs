using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;

public class DonationCalc : MonoBehaviour
{
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

        RandomDenominations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void RandomDenominations(){
        List<string> solution = new List<string>();
        System.Random rand = new System.Random();
        float total = 0f;
        while(solution.Count<5){
            int index = rand.Next(0, Money.Count);
            solution.Add(denominations[index]);

            total += Money[denominations[index]];
        }
        List<string> inHand = new List<string>(solution);
        int others = rand.Next(2, 5);
        for(int i = 0; i<others;i++){
            int index = rand.Next(0, Money.Count);
            inHand.Add(denominations[index]);
        }
        Debug.Log("You need to donate: $"+ total);
        Debug.Log(string.Format("You have: ({0}).", string.Join(", ", inHand)));
        Debug.Log(string.Format("The solution is: ({0}).", string.Join(", ", solution)));
    }
}
