﻿using UnityEngine;
using System.Collections;
using Magicolo.GeneralTools;

namespace Magicolo {
	public abstract class State : MonoBehaviourExtended, IState {

		public StateLayer layer;
		public StateMachine machine;
		
		public virtual void OnEnter() {
		}

		public virtual void OnExit() {
		}
		
		public virtual void OnAwake() {
		}
	
		public virtual void OnStart() {
		}
		
		public virtual void OnUpdate() {
		}
		
		public virtual void OnFixedUpdate() {
		}
			
		public virtual void OnLateUpdate() {
		}
		
		public virtual void MouseDown() {
		}
		
		public virtual void MouseDrag() {
		}
		
		public virtual void MouseUp() {
		}
				
		public virtual void MouseUpAsButton() {
		}
		
		public virtual void MouseEnter() {
		}
		
		public virtual void MouseOver() {
		}
		
		public virtual void MouseExit() {
		}
		
		public virtual void CollisionEnter(Collision collision) {
		}
	
		public virtual void CollisionStay(Collision collision) {
		}

		public virtual void CollisionExit(Collision collision) {
		}
	
		public virtual void CollisionEnter2D(Collision2D collision) {
		}
	
		public virtual void CollisionStay2D(Collision2D collision) {
		}

		public virtual void CollisionExit2D(Collision2D collision) {
		}
	
		public virtual void TriggerEnter(Collider collision) {
		}
	
		public virtual void TriggerStay(Collider collision) {
		}

		public virtual void TriggerExit(Collider collision) {
		}
	
		public virtual void TriggerEnter2D(Collider2D collision) {
		}
	
		public virtual void TriggerStay2D(Collider2D collision) {
		}

		public virtual void TriggerExit2D(Collider2D collision) {
		}

		public virtual void BecameVisible() {
		}
		
		public virtual void BecameInvisible() {
		}
	
		public T SwitchState<T>(int index = 0) where T : IState {
			return layer.SwitchState<T>(index);
		}
				
		public IState SwitchState(System.Type stateType, int index = 0) {
			return layer.SwitchState(stateType.Name, index);
		}
		
		public IState SwitchState(string stateName, int index = 0) {
			return layer.SwitchState(stateName, index);
		}
		
		public T GetActiveState<T>(int index = 0) where T : IState {
			return layer.GetActiveState<T>(index);
		}
		
		public IState GetActiveState(int index = 0) {
			return layer.GetActiveState(index);
		}
		
		public IState[] GetActiveStates() {
			return layer.GetActiveStates();
		}
		
		public T GetState<T>() where T : IState {
			return layer.GetState<T>();
		}
		
		public IState GetState(System.Type stateType) {
			return layer.GetState(stateType);
		}
		
		public IState GetState(string stateName) {
			return layer.GetState(stateName);
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
	}
}
