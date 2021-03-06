﻿using System.Collections;
using Magicolo.GeneralTools;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Magicolo.EditorTools {
	public class CustomEditorBase : Editor {
	
		public enum ButtonAlign {
			None,
			Left,
			Right,
			Center
		}
		
		public delegate void AddCallback(SerializedProperty arrayProperty);
		public delegate void DeleteCallback(SerializedProperty arrayProperty, int indexToRemove);
		public delegate void DropCallback<T>(T droppedObject);
		public delegate void ReorderCallback(SerializedProperty arrayProperty, int sourceIndex, int targetIndex);
		public delegate void ClearCallback(SerializedProperty property);
		
		public Event currentEvent;
		public bool deleteBreak;
		
		public virtual void OnEnable() {
		}
		
		public virtual void OnDisable() {
		}
		
		public virtual void Begin(bool space = true) {
			currentEvent = Event.current;
			deleteBreak = false;
		
			if (space) {
				EditorGUILayout.Space();
			}
			
			Undo.RecordObject(target, string.Format("{0} ({1}) modified.", target.name, target.GetType()));
			
			EditorGUI.BeginChangeCheck();
			
			serializedObject.Update();
		}
	
		public virtual void End(bool space = true) {
			if (space) {
				EditorGUILayout.Space();
			}
			
			serializedObject.ApplyModifiedProperties();
			
			if (EditorGUI.EndChangeCheck()) {
				EditorUtility.SetDirty(target);
			}
		}
			
		#region Buttons
		public bool Button(GUIContent label, GUIStyle style, ButtonAlign align, bool disableOnPlay, params GUILayoutOption[] options) {
			if (style == null) {
				style = new GUIStyle("MiniToolbarButton");
				style.clipping = TextClipping.Overflow;
				style.contentOffset = new Vector2(2, 0);
			}
			
			EditorGUI.BeginDisabledGroup(Application.isPlaying && disableOnPlay);
			EditorGUILayout.BeginVertical();
			GUILayout.Space(1);
			EditorGUILayout.BeginHorizontal();
			
			if (align == ButtonAlign.Right || align == ButtonAlign.Center) {
				EditorGUILayout.Space();
			}
			
			bool pressed = GUILayout.Button(label, style, options);
			
			if (align == ButtonAlign.Left || align == ButtonAlign.Center) {
				EditorGUILayout.Space();
			}
			
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			EditorGUI.EndDisabledGroup();
			
			return pressed;
		}
		
		public bool Button(GUIContent label, ButtonAlign align, bool disableOnPlay, params GUILayoutOption[] options) {
			return Button(label, null, align, disableOnPlay, options);
		}
		
		public bool Button(GUIContent label, GUIStyle style, bool disableOnPlay, params GUILayoutOption[] options) {
			return Button(label, style, ButtonAlign.None, disableOnPlay, options);
		}
		
		public bool Button(GUIContent label, bool disableOnPlay, params GUILayoutOption[] options) {
			return Button(label, null, ButtonAlign.None, disableOnPlay, options);
		}
		
		public bool Button(GUIContent label, params GUILayoutOption[] options) {
			return Button(label, null, ButtonAlign.None, false, options);
		}

		public bool LargeButton(GUIContent label, bool disableOnPlay, params GUILayoutOption[] options) {
			bool pressed = false;
			
			GUIStyle style = new GUIStyle("toolbarButton");
			style.fontStyle = FontStyle.Bold;
			style.fontSize = 10;
			style.clipping = TextClipping.Overflow;
			style.contentOffset = new Vector2(2, 0);
			
			pressed = Button(label, style, disableOnPlay, options);
			GUILayout.Space(2);
			
			return pressed;
		}
		
		public bool LargeAddButton(SerializedProperty property, GUIContent label, AddCallback addCallback, params GUILayoutOption[] options) {
			label = label ?? property.displayName.ToGUIContent();
			
			bool pressed = false;
			if (LargeButton(label, true, options)) {
				AddToArray(property, addCallback);
				pressed = true;
			}
			return pressed;
		}
	
		public bool LargeAddButton(SerializedProperty property, AddCallback addCallback, params GUILayoutOption[] options) {
			return LargeAddButton(property, null, addCallback, options);
		}
	
		public bool LargeAddButton(SerializedProperty property, GUIContent label, params GUILayoutOption[] options) {
			return LargeAddButton(property, label, null, options);
		}

		public bool LargeAddButton(SerializedProperty property, params GUILayoutOption[] options) {
			return LargeAddButton(property, null, null, options);
		}

		public bool AddButton() {
			GUIStyle style = new GUIStyle("toolbarbutton");
			style.clipping = TextClipping.Overflow;
			style.contentOffset = new Vector2(0, -1);
			style.fontSize = 10;
			
			bool pressed = Button("+".ToGUIContent(), style, ButtonAlign.Right, true, GUILayout.Width(16));
			
			return pressed;
		}
		
		public bool AddButton(SerializedProperty property, AddCallback addCallback = null) {
			bool pressed = false;
			if (AddButton()) {
				AddToArray(property, addCallback);
				pressed = true;
			}
			return pressed;
		}
		
		public bool SmallAddButton() {
			GUIStyle style = new GUIStyle("MiniToolbarButtonLeft");
			style.clipping = TextClipping.Overflow;
			style.contentOffset = new Vector2(-1, -1);
			style.fontSize = 10;
			
			bool pressed = Button("+".ToGUIContent(), style, ButtonAlign.Right, true, GUILayout.Width(16));
			
			return pressed;
		}
		
		public bool SmallAddButton(SerializedProperty property, AddCallback addCallback = null) {
			bool pressed = false;
			if (SmallAddButton()) {
				AddToArray(property, addCallback);
				pressed = true;
			}
			return pressed;
		}
		
		public bool DeleteButton() {
			GUIStyle style = new GUIStyle("MiniToolbarButtonLeft");
			style.clipping = TextClipping.Overflow;
			
			bool pressed = Button("−".ToGUIContent(), style, ButtonAlign.Right, true, GUILayout.Width(16));
			
			return pressed;
		}
		
		public bool DeleteButton(SerializedProperty property, int indexToRemove, DeleteCallback deleteCallback = null) {
			bool pressed = false;
			if (DeleteButton()) {
				DeleteFromArray(property, indexToRemove, deleteCallback);
				pressed = true;
			}
			return pressed;
		}
		#endregion
		
		#region Foldouts
		public void Foldout(SerializedProperty property, GUIContent label, GUIStyle style) {
			label = label ?? property.displayName.ToGUIContent();
			style = style ?? EditorStyles.foldout;
			
			EditorGUILayout.BeginHorizontal();
			property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, style);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
		}
		
		public void Foldout(SerializedProperty property, GUIContent label) {
			Foldout(property, label, null);
		}
		
		public void Foldout(SerializedProperty property) {
			Foldout(property, null, null);
		}
		
		public void Foldout(IShowable showable, GUIContent label, GUIStyle style) {
			style = style ?? EditorStyles.foldout;
			
			EditorGUILayout.BeginHorizontal();
			showable.Showing = EditorGUILayout.Foldout(showable.Showing, label, style);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
		}
		
		public void Foldout(IShowable showable, GUIContent label) {
			Foldout(showable, label, null);
		}
		
		public void DropFoldout<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, GUIStyle style, DropCallback<T> dropCallback) where T : Object {
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			Foldout(property, label, style);
			EditorGUILayout.EndHorizontal();
			
			DropArea<T>(dropArea, disableOnPlay, dropCallback);
		}
		
		public void DropFoldout<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, DropCallback<T> dropCallback) where T : Object {
			DropFoldout<T>(property, disableOnPlay, label, null, dropCallback);
		}
		
		public void DropFoldout<T>(SerializedProperty property, bool disableOnPlay, DropCallback<T> dropCallback) where T : Object {
			DropFoldout<T>(property, disableOnPlay, null, null, dropCallback);
		}
		
		public void DropFoldout<T>(SerializedProperty property, GUIContent label, DropCallback<T> dropCallback) where T : Object {
			DropFoldout<T>(property, false, label, null, dropCallback);
		}
		
		public void DropFoldout<T>(SerializedProperty property, DropCallback<T> dropCallback) where T : Object {
			DropFoldout<T>(property, false, null, null, dropCallback);
		}
		
		public void DropFoldout<T>(IShowable showable, bool disableOnPlay, GUIContent label, GUIStyle style, DropCallback<T> dropCallback) where T : Object {
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			Foldout(showable, label, style);
			EditorGUILayout.EndHorizontal();
			
			DropArea<T>(dropArea, disableOnPlay, dropCallback);
		}
		
		public void DropFoldout<T>(IShowable showable, bool disableOnPlay, GUIContent label, DropCallback<T> dropCallback) where T : Object {
			DropFoldout<T>(showable, disableOnPlay, label, null, dropCallback);
		}
		
		public void DropFoldout<T>(IShowable showable, GUIContent label, DropCallback<T> dropCallback) where T : Object {
			DropFoldout<T>(showable, false, label, null, dropCallback);
		}
		
		public bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, GUIStyle style, AddCallback addCallback = null) {
			label.text += string.Format(" ({0})", GetArraySize(arrayProperty, overrideArraySize));
			
			EditorGUILayout.BeginHorizontal();
		
			if (showable.Showing && GetArraySize(arrayProperty, overrideArraySize) == 0) {
				showable.Showing = false;
			}
		
			Foldout(showable, label, style);
		
			bool pressed = false;
			if (showable.Showing && GetArraySize(arrayProperty, overrideArraySize) == 0 && !Application.isPlaying) {
				AddToArray(arrayProperty, addCallback);
				pressed = true;
			}
		
			if (AddButton(arrayProperty, addCallback)) {
				pressed = true;
			}
		
			EditorGUILayout.EndHorizontal();
			return pressed;
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, showable, overrideArraySize, label, null, addCallback);
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, showable, overrideArraySize, null, null, addCallback);
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, GUIContent label, GUIStyle style, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, showable, -1, label, style, addCallback);
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, GUIContent label, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, showable, -1, label, null, addCallback);
		}
		
		public bool AddFoldOut(SerializedProperty arrayProperty, IShowable showable, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, showable, -1, null, null, addCallback);
		}
		
		public bool AddFoldOut(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, GUIStyle style, AddCallback addCallback = null) {
			label = label ?? arrayProperty.displayName.ToGUIContent();
			label.text += string.Format(" ({0})", GetArraySize(arrayProperty, overrideArraySize));
		
			EditorGUILayout.BeginHorizontal();
		
			if (arrayProperty.isExpanded && GetArraySize(arrayProperty, overrideArraySize) == 0) {
				arrayProperty.isExpanded = false;
			}
			
			Foldout(arrayProperty, label, style);
		
			bool pressed = false;
			if (arrayProperty.isExpanded && GetArraySize(arrayProperty, overrideArraySize) == 0 && !Application.isPlaying) {
				AddToArray(arrayProperty, addCallback);
				pressed = true;
			}
		
			if (AddButton(arrayProperty, addCallback)) {
				pressed = true;
			}
		
			EditorGUILayout.EndHorizontal();
			return pressed;
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, overrideArraySize, label, null, addCallback);
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, int overrideArraySize, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, overrideArraySize, null, null, addCallback);
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, GUIContent label, GUIStyle style, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, -1, label, style, addCallback);
		}
	
		public bool AddFoldOut(SerializedProperty arrayProperty, GUIContent label, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, -1, label, null, addCallback);
		}

		public bool AddFoldOut(SerializedProperty arrayProperty, AddCallback addCallback = null) {
			return AddFoldOut(arrayProperty, -1, null, null, addCallback);
		}

		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			bool pressed = AddFoldOut(arrayProperty, showable, overrideArraySize, label, style, addCallback);
			EditorGUILayout.EndHorizontal();
			
			DropArea<T>(dropArea, true, dropCallback);

			return pressed;
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, showable, overrideArraySize, label, null, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, int overrideArraySize, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, showable, overrideArraySize, null, null, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, showable, -1, label, style, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, showable, -1, label, null, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, IShowable showable, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, showable, -1, null, null, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			Rect dropArea = EditorGUILayout.BeginVertical();
			bool pressed = AddFoldOut(arrayProperty, overrideArraySize, label, style, addCallback);
			EditorGUILayout.EndVertical();
			
			DropArea<T>(dropArea, true, dropCallback);

			return pressed;
		}
				
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, int overrideArraySize, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, overrideArraySize, label, null, dropCallback, addCallback);
		}
			
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, int overrideArraySize, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, overrideArraySize, null, null, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, -1, label, style, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, GUIContent label, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, -1, label, null, dropCallback, addCallback);
		}
		
		public bool AddFoldOut<T>(SerializedProperty arrayProperty, DropCallback<T> dropCallback, AddCallback addCallback = null) where T : Object {
			return AddFoldOut<T>(arrayProperty, -1, null, null, dropCallback, addCallback);
		}
		
		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) {
			EditorGUILayout.BeginHorizontal();
		
			SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex(index);
			Foldout(elementProperty, label, style);
			bool pressed = DeleteButton(arrayProperty, index, deleteCallback);
		
			EditorGUILayout.EndHorizontal();
			
			if (!pressed) {
				Reorderable(arrayProperty, index, true, reorderCallback);
			}
		
			return pressed;
		}
		
		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) {
			return DeleteFoldOut(arrayProperty, index, label, null, reorderCallback, deleteCallback);
		}
	
		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) {
			return DeleteFoldOut(arrayProperty, index, null, null, reorderCallback, deleteCallback);
		}
	
		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, DeleteCallback deleteCallback = null) {
			return DeleteFoldOut(arrayProperty, index, label, style, null, deleteCallback);
		}
	
		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, GUIContent label, DeleteCallback deleteCallback = null) {
			return DeleteFoldOut(arrayProperty, index, label, null, null, deleteCallback);
		}
	
		public bool DeleteFoldOut(SerializedProperty arrayProperty, int index, DeleteCallback deleteCallback = null) {
			return DeleteFoldOut(arrayProperty, index, null, null, null, deleteCallback);
		}
		
		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) where T : Object {
			Rect dropArea = EditorGUILayout.BeginHorizontal();
			bool pressed = DeleteFoldOut(arrayProperty, index, label, style, reorderCallback, deleteCallback);
			EditorGUILayout.EndHorizontal();
			
			DropArea<T>(dropArea, true, dropCallback);
			
			return pressed;
		}
			
		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, DropCallback<T> dropCallback, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) where T : Object {
			return DeleteFoldOut<T>(arrayProperty, index, label, null, dropCallback, reorderCallback, deleteCallback);
		}
		
		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, DropCallback<T> dropCallback, ReorderCallback reorderCallback, DeleteCallback deleteCallback = null) where T : Object {
			return DeleteFoldOut<T>(arrayProperty, index, null, null, dropCallback, reorderCallback, deleteCallback);
		}
		
		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, GUIStyle style, DropCallback<T> dropCallback, DeleteCallback deleteCallback = null) where T : Object {
			return DeleteFoldOut<T>(arrayProperty, index, label, style, dropCallback, null, deleteCallback);
		}
			
		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, GUIContent label, DropCallback<T> dropCallback, DeleteCallback deleteCallback = null) where T : Object {
			return DeleteFoldOut<T>(arrayProperty, index, label, null, dropCallback, null, deleteCallback);
		}
	
		public bool DeleteFoldOut<T>(SerializedProperty arrayProperty, int index, DropCallback<T> dropCallback, DeleteCallback deleteCallback = null) where T : Object {
			return DeleteFoldOut<T>(arrayProperty, index, null, null, dropCallback, null, deleteCallback);
		}
		#endregion

		#region Miscellaneous
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, bool allowSceneObjects, GUIContent label, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object {
			label = label ?? property.displayName.ToGUIContent();
			
			EditorGUI.BeginDisabledGroup(Application.isPlaying && disableOnPlay);
			EditorGUILayout.BeginHorizontal();
			
			EditorGUI.BeginChangeCheck();
			property.objectReferenceValue = EditorGUILayout.ObjectField(label, property.objectReferenceValue, typeof(T), allowSceneObjects, options);
			if (EditorGUI.EndChangeCheck()) {
				property.serializedObject.ApplyModifiedProperties();
			}
			
			ContextMenu(new []{ "Clear".ToGUIContent() }, new GenericMenu.MenuFunction2[]{ OnPropertyCleared }, new object[]{ new ContextMenuClearData(property, clearCallback) });
			
			EditorGUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();
		}
		
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, bool allowSceneObjects, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, disableOnPlay, allowSceneObjects, null, clearCallback, options);
		}
		
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, bool allowSceneObjects, GUIContent label, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, disableOnPlay, allowSceneObjects, label, null, options);
		}
		
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, disableOnPlay, true, label, clearCallback, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, disableOnPlay, true, null, clearCallback, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, GUIContent label, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, disableOnPlay, true, label, null, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, bool disableOnPlay, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, disableOnPlay, true, null, null, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, GUIContent label, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, false, true, label, clearCallback, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, ClearCallback clearCallback, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, false, true, null, clearCallback, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, GUIContent label, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, false, true, label, null, options);
		}
	
		public void PropertyObjectField<T>(SerializedProperty property, params GUILayoutOption[] options) where T : Object {
			PropertyObjectField<T>(property, false, true, null, null, options);
		}
	
		public void MinMaxSlider(SerializedProperty minProperty, SerializedProperty maxProperty, float min, float max, bool disableOnPlay) {
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			
			float minValue = 0;
			if (minProperty.propertyType == SerializedPropertyType.Integer) {
				minValue = (int)minProperty.GetValue();
			}
			else if (minProperty.propertyType == SerializedPropertyType.Float) {
				minValue = (float)minProperty.GetValue();
			}
			
			float maxValue = 0;
			if (maxProperty.propertyType == SerializedPropertyType.Integer) {
				maxValue = (int)maxProperty.GetValue();
			}
			else if (maxProperty.propertyType == SerializedPropertyType.Float) {
				maxValue = (float)maxProperty.GetValue();
			}
			
			EditorGUI.BeginDisabledGroup(Application.isPlaying && disableOnPlay);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.Width(indent * 15));
			
			minValue = EditorGUILayout.FloatField(minValue, GUILayout.MaxWidth((Screen.width - EditorGUI.indentLevel * 15) * 0.125F));
			EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, min, max);
			maxValue = EditorGUILayout.FloatField(maxValue, GUILayout.MaxWidth((Screen.width - EditorGUI.indentLevel * 15) * 0.125F));
			
			EditorGUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();
			
			minValue = Mathf.Clamp(minValue, min, maxValue);
			maxValue = Mathf.Clamp(maxValue, minValue, max);
			
			if (minProperty.propertyType == SerializedPropertyType.Integer) {
				minProperty.SetValue((int)minValue);
			}
			else if (minProperty.propertyType == SerializedPropertyType.Float) {
				minProperty.SetValue(minValue);
			}
			
			if (maxProperty.propertyType == SerializedPropertyType.Integer) {
				maxProperty.SetValue((int)maxValue);
			}
			else if (maxProperty.propertyType == SerializedPropertyType.Float) {
				maxProperty.SetValue(maxValue);
			}
			
			EditorGUI.indentLevel = indent;
		}
		
		public void MinMaxSlider(SerializedProperty minProperty, SerializedProperty maxProperty, float min, float max) {
			MinMaxSlider(minProperty, maxProperty, min, max, false);
		}
							
		public string Popup(string currentOption, string[] displayedOptions, GUIContent label, GUIStyle style, params GUILayoutOption[] options) {
			style = style ?? new GUIStyle("popup");
			int currentIndex = System.Array.IndexOf(displayedOptions, currentOption);
			return displayedOptions[Mathf.Clamp(EditorGUILayout.Popup(label, currentIndex, displayedOptions.ToGUIContents(), style, options), 0, Mathf.Max(displayedOptions.Length - 1, 0))];
		}
				
		public string Popup(string currentOption, string[] displayedOptions, GUIContent label, params GUILayoutOption[] options) {
			return Popup(currentOption, displayedOptions, label, null, options);
		}
				
		public void Popup(SerializedProperty stringProperty, string[] displayedOptions, GUIContent label, GUIStyle style, params GUILayoutOption[] options) {
			label = label ?? stringProperty.displayName.ToGUIContent();
			stringProperty.stringValue = Popup(stringProperty.stringValue, displayedOptions, label, style, options);
		}
								
		public void Popup(SerializedProperty stringProperty, string[] displayedOptions, GUIContent label, params GUILayoutOption[] options) {
			Popup(stringProperty, displayedOptions, label, null, options);
		}
										
		public void Popup(SerializedProperty stringProperty, string[] displayedOptions, params GUILayoutOption[] options) {
			Popup(stringProperty, displayedOptions, null, null, options);
		}

		public void DropArea<T>(Rect dropArea, bool disableOnPlay, DropCallback<T> dropCallback) where T : Object {
			if (Application.isPlaying && disableOnPlay) {
				return;
			}
			
			if (dropArea.Contains(currentEvent.mousePosition)) {
				if (DragAndDrop.objectReferences != null && DragAndDrop.objectReferences.Length > 0) {
					GameObject gameObject = DragAndDrop.objectReferences[0] as GameObject;
					T dropTarget = DragAndDrop.objectReferences[0] as T ?? gameObject == null ? default(T) : gameObject.GetComponent(typeof(T)) as T;
					
					if (dropTarget != null) {
						DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					}
				}
				
				if (currentEvent.type == EventType.DragPerform) {
					DragAndDrop.AcceptDrag();
						
					foreach (Object droppedElement in DragAndDrop.objectReferences) {
						GameObject gameObject = droppedElement as GameObject;
						T dropTarget = DragAndDrop.objectReferences[0] as T ?? gameObject == null ? default(T) : gameObject.GetComponent(typeof(T)) as T;
						
						if (dropTarget != null) {
							dropCallback(dropTarget);
						}
					}
					currentEvent.Use();
				}
			}
		}
		
		public void DropArea<T>(Rect dropArea, DropCallback<T> dropCallback) where T : Object {
			DropArea<T>(dropArea, false, dropCallback);
		}
				
		public void DropArea<T>(bool disableOnPlay, DropCallback<T> dropCallback) where T : Object {
			DropArea<T>(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), disableOnPlay, dropCallback);
		}

		public void DropArea<T>(DropCallback<T> dropCallback) where T : Object {
			DropArea<T>(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), false, dropCallback);
		}

		public void ContextMenu(Rect contextArea, GUIContent[] options, GenericMenu.MenuFunction[] callbacks) {
			if (currentEvent.type == EventType.ContextClick) {
				if (contextArea.Contains(currentEvent.mousePosition)) {
					GenericMenu menu = new GenericMenu();
					
					for (int i = 0; i < options.Length; i++) {
						menu.AddItem(options[i], false, callbacks[i]);
					}
					menu.ShowAsContext();
					
					currentEvent.Use();
				}
			}
		}
				
		public void ContextMenu(GUIContent[] options, GenericMenu.MenuFunction[] callbacks) {
			ContextMenu(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), options, callbacks);
		}
		
		public void ContextMenu(Rect contextArea, GUIContent[] options, GenericMenu.MenuFunction2[] callbacks, object[] data) {
			if (currentEvent.type == EventType.ContextClick) {
				if (contextArea.Contains(currentEvent.mousePosition)) {
					GenericMenu menu = new GenericMenu();
					
					for (int i = 0; i < options.Length; i++) {
						menu.AddItem(options[i], false, callbacks[i], data[i]);
					}
					menu.ShowAsContext();
					
					currentEvent.Use();
				}
			}
		}
				
		public void ContextMenu(GUIContent[] options, GenericMenu.MenuFunction2[] callbacks, object[] data) {
			ContextMenu(EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), options, callbacks, data);
		}
		
		public void Reorderable(SerializedProperty arrayProperty, int index, bool disableOnPlay, Rect dragArea, ReorderCallback reorderCallback = null) {
			if (Application.isPlaying && disableOnPlay) {
				return;
			}
			
			string arrayIdentifier = arrayProperty.name + arrayProperty.arraySize + arrayProperty.depth + arrayProperty.propertyPath + arrayProperty.propertyType;
			SerializedProperty selectedArray = DragAndDrop.GetGenericData(arrayIdentifier) as SerializedProperty;
			int selectedIndex = DragAndDrop.GetGenericData("Selected Index") == null ? -1 : (int)DragAndDrop.GetGenericData("Selected Index");
			int targetIndex = DragAndDrop.GetGenericData("Target Index") == null ? -1 : (int)DragAndDrop.GetGenericData("Target Index");
			GUIStyle selectedStyle = new GUIStyle("TL SelectionButton PreDropGlow");
			GUIStyle targetStyle = new GUIStyle("ColorPickerBox");
			
			switch (currentEvent.type) {
				case EventType.MouseDown:
					if (dragArea.Contains(currentEvent.mousePosition)) {
						DragAndDrop.PrepareStartDrag();
						DragAndDrop.SetGenericData(arrayIdentifier, arrayProperty);
						DragAndDrop.SetGenericData("Selected Index", index);
						DragAndDrop.objectReferences = new []{ arrayProperty.serializedObject.targetObject };
						currentEvent.Use();
					}
					break;
				case EventType.MouseDrag:
					if (selectedArray != null && selectedIndex == index) {
						DragAndDrop.StartDrag(string.Format("Dragging array element {0} at index {1}.", arrayProperty.name, index));
						currentEvent.Use();
					}
					break;
				case EventType.MouseUp:
					if (selectedArray != null && selectedIndex == index) {
						DragAndDrop.PrepareStartDrag();
						currentEvent.Use();
					}
					break;
				case EventType.DragUpdated:
					if (selectedArray != null) {
						if (selectedIndex == index) {
							GUI.Label(dragArea, GUIContent.none, selectedStyle);
						}
						else if (selectedIndex != -1 && dragArea.Contains(currentEvent.mousePosition)) {
							DragAndDrop.visualMode = DragAndDropVisualMode.Move;
							DragAndDrop.SetGenericData("Target Index", index);
							GUI.Label(dragArea, GUIContent.none, targetStyle);
							currentEvent.Use();
						}
					}
					break;
				case EventType.DragPerform:
					if (selectedArray != null && selectedIndex != -1 && dragArea.Contains(currentEvent.mousePosition)) {
						DragAndDrop.AcceptDrag();
						ReorderArray(arrayProperty, selectedIndex, targetIndex, reorderCallback);
						currentEvent.Use();
					}
					break;
				case EventType.DragExited:
					if (selectedArray != null && selectedIndex == index) {
						DragAndDrop.PrepareStartDrag();
						currentEvent.Use();
					}
					break;
				case EventType.Repaint:
					if (selectedArray != null) {
						if (selectedIndex == index) {
							GUI.Label(dragArea, GUIContent.none, selectedStyle);
						}
						else if (selectedIndex != -1 && dragArea.Contains(currentEvent.mousePosition)) {
							DragAndDrop.visualMode = DragAndDropVisualMode.Move;
							DragAndDrop.SetGenericData("Target Index", index);
							GUI.Label(dragArea, GUIContent.none, targetStyle);
						}
					}
					break;
			}
		}
		
		public void Reorderable(SerializedProperty arrayProperty, int index, Rect dragArea, ReorderCallback reorderCallback = null) {
			Reorderable(arrayProperty, index, false, dragArea, reorderCallback);
		}

		public void Reorderable(SerializedProperty arrayProperty, int index, bool disableOnPlay, ReorderCallback reorderCallback = null) {
			Reorderable(arrayProperty, index, disableOnPlay, EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), reorderCallback);
		}

		public void Reorderable(SerializedProperty arrayProperty, int index, ReorderCallback reorderCallback = null) {
			Reorderable(arrayProperty, index, EditorGUI.IndentedRect(GUILayoutUtility.GetLastRect()), reorderCallback);
		}

		public void Separator(bool reserveVerticalSpace = true) {
			if (reserveVerticalSpace) {
				GUILayout.Space(4);
				EditorGUILayout.LabelField(GUIContent.none, new GUIStyle("RL DragHandle"), GUILayout.Height(4));
				GUILayout.Space(4);
			}
			else {
				Rect position = EditorGUILayout.BeginVertical();
				position.y += 7;
				EditorGUI.LabelField(position, GUIContent.none, new GUIStyle("RL DragHandle"));
				EditorGUILayout.EndVertical();
			}
		}
		#endregion
		
		#region Utility
		public void BeginBox(GUIStyle style) {
			Rect rect = EditorGUILayout.BeginVertical();
			rect.x += EditorGUI.indentLevel * 15;
			rect.width -= EditorGUI.indentLevel * 15 - 2;
			rect.height += 1;
			
			GUI.Box(rect, "", style);
		}
	
		public void BeginBox() {
			BeginBox(new GUIStyle("box"));
		}
	
		public void EndBox() {
			EditorGUILayout.EndVertical();
		}

		public void AddToArray(SerializedProperty arrayProperty, AddCallback addCallback = null) {
			if (addCallback == null) {
				arrayProperty.arraySize += 1;
				arrayProperty.serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(arrayProperty.serializedObject.targetObject);
			}
			else {
				addCallback(arrayProperty);
			}
		}

		public void DeleteFromArray(SerializedProperty arrayProperty, int indexToRemove, DeleteCallback deleteCallback = null) {
			if (deleteCallback == null) {
				arrayProperty.GetArrayElementAtIndex(indexToRemove).SetValue(null);
				arrayProperty.DeleteArrayElementAtIndex(indexToRemove);
				arrayProperty.serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(arrayProperty.serializedObject.targetObject);
			}
			else {
				deleteCallback(arrayProperty, indexToRemove);
			}
			deleteBreak = true;
		}

		public void Clear(SerializedProperty property, ClearCallback clearCallback = null) {
			if (clearCallback == null) {
				if (property.isArray) {
					ClearArray(property);
				}
				else {
					property.SetValue(null);
					property.serializedObject.ApplyModifiedProperties();
					EditorUtility.SetDirty(property.serializedObject.targetObject);
				}
			}
			else {
				clearCallback(property);
			}
			deleteBreak = true;
		}
		
		public void ClearArray(SerializedProperty arrayProperty, ClearCallback clearCallback = null) {
			if (clearCallback == null) {
				arrayProperty.ClearArray();
				arrayProperty.serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(arrayProperty.serializedObject.targetObject);
			}
			else {
				clearCallback(arrayProperty);
			}
			deleteBreak = true;
		}
		
		public void ReorderArray(SerializedProperty arrayProperty, int sourceIndex, int targetIndex, ReorderCallback reorderCallback = null) {
			if (reorderCallback == null) {
				arrayProperty.MoveArrayElement(sourceIndex, targetIndex);
				serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(arrayProperty.serializedObject.targetObject);
			}
			else {
				reorderCallback(arrayProperty, sourceIndex, targetIndex);
			}
		}
		
		public int GetArraySize(SerializedProperty property, int overrideArraySize) {
			int arraySize = property.arraySize;
			if (overrideArraySize >= 0) {
				arraySize = overrideArraySize;
			}
			return arraySize;
		}
		#endregion
	
		void OnPropertyCleared(object data) {
			ContextMenuClearData contextData = data as ContextMenuClearData;
			Clear(contextData.property, contextData.clearCallback);
		}

		public class ContextMenuClearData {
			
			public readonly SerializedProperty property;
			public readonly ClearCallback clearCallback;
			
			public ContextMenuClearData(SerializedProperty property, ClearCallback clearCallback) {
				this.property = property;
				this.clearCallback = clearCallback;
			}

			public ContextMenuClearData(SerializedProperty property) {
				this.property = property;
			}
		}
	}
}