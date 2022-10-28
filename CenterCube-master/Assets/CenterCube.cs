using System;
using System.Collections;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;
using Rnd = UnityEngine.Random;
using System.Collections.Generic;

struct Ring
{
    public int id;
    public Color color;
}
struct T12
{
    public Ring T1;
    public Ring T2;
}
public class CenterCube : MonoBehaviour
{
    public enum ColorName
    {
        Red,
        Green,
        Blue,
        Yellow,
        Cyan,
        Magenta,
        White,
        Black
    }

    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;
    public KMSelectable[] centers;
    public MeshRenderer[] ringColors;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private static Color[] color = { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta, Color.white};
    private List<Ring> listOfRings = new List<Ring>();
    private List<Color> intersects = new List<Color>();

    public string convertToBin(Color c)
    {
        string colorCode = "";
        if ((int)c.r == 1) colorCode += "1";
        else colorCode += "0";
        if ((int)c.g == 1) colorCode += "1";
        else colorCode += "0";
        if ((int)c.b == 1) colorCode += "1";
        else colorCode += "0";
        return colorCode;
    }
    public Color funnySwitchMoment(string binary)
    {
        switch (binary)
        {
            case "100": return Color.red;
            case "010": return Color.green;
            case "001": return Color.blue;
            case "101": return Color.magenta;
            case "110": return Color.yellow;
            case "011": return Color.cyan;
            case "111": return Color.white;
            default: return Color.black;
        }
    }
    public void justForColorLogging(int pos, int color)
    {
        switch (color)
        {
            case 0:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Red);
                break;
            case 1:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Green);
                break;
            case 2:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Blue);
                break;
            case 3:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Yellow);
                break;
            case 4:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Cyan);
                break;
            case 5:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Magenta);
                break;
            case 6:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.White);
                break;
            case 7:
                Debug.LogFormat("[CenterCube #{0}] Color {1}: {2}", _moduleId, pos + 1, ColorName.Black);
                break;
        }
    }


    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        T12 t12 = new T12();
        for (int i = 0; i < 12; i++)
        {
            Color col = color.PickRandom();
            Ring s;
            s.id = i;
            s.color = col;
            listOfRings.Add(s);
            ringColors[i].material.color = col;
            justForColorLogging(i, Array.IndexOf(color, col));
            if (i % 2 == 0) t12.T1 = listOfRings[i];
            else
            {
                t12.T2 = listOfRings[i];
                string value = (int.Parse(convertToBin(t12.T1.color)) ^ int.Parse(convertToBin(t12.T2.color))).ToString();
                while (value.Length != 3) value = "0" + value;
                Debug.Log(value);
                Debug.Log(funnySwitchMoment(value));
                intersects.Add(funnySwitchMoment(value));
            }
        }
        foreach (Color color in intersects) Debug.Log(color);
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} press # (to press the button a number of times. Bounded from answer to answer + 100)";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        yield return null;
        string[] param = command.Split(' ');

    }
    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;

    }
}
