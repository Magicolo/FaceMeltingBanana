using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo.GeneralTools;

namespace Magicolo {
	public abstract class StateLayer : MonoBehaviourExtended, IStateLayer {
		
		public StateMachine machine;
		
		[SerializeField] State[] states = new State[0];
		[SerializeField] List<State> currentStates = new List<State>{ null };
		List<IState> activeStates;
		Dictionary<string, IState> nameStateDict;

		public virtual void OnAwake() {
			activeStates = new List<IState>(new IState[currentStates.Count]);
			
			BuildStateDict();
			
			foreach (State state in states) {
				state.OnAwake();
			}
		}

		public virtual void OnStart() {
			for (int i = 0; i < currentStates.Count; i++) {
				SwitchState(currentStates[i], i);
			}
			
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).OnStart();
			}
		}
		
		public virtual void OnUpdate() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).OnUpdate();
			}
		}
		
		public virtual void OnFixedUpdate() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).OnFixedUpdate();
			}
		}
			
		public virtual void OnLateUpdate() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).OnLateUpdate();
			}
		}
		
		public virtual void MouseDown() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseDown();
			}
		}
		
		public virtual void MouseDrag() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseDrag();
			}
		}
		
		public virtual void MouseUp() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseUp();
			}
		}
				
		public virtual void MouseUpAsButton() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseUpAsButton();
			}
		}
		
		public virtual void MouseEnter() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseEnter();
			}
		}
		
		public virtual void MouseOver() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseOver();
			}
		}
		
		public virtual void MouseExit() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).MouseExit();
			}
		}
		
		public virtual void CollisionEnter(Collision collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).CollisionEnter(collision);
			}
		}
	
		public virtual void CollisionStay(Collision collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).CollisionStay(collision);
			}
		}

		public virtual void CollisionExit(Collision collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).CollisionExit(collision);
			}
		}
	
		public virtual void CollisionEnter2D(Collision2D collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).CollisionEnter2D(collision);
			}
		}
	
		public virtual void CollisionStay2D(Collision2D collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).CollisionStay2D(collision);
			}
		}

		public virtual void CollisionExit2D(Collision2D collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).CollisionExit2D(collision);
			}
		}
	
		public virtual void TriggerEnter(Collider collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).TriggerEnter(collision);
			}
		}
	
		public virtual void TriggerStay(Collider collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).TriggerStay(collision);
			}
		}

		public virtual void TriggerExit(Collider collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).TriggerExit(collision);
			}
		}
	
		public virtual void TriggerEnter2D(Collider2D collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).TriggerEnter2D(collision);
			}
		}
	
		public virtual void TriggerStay2D(Collider2D collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).TriggerStay2D(collision);
			}
		}

		public virtual void TriggerExit2D(Collider2D collision) {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).TriggerExit2D(collision);
			}
		}

		public virtual void BecameVisible() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).BecameVisible();
			}
		}
		
		public virtual void BecameInvisible() {
			for (int i = 0; i < activeStates.Count; i++) {
				GetActiveState(i).BecameInvisible();
			}
		}
	
		public T SwitchState<T>(int index = 0) where T : IState {
			return (T)SwitchState(GetState(typeof(T).Name), index);
		}
				
		public IState SwitchState(System.Type stateType, int index = 0) {
			return SwitchState(GetState(stateType), index);
		}
		
		public IState SwitchState(string stateName, int index = 0) {
			return SwitchState(GetState(stateName), index);
		}
		
		public T GetActiveState<T>(int index = 0) where T : IState {
			return (T)GetActiveState(index);
		}
		
		public IState GetActiveState(int index = 0) {
			return activeStates[index] ?? EmptyState.Instance;
		}
		
		public IState[] GetActiveStates() {
			return activeStates.ToArray();
		}
		
		public T GetState<T>() where T : IState {
			return (T)GetState(typeof(T).Name);
		}
		
		public IState GetState(System.Type stateType) {
			return GetState(stateType.Name);
		}
		
		public IState GetState(string stateName) {
			IState state = EmptyState.Instance;
			
			try {
				state = nameStateDict[stateName];
			}
			catch {
				Logger.LogError(string.Format("State named {0} was not found.", stateName));
			}
			
			return state;
		}
		
		public T GetLayer<T>() where T : IStateLayer {
			return machine.GetLayer<T>();
		}
		
		public IStateLayer GetLayer(System.Type layerType) {
			return machine.GetLayer(layerType);
		}
		
		public IStateLayer GetLayer(string layerName) {
			return machine.GetLayer(layerName);
		}
		
		IState SwitchState(IState state, int index = 0) {
			state = state ?? EmptyState.Instance;
			
			GetActiveState(index).OnExit();
			activeStates[index] = state;
			currentStates[index] = state as State == null ? null : (State)state;
			state.OnEnter();
		
			return state;
		}
		
		void BuildStateDict() {
			nameStateDict = new Dictionary<string, IState>();
			
			nameStateDict[EmptyState.Instance.GetType().Name] = EmptyState.Instance;
			
			foreach (State state in states) {
				if (state != null) {
					nameStateDict[state.GetType().Name] = state;
				}
			}
		}
	}
}

