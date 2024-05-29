// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;

// public class TEST : MonoBehaviour
// {
//     [NamedArrayAttribute("Map", "Map 1", "Map 3")]
//     public float[] coisos;
// }

// public class NamedArrayAttribute : PropertyAttribute
// {
//     public string[] names;

//     public NamedArrayAttribute(params string[] names)
//     {
//         this.names = names;
//     }
// }

// [CustomPropertyDrawer(typeof(NamedArrayAttribute))]
// public class NamedArrayDrawer : PropertyDrawer
// {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         NamedArrayAttribute namedArray = attribute as NamedArrayAttribute;

//         if (property.propertyType == SerializedPropertyType.ArraySize)
//         {
//             EditorGUI.PropertyField(position, property, label, true);
//         }

//         else if (property.propertyType == SerializedPropertyType.Float)
//         {
//             int index = GetIndex(property);
//             if (index >= 0 && index < namedArray.names.Length)
//             {
//                 label.text = namedArray.names[index];
//             }

//             EditorGUI.PropertyField(position, property, label, true);
//         }
//         else
//         {
//             EditorGUI.PropertyField(position, property, label, true);
//         }
//     }

//     private int GetIndex(SerializedProperty property)
//     {
//         string path = property.propertyPath;
//         string indexStr = path.Substring(path.LastIndexOf("[") + 1);
//         indexStr = indexStr.Substring(0, indexStr.Length - 1);

//         if (int.TryParse(indexStr, out int index))
//         {
//             return index;
//         }
//         return -1;
//     }
// }
