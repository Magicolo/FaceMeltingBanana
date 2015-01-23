using UnityEngine;
using UnityEditor;

namespace Magicolo.EditorTools {
	[CustomPropertyDrawer(typeof(DisableAttribute))]
	public class DisableDrawer : CustomPropertyDrawerBase {

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			drawPrefixLabel = false;
			position = Begin(position, property, label);
		
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.PropertyField(position, property, label, true);
			EditorGUI.EndDisabledGroup();
			
			End(property);
		}
	}
}