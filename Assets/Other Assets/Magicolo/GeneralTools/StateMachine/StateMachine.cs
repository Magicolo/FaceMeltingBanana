using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Magicolo.GeneralTools;

namespace Magicolo {
	[AddComponentMenu("Magicolo/State Machine")]
	public class StateMachine : MonoBehaviourExtended, IStateMachine {

		[SerializeField] StateLayer[] layers = new StateLayer[0];
		Dictionary<string, IStateLayer> nameLayerDict;
		
		public void Awake() {
			BuildLayerDict();
			
			foreach (StateLayer layer in layers) {
				layer.OnAwake();
			}
		}
	
		public void Start() {
			foreach (StateLayer layer in layers) {
				layer.OnStart();
			}
		}
		
		public void Update() {
			foreach (StateLayer layer in layers) {
				layer.OnUpdate();
			}
		}
	
		public void FixedUpdate() {
			foreach (StateLayer layer in layers) {
				layer.OnFixedUpdate();
			}
		}
		
		public void LateUpdate() {
			foreach (StateLayer layer in layers) {
				layer.OnLateUpdate();
			}
		}

		public void OnMouseDown() {
			foreach (StateLayer layer in layers) {
				layer.MouseDown();
			}
		}
		
		public void OnMouseDrag() {
			foreach (StateLayer layer in layers) {
				layer.MouseDrag();
			}
		}
		
		public void OnMouseUp() {
			foreach (StateLayer layer in layers) {
				layer.MouseUp();
			}
		}
		
		public void OnMouseUpAsButton() {
			foreach (StateLayer layer in layers) {
				layer.MouseUpAsButton();
			}
		}
		
		public void OnMouseEnter() {
			foreach (StateLayer layer in layers) {
				layer.MouseEnter();
			}
		}
		
		public void OnMouseOver() {
			foreach (StateLayer layer in layers) {
				layer.MouseOver();
			}
		}
		
		public void OnMouseExit() {
			foreach (StateLayer layer in layers) {
				layer.MouseExit();
			}
		}
		
		public void OnCollisionEnter(Collision collision) {
			foreach (StateLayer layer in layers) {
				layer.CollisionEnter(collision);
			}
		}
	
		public void OnCollisionStay(Collision collision) {
			foreach (StateLayer layer in layers) {
				layer.CollisionStay(collision);
			}
		}

		public void OnCollisionExit(Collision collision) {
			foreach (StateLayer layer in layers) {
				layer.CollisionExit(collision);
			}
		}
	
		public void OnCollisionEnter2D(Collision2D collision) {
			foreach (StateLayer layer in layers) {
				layer.CollisionEnter2D(collision);
			}
		}
	
		public void OnCollisionStay2D(Collision2D collision) {
			foreach (StateLayer layer in layers) {
				layer.CollisionStay2D(collision);
			}
		}

		public void OnCollisionExit2D(Collision2D collision) {
			foreach (StateLayer layer in layers) {
				layer.CollisionExit2D(collision);
			}
		}
	
		public void OnTriggerEnter(Collider collision) {
			foreach (StateLayer layer in layers) {
				layer.TriggerEnter(collision);
			}
		}
	
		public void OnTriggerStay(Collider collision) {
			foreach (StateLayer layer in layers) {
				layer.TriggerStay(collision);
			}
		}

		public void OnTriggerExit(Collider collision) {
			foreach (StateLayer layer in layers) {
				layer.TriggerExit(collision);
			}
		}
	
		public void OnTriggerEnter2D(Collider2D collision) {
			foreach (StateLayer layer in layers) {
				layer.TriggerEnter2D(collision);
			}
		}
	
		public void OnTriggerStay2D(Collider2D collision) {
			foreach (StateLayer layer in layers) {
				layer.TriggerStay2D(collision);
			}
		}

		public void OnTriggerExit2D(Collider2D collision) {
			foreach (StateLayer layer in layers) {
				layer.TriggerExit2D(collision);
			}
		}
		
		public void OnBecameVisible() {
			foreach (StateLayer layer in layers) {
				layer.BecameVisible();
			}
		}
		
		public void OnBecameInvisible() {
			foreach (StateLayer layer in layers) {
				layer.BecameInvisible();
			}
		}

		public T GetLayer<T>() where T : IStateLayer {
			return (T)GetLayer(typeof(T).Name);
		}
		
		public IStateLayer GetLayer(System.Type layerType) {
			return GetLayer(layerType.Name);
		}
		
		public IStateLayer GetLayer(string layerName) {
			IStateLayer layer = null;
			
			try {
				layer = nameLayerDict[layerName];
			}
			catch {
				Logger.LogError(string.Format("Layer named {0} was not found.", layerName));
			}
			
			return layer;
		}
		
		void BuildLayerDict() {
			nameLayerDict = new Dictionary<string, IStateLayer>();
			
			foreach (StateLayer layer in layers) {
				if (layer != null) {
					nameLayerDict[layer.GetType().Name] = layer;
				}
			}
		}
	}
}
