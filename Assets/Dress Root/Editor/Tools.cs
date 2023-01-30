using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public static class Tool  {

	// Use this for initialization
	[MenuItem("Tools/WrapNameSpace")]
	static	void  WrapNameSpace () {
		foreach(var file in Selection.objects)
		{
			 string myPath = AssetDatabase.GetAssetPath( file );

			 if(myPath.Contains(".cs"))
			 {
				 StreamReader reader = new StreamReader(myPath);
				 string content  = reader.ReadToEnd();
				 reader.Close();
				 content = content.Replace("public class", "namespace Dance { \n public class");
				 content += "\n}";

			 	Debug.Log(content);
				 

				 StreamWriter writer = new StreamWriter(myPath);
				writer.Write(content);
				writer.Flush();
				writer.Close();

			 }
			 Debug.Log(myPath);
		}
	}
	
	// Update is called once per frame
	
}
