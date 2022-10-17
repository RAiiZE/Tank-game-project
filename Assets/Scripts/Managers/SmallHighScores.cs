using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SmallHighScores : MonoBehaviour
{
    public int[] scores = new int[10];

    string currentDirectory;

    public string scoreFileName = "SmallHighScores.txt";

    void Start()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Our current directory is: " + currentDirectory);

        LoadScoresFromFile();
    }


    void Update()
    {

    }

    public void LoadScoresFromFile()
    {
        //before we try to read a file, we should checke that it exists. if it doesnt exist, we'll log a message and abort.
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if (fileExists == true)
        {
            Debug.Log("Found high score file " + scoreFileName);
        }
        else
        {
            Debug.Log("The file " + scoreFileName + " Does not exist. No scores will be loaded.", this);
            return;
        }

        //make a new array of default values. this ensures that no old values stick around if we've loaded a scores file in the past.
        scores = new int[scores.Length];

        //now we read the file in. We do this using a "StreamReader" which we give our full file path to. Don't forget the directory seperator bwtween the directory and the filename.
        StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFileName);

        //a counter to make sure we dont go past the end of our scores
        int scoreCount = 0;

        //a while loop which runs as long as there is data to be read AND we havnt reached the end of our scores array.
        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            // read that line into a variable
            string fileLine = fileReader.ReadLine();

            //try to parse that variable into an int, First make a variable to put it in
            int readScore = -1;

            //try to parse it
            bool didParse = int.TryParse(fileLine, out readScore);
            if (didParse)
            {
                //if we successfully read a number, put it in the array.
                scores[scoreCount] = readScore;
            }
            else
            {
                //if the number couldnt be parsed then we probably had junk in our file. lets print an error and then use a default value.
                Debug.Log("Invalid line in scores file at " + scoreCount + ", using default value.", this);
                scores[scoreCount] = 0;
            }
            //dont forget to increment the count
            scoreCount++;
        }

        //make sure to close the stream
        fileReader.Close();
        Debug.Log("High scores read from " + scoreFileName);
    }
    public void SaveScoresToFile()
    {
        //create a streamwriter for our file path
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);

        //write the lines to the file
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }

        // close the stream
        fileWriter.Close();

        //write a log message
        Debug.Log("High scores written to " + scoreFileName);
    }
    public void AddScore(int newScore)
    {
        //first up we find out what index it belongs at. this will be the first index with a score lower than the new score
        int desiredIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            //instead of checking the value of desiredIndex we could also use break to stop the loop
            if (scores[i] > newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }

        //if no desired index was found then the score isnt high enough to get on the table so we just abort
        if (desiredIndex < 0)
        {
            Debug.Log("Score of " + newScore + " not high enough for the high score list.", this);
            return;
        }

        //then we move all of the scores after that index back by one position. we'll do this by looping from the back of the array to our desired index.
        for (int i = scores.Length - 1; i > desiredIndex; i--)
        {
            scores[i] = scores[i - 1];
        }

        //insert our new score in its place
        scores[desiredIndex] = newScore;
        Debug.Log("Score of " + newScore + " entered into high scores at position " + desiredIndex, this);
    }
}
