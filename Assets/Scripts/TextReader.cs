using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

//src for kjv bible https://github.com/ErikSchierboom/sentencegenerator/blob/master/samples/the-king-james-bible.txt
public class TextReader : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string textPath;
    [SerializeField] TextMeshProUGUI text;
    StreamReader sr;
    void Start()
    {
        //Debug.Log(textPath);
        sr = new StreamReader(textPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string getSection(){
        if (sr == null){
            return "";
        }
        string section = "";

        string line;
        while ((line = sr.ReadLine()) != null)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                section+=line;
            }
            else
            {
                break;
            }
        }

        return section;
    }

    public void changeText(){
        text.text=getSection();
    }
}
