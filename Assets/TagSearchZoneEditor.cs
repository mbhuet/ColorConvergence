using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TagSearchZone))]
public class TagSearchZoneEditor : Editor
{
    //first get tags from target
    //display a tag field for each one
    //
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Search for These Tags:");
        TagSearchZone myTarget = (TagSearchZone)target;
        string[] tags = myTarget.searchTags;
        if (tags == null) tags = new string[0];
        for(int i =0; i<tags.Length; i++)
        {
            myTarget.searchTags[i] = EditorGUILayout.TagField(myTarget.searchTags[i]);
        }
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("-", GUILayout.Width(20)) && tags.Length >0 )
        {
            string[] tagsMinus = new string[tags.Length-1];
            for (int i = 0; i < tagsMinus.Length; i++){
                tagsMinus[i] = tags[i];
            }
            myTarget.searchTags = tagsMinus;
        }
        if(GUILayout.Button("+", GUILayout.Width(20)))
        {
            string[] tagsPlus = new string[tags.Length + 1];
            tags.CopyTo(tagsPlus, 0);
            tagsPlus[tagsPlus.Length - 1] = "Untagged";
            myTarget.searchTags = tagsPlus;
        }
        GUILayout.EndHorizontal();



    }
}