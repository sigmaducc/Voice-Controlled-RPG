using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };

    Camera cam;
    PlayerMotor motor;
    public Vector3[] objectPosition;

    public string[] keywords;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;    

    protected KeywordRecognizer recognizer;
    protected string word;
    
    private void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        Data();
        VoiceInput();  
    }

    private void Data()
    {
        objectPosition = new Vector3[17];
        objectPosition[0] = GameObject.Find("Bridge").transform.position;
        int j = 1;
        for(int i = 1; i < 5; i++){
            objectPosition[i] = GameObject.Find((string)("Lamp"+ IntegerToWritten(j))).transform.position;
            j++;
        }
        int k = 1;
        for(int i = 5; i < 17; i++){
            objectPosition[i] = GameObject.Find((string)("Tree"+ IntegerToWritten(k))).transform.position;
            k++;
        }
        Debug.Log(objectPosition[5]);
        Debug.Log(objectPosition[6]);
        Debug.Log(objectPosition[7]);
        Debug.Log(objectPosition[8]);
    }
    private void VoiceInput()
    {
        keywords = new string[17];

	    keywords[0] = "Bridge";

        keywords[1] = "Lamp One";
        keywords[2] = "Lamp Two";
        keywords[3] = "Lamp Three";
        keywords[4] = "Lamp Four";

        keywords[5] = "Tree One";
        keywords[6] = "Tree Two";
        keywords[7] = "Tree Three";
        keywords[8] = "Tree Four";

        keywords[9] = "Tree Five";
        keywords[10] = "Tree Six";
        keywords[11] = "Tree Seven";
        keywords[12] = "Tree Eight";

        keywords[13] = "Tree Nine";
        keywords[14] = "Tree Ten";
        keywords[15] = "Tree Eleven";
        keywords[16] = "Tree Twelve";
        /*
        for(int i = 1; i < 5; i++)
        {
            int j = 1;
            keywords[i] = (string)("go to Lamp "+ IntegerToWritten(j));
            j++;
        }

        for(int i = 5; i < 17; i++)
        {
            int j = 1;
            keywords[i] = (string)("Tree "+ IntegerToWritten(j));
            j++;
        }*/

        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
            Debug.Log( recognizer.IsRunning );
        }

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }  
    }
    
    
    private void Run()
    {
        switch (word)
        {
            case "Bridge": motor.MoveToPoint(objectPosition[0]);
                break;
            case "Lamp One": motor.MoveToPoint(objectPosition[1]);
                break;
            case "Lamp Two": motor.MoveToPoint(objectPosition[2]);
                break;
            case "Lamp Three": motor.MoveToPoint(objectPosition[3]);
                break;
            case "Lamp Four": motor.MoveToPoint(objectPosition[4]);
                break;
            case "Tree One": motor.MoveToPoint(objectPosition[5]);
                break;
            case "Tree Two": motor.MoveToPoint(objectPosition[6]);
                break;
            case "Tree Three": motor.MoveToPoint(objectPosition[7]);
                break;
            case "Tree Four": motor.MoveToPoint(objectPosition[8]);
                break;
            case "Tree Five": motor.MoveToPoint(objectPosition[9]);
                break;
            case "Tree Six": motor.MoveToPoint(objectPosition[10]);
                break;
            case "Tree Seven": motor.MoveToPoint(objectPosition[11]);
                break;
            case "Tree Eight": motor.MoveToPoint(objectPosition[12]);
                break;
            case "Tree Nine": motor.MoveToPoint(objectPosition[13]);
                break;
            case "Tree Ten": motor.MoveToPoint(objectPosition[14]);
                break;
            case "Tree Eleven": motor.MoveToPoint(objectPosition[15]);
                break;
            case "Tree Twelve": motor.MoveToPoint(objectPosition[16]);
                break;
            /*for(int i = 1; i < 5; i++)
            {
                int j = 1;
                case ((string)(" Lamp "+ IntegerToWritten(j))) : motor.MoveToPoint(objectPosition[i]);
                j++;
                break;
            }

            for(int i = 5; i < 17; i++)
            {
                int j = 1;
                case ((string)(" Tree "+ IntegerToWritten(j))) : motor.MoveToPoint(objectPosition[i]);
                j++;
                break;
            }*/
        }
    }
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        Debug.Log(word);
        Run();
    }
    private string FriendlyInteger(int n, string leftDigits, int thousands)
    {
        if (n == 0)
        {
            return leftDigits;
        }

        string friendlyInt = leftDigits;

        if (friendlyInt.Length > 0)
        {
            friendlyInt += " ";
        }

        if (n < 10)
        {
            friendlyInt += ones[n];
        }
        else if (n < 20)
        {
            friendlyInt += teens[n - 10];
        }
        else if (n < 100)
        {
            friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
        }
        else if (n < 1000)
        {
            friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
        }
        else
        {
            friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands+1), 0);
            if (n % 1000 == 0)
            {
                return friendlyInt;
            }
        }

        return friendlyInt + thousandsGroups[thousands];
    }
    public string IntegerToWritten(int n)
    {
        if (n == 0)
        {
            return "Zero";
        }
        else if (n < 0)
        {
            return "Negative " + IntegerToWritten(-n);
        }

        return FriendlyInteger(n, "", 0);
    }
}
