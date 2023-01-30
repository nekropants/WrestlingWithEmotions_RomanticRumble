using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Extension methods must be defined in a static class 
using UnityEngine.UI;

public static class Extensions
{
    public static string ValuesAsString<T>(this T[] arr)
    {
        string s = "";
        for (int i = 0; i < arr.Length; i++)
        {
            s += arr[i] + ", ";
        }

        return s;
    }


    public static void SetChildrenActive(this GameObject go, bool active)
    {
        foreach (Transform t in go.transform)
        {
            t.gameObject.SetActive(active);
        }
    }



    public static void BroadcastMessageToAllMonoBehaviours(this MonoBehaviour mono, string message)
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gameObjects)
        {
            go.SendMessage(message, SendMessageOptions.DontRequireReceiver);
        }
    }

    public static T GetComponentInHeirarchy<T>(this MonoBehaviour mono) where T : Component
    {
        Transform root = mono.transform;


        do
        {
            T comp = root.GetComponent<T>();
            if (comp != null)
                return comp;

            root = root.parent;
        }
        while (root != null);

        return null;
    }

    public static T GetClosest<T>(List<T> list, Vector2 from) where T : Component
    {
        T closest = null;
        float closestDist = float.MaxValue;

        for (int i = 0; i < list.Count; i++)
        {
            float dist = Vector2.SqrMagnitude(from - (Vector2) list[i].transform.position);
            if (closestDist > dist)
            {
                closest = list[i];
                closestDist = dist;
            }
        }

        return closest;
    }

    public static float GetClosest<T>(Vector2 from, List<T> list) where T : Component
    {
        float closestDist = float.MaxValue;

        for (int i = 0; i < list.Count; i++)
        {
            float dist = Vector2.SqrMagnitude(from - (Vector2)list[i].transform.position);
            if (closestDist > dist)
            {
            }
        }

        return closestDist;
    }

    public static void SetAlpha(this Text text, float alpha)
    {
        Color col = text.color;
        col.a = alpha;
        text.color = col;
    }
    public static Color SetAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
    public static Color SetIncrementAlpha(this Color color, float alphaIncrement)
    {
        color.a += alphaIncrement;
        return color;
    }
    public static T Clone<T>(this T monobehaviour) where T : MonoBehaviour
    {
        return GameObject.Instantiate(monobehaviour) as T;
    }

    public static GameObject Clone(this GameObject monobehaviour)
    {
        return GameObject.Instantiate(monobehaviour) as GameObject;
    }

    public static float Distance(this Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }

    public static float Distance(this Vector2 from, Vector2 to)
    {
        return Vector2.Distance(from, to);
    }

    public static float ManhattanDistance(this Vector3 from, Vector3 to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y) + Mathf.Abs(from.z - to.z);
    }

    public static float ManhattanDistance(this Vector2 from, Vector2 to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
    }

   
    public static T RandomElement<T>(this T[] array)
    {
   
        return array[Random.Range(0, array.Length)];
    }


     public static Vector3 BottomLeftWorldPos(this Camera cam)
    {
        return cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

     public static Vector3 BottomRightWorldPos(this Camera cam)
     {
         return cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
     }

    public static int RandomIndex<T>(this T[] array)
    {
        return Random.Range(0, array.Length);
    }

    public static void InitializeValues<T>(this List<T> array, T value)
    {
        for (int i = 0; i < array.Count; i++)
        {
            array[i] = value;
        }
    }


    public static bool DebugKey()
    {
        return Input.GetKeyDown(KeyCode.Space) && Application.isEditor;
    }
  
    public static void InitializeValues<T>(this T[] array, T value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = value;
        }
    }

   
    public static void StripNullValues(this IDictionary original)
    {
        object[] keys = new object[original.Count];
        original.Keys.CopyTo(keys, 0);

        for (int index = 0; index < keys.Length; index++)
        {
            var key = keys[index];
            if (key == null || original[key] == null)
            {
                original.Remove(key);
            }
        }
    }


    public static bool Contains<T>(this System.Array array, T element)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (element.Equals((T)array.GetValue(i)))
                return true;
        }

        return false;
    }


    public static GameObject GetGameObject(this Object obj)
    {
        Component comp = obj as Component;
        if (comp != null)
            return comp.gameObject;

        GameObject go = obj as GameObject;
        return go;
    }

    /// <summary>
    /// returns a sin value betweem 0 and 1
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float NormalizedSin(float t)
    {
        return (Mathf.Sin(t) + 1)/2;
    }
    public static void SetXY(this Transform t, Vector3 v)
    {

        Vector3 position = t.position;
        position.x = v.x;
        position.y = v.y;
        t.position = position;
    }
    public static void SetXY(this Transform t, float x, float y)
    {

        Vector3 position = t.position;
        position.x = x;
        position.y = y;
        t.position = position;
    }

    public static void SetX(this Transform t, float value)
    {

        Vector3 position = t.position;
        position.x = value;
        t.position = position;
    }

    public static void SetLocalXY(this Transform t, Vector3 value)
    {
        value.z = t.localPosition.z;
        t.localPosition = value;
    }

    public static void SetLocalXY(this Transform t, float x, float y)
    {
        Vector3 v = t.localPosition;
        v.x = x;
        v.y = y;
        t.localPosition = v;
    }

    public static void SetY(this Transform t, float value)
    {

        Vector3 position = t.position;
        position.y = value;
        t.position = position;
    }

    public static void SetZ(this Transform t, float value)
    {

        Vector3 position = t.position;
        position.z = value;
        t.position = position;
    }

    public static void SetLocalX(this Transform t, float value)
    {
        Vector3 position = t.localPosition;
        position.x = value;
        t.localPosition = position;
    }

    public static void FlipXScale(this Transform t)
    {
        Vector3 scale = t.localScale;
        scale.x = -scale.x;
        t.localScale = scale;
    }
    public static void SetLocalY(this Transform t, float value)
    {

        Vector3 position = t.localPosition;
        position.y = value;
        t.localPosition = position;
    }

    public static void SetLocalZ(this Transform t, float value)
    {

        Vector3 position = t.localPosition;
        position.z = value;
        t.localPosition = position;
    }


    public static void SetX(ref Vector3 v, float value)
    {
        v.x = value;
    }

    public static void SetY(ref Vector3 v, float value)
    {
        v.y = value;
    }

    public static void SetZ(ref Vector3 v, float value)
    {
        v.z = value;
    }

    public static Vector3 MouseWorldPos
    {
        get
        {
            Vector3 mouse = Input.mousePosition;
            mouse =  Camera.main.ScreenToWorldPoint(mouse);
            mouse.z = 0;
            return mouse;
        }
    }


    public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance, int layerMask)
    {
        return Raycast(origin, direction, out hit, distance, layerMask, true, Color.magenta, 10);
    }

    public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance, int layerMask, bool debugDraw, Color color, float debugDuration = 0)
    {
        if (debugDraw)
        {
            Debug.DrawRay(origin, direction * distance, color, debugDuration);
        }
        return Physics.Raycast(origin, direction, out hit, distance, layerMask);
    }
    public delegate void MethodSignature();
    public delegate void MethodSignature<T1>();

    public static void Invoke(this MonoBehaviour mono, MethodSignature method, float time)
    {
        mono.Invoke(method.Method.Name, time);
    }

    public static void DrawRect(Vector3 origin, float width, float height, Color color, float time)
    {
        Vector3 A = origin + new  Vector3  (-width, height, 0)/2;
        Vector3 B = origin + new  Vector3  (width, height, 0)/2;
        Vector3 C = origin + new  Vector3  (width, -height, 0)/2;
        Vector3 D = origin + new  Vector3  (-width, -height, 0)/2;
        Debug.DrawLine(A, B, color, time);
        Debug.DrawLine(B, C, color, time);
        Debug.DrawLine(C, D, color, time);
        Debug.DrawLine(D, A, color, time);
    }

    public static void DrawCircle(Vector3 origin, float radius, Color color, float duration)
    {
        Vector3 A = Vector3.up * radius + origin;
        int segments = 64;
        for (int i = 1; i < segments + 1; i++)
        {
            float angle = i / (float)segments;
            angle *= 360;
            Vector3 B = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * radius + origin;
//
            Debug.DrawLine(A, B, color, duration);
            A = B;
        }

    }

    /// <summary>
    /// getst the angle between 0 and 360
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>

    public static float Angle360(this Vector3 from, Vector3 to)
    {
        Vector3 fromN = from;

        fromN.z = 0;
        to.z = 0;

        fromN.Normalize();
        to.Normalize();

        if (from == to)
            return 0;

        if (from == -to)
            return 180;

        float angle = Vector3.Angle(to, fromN);
        Vector3 cross = Vector3.Cross(to, fromN);

        if (cross.z < 0)
            angle = -angle;

        if (angle < 0)
            angle += 360;


        return angle;
    }

    public static void SetAlpha(this SpriteSM sprite, float alpha)
    {
        Color col = sprite.color;
        col.a = alpha;
        sprite.SetColor( col);
    }
    public static void OffsetLowerLeftPixel(this SpriteSM sprite, Vector2 offset)
    {
        sprite.SetLowerLeftPixel(sprite.lowerLeftPixel + offset);
    }

    public static void OffsetLowerLeftPixel(this SpriteSM sprite, float x, float y)
    {
        sprite.SetLowerLeftPixel(sprite.lowerLeftPixel + new Vector2(x, y ));
    }

    public static void SetLowerLeftPixel_X(this SpriteSM sprite, float x)
    {
        sprite.SetLowerLeftPixel(x, sprite.lowerLeftPixel.y);
    }

    public static void SetLowerLeftPixel_Y(this SpriteSM sprite, float y)
    {
        sprite.SetLowerLeftPixel( sprite.lowerLeftPixel.x, y);
    }
    public static void SetAlpha(this Material mat, float alpha)
    {
        Color col = mat.GetColor("_TintColor");
        col.a = alpha;
        mat.SetColor("_TintColor", col);
    }

    public static void SetColor(this Material mat, Color col)
    {
        mat.SetColor("_TintColor", col);
    }

    public static void SetParticleColor(this ParticleSystem particleSystem, Color col)
    {

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].color = col;
        }
        particleSystem.SetParticles(particles, particles.Length);
        particleSystem.startColor = col;
    }



    //public static void SendMessage<T1>(this MonoBehaviour mono, MethodSignature<T1> method, T1 value)
    //{
    //    mono.SendMessage(method.Method.Name, value);
    //}

    //public static void SendMessage<T1>(this GameObject go, MethodSignature<T1> method, T1 value)
    //{
    //    go.SendMessage(method.Method.Name, value);
    //}
    //public static void SendMessage<T0, T1>(this MonoBehaviour mono, System.Func<T0, T1> method, T0 value) 
    //{
    //    mono.SendMessage(method.Method.Name, value);
    //}

    //public static void SendMessage<T0, T1>(this GameObject go, System.Func<T0, T1> method, T0 value) 
    //{
    //    go.SendMessage(method.Method.Name, value);
    //}

    // Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
    public static string ToHex( this Color32 color)
    {
        string hex = "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
         return hex;
     }
   

    public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color(r, g, b, 255);
    }
}


public static class Colors
{
    public static Color Orange { get { return  new Color(1, .5f, 0, 1 );}}
}
