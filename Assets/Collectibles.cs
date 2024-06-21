using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public static bool[] collected = new bool[4];
    public static void Collect(Collectible value)
    {
        collected[(int)value] = true;
    }
}
public enum Collectible
{
    RedKey,GreenKey,BlueKey,PrismaticGem
}