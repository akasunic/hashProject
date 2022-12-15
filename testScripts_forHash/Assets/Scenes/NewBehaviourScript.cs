using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.WSA;

public class NewBehaviourScript : MonoBehaviour
{
    //put all game objects you need to access here!!
    public GameObject cylinder;
    public GameObject cube;

    private GameObject[] objects; //in Start(), put all the objects in here! Will be useful for turning all to be inactive! 



    List<Action> actions = new List<Action>();

    private int numPermsPerCat = 16;//number of permutations per category
    private float picWaitTime = 1.0f;
    private int hashChar;
    private int hashCharPerm;
    private int hashLength = 40; //how many characters the hash should have/how long it should be
    private string[] abc = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };
    private int chosenPermutation;
    private int chosenOffset;
    private IEnumerator coroutine;
    //probably make hashes private
 
    private string[] hashes = new string[3];//consider how many hashes/pics you want to generate. Maybe 100? 
    private string[] hashPerms = new string[3]; //messyy... but make sure these the same
     
    //instantiate hash code arrays and mapping variables (case switch sort of set up)
    //instantiate all game objects you'll need--- don't store in array i don't think, too messy
    //

    //Awake is called before Start(). See Unity documentation for more.
    private void Awake()
    {

        //create the hashes
        for (int i=0; i<hashes.Length; i++)
        {
            chosenPermutation = UnityEngine.Random.Range(0, hashLength); //int that decides which of the characters in the hash to change
            chosenOffset = UnityEngine.Random.Range(0, numPermsPerCat - 1); //note, there are 15 options bc needs to be different than the current one. Going to offset by +1
            for (int b=0; b<hashLength; b++)
            {
                
                hashChar = UnityEngine.Random.Range(0, numPermsPerCat);
                hashes[i] += abc[hashChar]; //setting hashChar for this given space (and this given hash)
                if (b == chosenPermutation) //so if this is the chosen character to manipulate
                {
                    //let's offset the character choice by +1 for hashPerms
                    hashCharPerm = hashChar + chosenOffset;
                    if (hashCharPerm > numPermsPerCat-1)
                    {
                        hashCharPerm -= numPermsPerCat;
                    }
                    hashPerms[i] += abc[hashCharPerm];
                    
                }
                else
                {
                    hashPerms[i] += abc[hashChar];//right now, setting equal, wil change in a moment
                }
                
               
            }
            
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToCapture());
        StartCoroutine(WaitToCapturePerms());
    }

    void SetScene(string givenHash)
    {
        //for each character in the string, call the appropriate method. So will have 40 different methods!
        //rather than a for loop, call sequentially. Make sure you comment a ton!!!
        //
        choosePerson_0(givenHash); //in reality, would do, say, givenHash[3]. wonder if i should do a better mapping, though! Otherwise manually doing...

    }

    void choosePerson_0(string givenHashChar)
    {
        switch (givenHashChar)
        {
            case "a":
                //print("test");
                cube.SetActive(true);
                cylinder.SetActive(false);
                break;

            case "b":
                //print("test");
                cylinder.SetActive(true);
                cube.SetActive(false);
                break;

            case "c":
                cylinder.SetActive(true);
                cube.SetActive(true);
                //print("test");
                break;

            case "d":
                //print("test");
                break;

            case "e":
                //print("test");
                break;

            case "f":
                //print("test");
                break;

            case "g":
                //print("test");
                break;

            case "h":
                //print("test");
                break;

            case "i":
                //print("test");
                break;

            case "j":
                //print("test");
                break;

            case"k":

                break;

            case "l":   
                break;
            case "m":
                break;
            case "n":
                break;
            case "o":
                break;
            case "p":
                break;
            default:
                print("something's wrong");
                break;

        }
    }


 

    
    //want to generate hash and then take a new photo every 2 seconds
    private IEnumerator WaitToCapture()
    {
        DeactivateAllObjects(); //defaul to deactivating all, then only have to worry about what you want to be active!
        for (int i = 0; i < hashes.Length; i++)
        {
            print("main: " + hashes[i] + "; perm: " + hashPerms[i]);
            //note that really would just call SetScene(hashes[i]). Only using this to test!!
            if (i == 0)
            {
                SetScene("a"); //testing!!
            }
           else if (i == 1)
            {
                SetScene("b");//testing!
            }
           else if (i == 2)
            {
                SetScene("c");//testing!!
            }
            while(!System.IO.File.Exists("hashPic_" + hashes[i] + ".png"))
            {
                ScreenCapture.CaptureScreenshot("hashPic_" + hashes[i] + ".png");
                yield return null;
            }



            yield return new WaitForSeconds(picWaitTime);

        }

    }

 

    //also need to take a photo of the permutation! should probably store this better
    //remains to be seen: will this work when actually manipulating scene? Will more time be needed??
    private IEnumerator WaitToCapturePerms()
    {
        DeactivateAllObjects(); //defaul to deactivating all, then only have to worry about what you want to be active!
        for (int i = 0; i < hashes.Length; i++)
        {
            //note that really would just call SetScene(hashPerms[i]). Only using this to test!!
            if (i == 0)
            {
                SetScene("a"); //testing!!
            }
            else if (i == 1)
            {
                SetScene("b");//testing!
            }
            else if (i == 2)
            {
                SetScene("c");//testing!!
            }



            while (!System.IO.File.Exists("hashPicPerm_" + hashes[i] + "_" + hashPerms[i] + ".png"))
            {
                ScreenCapture.CaptureScreenshot("hashPicPerm_" + hashes[i] + "_" + hashPerms[i] + ".png");
                yield return null;
            }



            yield return new WaitForSeconds(picWaitTime);

        }

    }

    //NOTE!!! should deactivate subobjects whenever possible, but still keep main objects checked!!!
    public void DeactivateAllObjects()
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SAMPLESWITCH(string givenHashChar)
    {
        switch (givenHashChar)
        {
            case "a":
                //print("test");
                break;

            case "b":
                //print("test");
                break;

            case "c":
                //print("test");
                break;

            case "d":
                //print("test");
                break;

            case "e":
                //print("test");
                break;

            case "f":
                //print("test");
                break;

            case "g":
                //print("test");
                break;

            case "h":
                //print("test");
                break;

            case "i":
                //print("test");
                break;

            case "j":
                //print("test");
                break;

            case "k":

                break;

            case "l":
                break;
            case "m":
                break;
            case "n":
                break;
            case "o":
                break;
            case "p":
                break;
            default:
                print("something's wrong");
                break;

        }
    }


}



