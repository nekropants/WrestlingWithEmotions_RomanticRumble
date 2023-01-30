using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Battlehub.SplineEditor
{
    public static class SplineMenu
    {
        const string root = "Battlehub/";

        [MenuItem("Tools/Spline/Create")]
        public static void Create()
        {
            GameObject spline = new GameObject();
            spline.name = "Spline";
            Undo.RegisterCreatedObjectUndo(spline, "Battlehub.Spline.Create");

            Spline splineComponent = spline.AddComponent<Spline>();
            splineComponent.SetControlPointMode(ControlPointMode.Mirrored);

            Camera sceneCam = SceneView.lastActiveSceneView.camera;
            spline.transform.position = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 5f));

            Selection.activeGameObject = spline.gameObject;
        }

        [MenuItem("Tools/Spline/Set Mode/Free", validate = true)]
        private static bool CanSetFreeMode()
        {
            return CanSetMode();
        }

        [MenuItem("Tools/Spline/Set Mode/Aligned", validate = true)]
        private static bool CanSetAlignedMode()
        {
            return CanSetMode();
        }

        [MenuItem("Tools/Spline/Set Mode/Mirrored", validate = true)]
        private static bool CanSetMirroredMode()
        {
            return CanSetMode();
        }

        private static bool CanSetMode()
        {
            GameObject[] selected = Selection.gameObjects;
            return selected.Any(s => s.GetComponentInParent<Spline>());
        }

        [MenuItem("Tools/Spline/Set Mode/Free")]
        private static void SetFreeMode()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                SetMode(gameObjects[i], ControlPointMode.Free);
            }

        }

        [MenuItem("Tools/Spline/Set Mode/Aligned")]
        private static void SetAlignedMode()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                SetMode(gameObjects[i], ControlPointMode.Aligned);
            }
        }

        [MenuItem("Tools/Spline/Set Mode/Mirrored")]
        private static void SetMirroredMode()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                SetMode(gameObjects[i], ControlPointMode.Mirrored);
            }
        }

        private static void SetMode(GameObject selected, ControlPointMode mode)
        {
            Spline spline = selected.GetComponentInParent<Spline>();
            if (spline == null)
            {
                return;
            }

            SplineControlPoint selectedControlPoint = selected.GetComponent<SplineControlPoint>();
            Undo.RecordObject(spline, "Battlehub.Spline.SetMode");
            EditorUtility.SetDirty(spline);

            if (selectedControlPoint != null)
            {
                spline.SetControlPointMode(selectedControlPoint.Index, mode);
            }
            else
            {
                spline.SetControlPointMode(mode);
            }
        }

        [MenuItem("Tools/Spline/Append _&4", validate = true)]
        private static bool CanAppend()
        {
            GameObject selected = Selection.activeObject as GameObject;
            if (selected == null)
            {
                return false;
            }

            return selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Append _&4")]
        private static void Append()
        {
            GameObject selected = Selection.activeObject as GameObject;
            Spline spline = selected.GetComponentInParent<Spline>();
            Undo.RecordObject(spline, "Battlehub.Spline.Append");
            spline.Extend();
            EditorUtility.SetDirty(spline);
            Selection.activeGameObject = spline.GetComponentsInChildren<SplineControlPoint>().Last().gameObject;
        }

        [MenuItem("Tools/Spline/Prepend _&5", validate = true)]
        private static bool CanPrepend()
        {
            GameObject selected = Selection.activeObject as GameObject;
            if (selected == null)
            {
                return false;
            }

            return selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Prepend _&5")]
        private static void Prepend()
        {
            GameObject selected = Selection.activeObject as GameObject;
            Spline spline = selected.GetComponentInParent<Spline>();
            Undo.RecordObject(spline, "Battlehub.Spline.Prepend");
            spline.Extend(true);
            EditorUtility.SetDirty(spline);
            Selection.activeGameObject = spline.GetComponentsInChildren<SplineControlPoint>().First().gameObject;
        }

        [MenuItem("Tools/Spline/Remove Curve", validate = true)]
        private static bool CanRemove()
        {
            GameObject selected = Selection.activeObject as GameObject;
            if (selected == null)
            {
                return false;
            }

            return selected.GetComponent<SplineControlPoint>() && selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Remove Curve")]
        private static void Remove()
        {
            GameObject selected = Selection.activeObject as GameObject;
            SplineControlPoint ctrlPoint = selected.GetComponent<SplineControlPoint>();
            Spline spline = selected.GetComponentInParent<Spline>();
            Selection.activeGameObject = spline.gameObject;
            Undo.RecordObject(spline, "Battlehub.Spline.Remove");
            spline.Remove((ctrlPoint.Index - 1) / 3);
            EditorUtility.SetDirty(spline);
        }

        [MenuItem("Tools/Spline/Smooth", validate = true)]
        private static bool CanSmooth()
        {
            GameObject selected = Selection.activeObject as GameObject;
            if (selected == null)
            {
                return false;
            }

            return selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Smooth")]
        private static void Smooth()
        {
            GameObject selected = Selection.activeObject as GameObject;
            Spline spline = selected.GetComponentInParent<Spline>();
            Undo.RecordObject(spline, "Battlehub.Spline.Remove");
            spline.Smooth();
            EditorUtility.SetDirty(spline);
        }


        //[MenuItem("Tools/Spline/Save", validate = true)]
        //private static bool CanSave()
        //{
        //    GameObject selected = Selection.activeObject as GameObject;
        //    if (selected == null)
        //    {
        //        return false;
        //    }

        //    return selected.GetComponentInParent<Spline>();
        //}


        //[MenuItem("Tools/Spline/Save")]
        //private static void Save()
        //{
        //    GameObject selected = Selection.activeObject as GameObject;
        //    Spline spline = selected.GetComponentInParent<Spline>();
        //    Save(spline.gameObject, spline.name + ".prefab");
        //}
        //private static void Save(GameObject go, string name)
        //{
        //    if (!System.IO.Directory.Exists(Application.dataPath + "/" + root + "SavedPrefabs"))
        //    {
        //        AssetDatabase.CreateFolder("Assets/" + root.Remove(root.Length - 1), "SavedPrefabs");
        //    }

        //    CreatePrefab(name, go);
        //}


        //private static void CreatePrefab(string name, GameObject go)
        //{
        //    PrefabType prefabType = PrefabUtility.GetPrefabType(go);
        //    if (prefabType == PrefabType.Prefab || prefabType == PrefabType.PrefabInstance)
        //    {
        //        Object prefabParent = PrefabUtility.GetPrefabParent(go);
        //        string path = AssetDatabase.GetAssetPath(prefabParent);
        //        //PrefabUtility.ReplacePrefab(go, prefabParent, ReplacePrefabOptions.ConnectToPrefab);
        //        AssetDatabase.SaveAssets();
        //        Debug.Log("Prefab replaced: " + path);
        //    }
        //    else
        //    {
        //        string path = "Assets/" + root + "SavedPrefabs/" + name;
        //       //s string uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);
        //        PrefabUtility.CreatePrefab(path, go, ReplacePrefabOptions.ConnectToPrefab);
                
        //        Debug.Log("Prefab created: " + path);
        //    }
        //}


    }
}

