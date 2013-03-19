using UnityEngine;
using System.Collections;

public class Helpers : System.Object
{
    public static Object Find(string name, System.Type type)
    {
        Object [] objs = Resources.FindObjectsOfTypeAll(type);
 
        foreach (Object obj in objs)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
 
        return null;
    }
}