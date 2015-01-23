using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;
using Magicolo.EditorTools;

namespace Magicolo.GraphicsTools {
	[CustomEditor(typeof(RenderReorder)), CanEditMultipleObjects]
	public class RenderReorderEditor : CustomEditorBase {

		RenderReorder renderReorder;
		List<Material> materials;
		SerializedProperty materialsProperty;
		Material currentMaterial;
		
		bool updateMaterials;
		
		public override void OnEnable() {
			base.OnEnable();
			
			renderReorder = (RenderReorder)target;
			updateMaterials = true;
		}
		
		public override void OnInspectorGUI() {
			Begin();
			
			ShowRenderType();
			ShowMaterials();
			
			End();
		}
		
		void ShowRenderType() {
			FindMaterials();
			
			EditorGUI.BeginChangeCheck();
			Popup(serializedObject.FindProperty("shader"), renderReorder.shaders);
			if (EditorGUI.EndChangeCheck() || updateMaterials) {
				UpdateShaders();
			}
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("renderType"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("offset"));
		}
		
		void ShowMaterials() {
			materials = renderReorder.materials;
			materialsProperty = serializedObject.FindProperty("materials");
			
			BeginBox();
			
			for (int i = 0; i < materialsProperty.arraySize; i++) {
				currentMaterial = materials[i];
				currentMaterial.renderQueue = (int)renderReorder.renderType + renderReorder.offset + materialsProperty.arraySize - i - 1;
				
				ShowMaterial();
				Reorderable(materialsProperty, i, true);
			}
			
			EndBox();
		}
		
		void ShowMaterial() {
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.BeginVertical();
			
			GUILayout.Space(8);
			EditorGUILayout.LabelField(GUIContent.none, new GUIStyle("RL DragHandle"), GUILayout.Width(20), GUILayout.Height(2));
			GUILayout.Space(2);
			
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.LabelField(currentMaterial.name);
			EditorGUILayout.LabelField(currentMaterial.renderQueue.ToString(), GUILayout.Width(32));
			
			EditorGUILayout.EndHorizontal();
		}
		
		void FindMaterials() {
			List<Material> materialList = new List<Material>();
			bool update = false;
			
			foreach (Renderer renderer in renderReorder.GetComponentsInChildren<Renderer>()) {
				Material material = renderer.sharedMaterial;
					
				if (material != null) {
					if (!materialList.Contains(material)) {
						materialList.Add(material);
					}
				}
			}
			
			for (int i = renderReorder.materials.Count - 1; i >= 0; i--) {
				Material material = renderReorder.materials[i];
				
				if (!materialList.Contains(material)) {
					renderReorder.materials.Remove(material);
					update = true;
				}
			}
			
			for (int i = materialList.Count - 1; i >= 0; i--) {
				Material material = materialList[i];
				
				if (!renderReorder.materials.Contains(material)) {
					renderReorder.materials.Add(material);
					update = true;
				}
			}
			
			if (update) {
				serializedObject.Update();
			}
		}
		
		void UpdateShaders() {
			foreach (Material material in renderReorder.materials) {
				material.shader = Shader.Find(renderReorder.shader);
			}
			
			SceneView.RepaintAll();
		}
	}
}
