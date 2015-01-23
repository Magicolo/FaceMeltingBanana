﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Magicolo.EditorTools {
	[System.Serializable]
	[CustomPropertyDrawer(typeof(ShowPropertiesAttribute)), CanEditMultipleObjects]
	public class ShowPropertiesDrawer : CustomPropertyDrawerBase {

		SerializedObject serialized;
		SerializedProperty iterator;
		float totalHeight;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			drawPrefixLabel = false;
			totalHeight = 0;
			
			Begin(position, property, label);
			
			position.height = EditorGUI.GetPropertyHeight(property, label, true);
			EditorGUI.PropertyField(position, property);
			totalHeight += position.height;
			position.y += position.height;
				
			if (property.objectReferenceValue != null) {
				serialized = new SerializedObject(property.objectReferenceValue);
				iterator = serialized.GetIterator();
				iterator.NextVisible(true);
				
				EditorGUI.indentLevel += 1;
				
				while (true) {
					if (iterator.type != "PPtr<MonoScript>" && iterator.propertyType != SerializedPropertyType.Generic) {
						position.height = EditorGUI.GetPropertyHeight(iterator, iterator.displayName.ToGUIContent(), true);
						totalHeight += position.height;
						EditorGUI.PropertyField(position, iterator);
						position.y += position.height;
					}
					
					if (!iterator.NextVisible(false)) {
						break;
					}
				}
				
				EditorGUI.indentLevel -= 1;
				
				serialized.ApplyModifiedProperties();
			}
			
			End(property);
		}
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return totalHeight;
		}
	}
}

