using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo {
	public abstract class Model : MonoBehaviourExtended {

		public enum ColliderTypes {
			Sphere,
			Box,
			Circle2D,
			Box2D,
			Polygon2D
		}
		
		public abstract Dictionary<string, Vector2[]> BoneNameVerticesDict { get; }
		
		[SerializeField, PropertyField(DisableOnPlay = true)]
		ColliderTypes colliderType;
		public ColliderTypes ColliderType {
			get {
				return colliderType;
			}
			set {
				if (colliderType != value) {
					colliderType = value;
					UpdateBoneColliders();
				}
			}
		}
		
		[SerializeField, PropertyField(typeof(MinAttribute), DisableOnPlay = true)]
		float colliderSize = 1;
		public float ColliderSize {
			get {
				return colliderSize;
			}
			set {
				if (colliderSize != value) {
					colliderSize = value;
					UpdateBoneColliders();
				}
			}
		}
		
		[SerializeField, PropertyField(DisableOnPlay = true)]
		bool generateColliders;
		public bool GenerateColliders {
			get {
				return generateColliders;
			}
			set {
				if (generateColliders != value) {
					generateColliders = value;
					UpdateBoneColliders();
				}
			}
		}

		[Button("Reset Colliders", "ResetColliders", NoPrefixLabel = true)] public bool resetColliders;
		void ResetColliders() {
			UpdateBoneColliders(true);
		}
		
		void UpdateBoneColliders(bool reset = false) {
			DestroyBoneColliders(reset);
			
			if (generateColliders) {
				GenerateBoneColliders();
			}
		}
		
		void GenerateBoneColliders() {
			foreach (string boneName in BoneNameVerticesDict.Keys) {
				GameObject bone = this.FindChildRecursive(boneName.Replace('.', '_'));
				GameObject boneCollider = bone.FindChild(bone.name + "Collider");
				
				if (boneCollider == null) {
					boneCollider = bone.AddChild(bone.name + "Collider");
					boneCollider.transform.SetPosition(transform.position);
					boneCollider.transform.rotation = Quaternion.identity;
					boneCollider.transform.SetScale(-transform.localScale.x, "X");
				}
				
				if (boneCollider != null) {
					switch (colliderType) {
						case ColliderTypes.Sphere:
							GenerateSphereCollider(boneCollider, BoneNameVerticesDict[boneName]);
							break;
						case ColliderTypes.Box:
							GenerateBoxCollider(boneCollider, BoneNameVerticesDict[boneName]);
							break;
						case ColliderTypes.Circle2D:
							GenerateCircleCollider2D(boneCollider, BoneNameVerticesDict[boneName]);
							break;
						case ColliderTypes.Box2D:
							GenerateBoxCollider2D(boneCollider, BoneNameVerticesDict[boneName]);
							break;
						case ColliderTypes.Polygon2D:
							GeneratePolygonCollider2D(boneCollider, BoneNameVerticesDict[boneName]);
							break;
					}
				}
			}
		}
		
		void GenerateSphereCollider(GameObject boneCollider, Vector2[] points) {
			float l = float.MaxValue;
			float r = float.MinValue;
			float d = float.MaxValue;
			float u = float.MinValue;
		
			for (int i = 0; i < points.Length; i++) {
				if (points[i].x < l) {
					l = points[i].x;
				}
			
				if (points[i].x > r) {
					r = points[i].x;
				}
			
				if (points[i].y < d) {
					d = points[i].y;
				}
			
				if (points[i].y > u) {
					u = points[i].y;
				}
			}
		
			float radius = Mathf.Max(Mathf.Abs(l - r), Mathf.Abs(d - u)) / 2 * colliderSize;
			if (radius >= 0.001F) {
				SphereCollider sphere = boneCollider.GetOrAddComponent<SphereCollider>();
				sphere.radius = radius;
				sphere.center = new Vector2(l + r, u + d) / 2;
			}
		}
		
		void GenerateBoxCollider(GameObject boneCollider, Vector2[] points) {
			float l = float.MaxValue;
			float r = float.MinValue;
			float d = float.MaxValue;
			float u = float.MinValue;
		
			for (int i = 0; i < points.Length; i++) {
				if (points[i].x < l) {
					l = points[i].x;
				}
			
				if (points[i].x > r) {
					r = points[i].x;
				}
			
				if (points[i].y < d) {
					d = points[i].y;
				}
			
				if (points[i].y > u) {
					u = points[i].y;
				}
			}
		
			Vector3 size = new Vector3(Mathf.Abs(l - r), Mathf.Abs(d - u), 1) * colliderSize;
			if (size.x >= 0.001F || size.y >= 0.001F) {
				BoxCollider box = boneCollider.GetOrAddComponent<BoxCollider>();
				box.size = new Vector2(Mathf.Max(size.x, 0.001F), Mathf.Max(size.y, 0.001F));
				box.center = new Vector2(l + r, u + d) / 2;
			}
		}

		void GenerateCircleCollider2D(GameObject boneCollider, Vector2[] points) {
			float l = float.MaxValue;
			float r = float.MinValue;
			float d = float.MaxValue;
			float u = float.MinValue;
		
			for (int i = 0; i < points.Length; i++) {
				if (points[i].x < l) {
					l = points[i].x;
				}
			
				if (points[i].x > r) {
					r = points[i].x;
				}
			
				if (points[i].y < d) {
					d = points[i].y;
				}
			
				if (points[i].y > u) {
					u = points[i].y;
				}
			}
		
			float radius = Mathf.Max(Mathf.Abs(l - r), Mathf.Abs(d - u)) / 2 * colliderSize;
			if (radius >= 0.001F) {
				CircleCollider2D circle2D = boneCollider.GetOrAddComponent<CircleCollider2D>();
				circle2D.radius = radius;
				circle2D.center = new Vector2(l + r, u + d) / 2;
			}
		}
		
		void GenerateBoxCollider2D(GameObject boneCollider, Vector2[] points) {
			float l = float.MaxValue;
			float r = float.MinValue;
			float d = float.MaxValue;
			float u = float.MinValue;
		
			for (int i = 0; i < points.Length; i++) {
				if (points[i].x < l) {
					l = points[i].x;
				}
			
				if (points[i].x > r) {
					r = points[i].x;
				}
			
				if (points[i].y < d) {
					d = points[i].y;
				}
			
				if (points[i].y > u) {
					u = points[i].y;
				}
			}
		
			Vector2 size = new Vector2(Mathf.Abs(l - r), Mathf.Abs(d - u)) * colliderSize;
			if (size.x >= 0.055F || size.y >= 0.055F) {
				BoxCollider2D box2D = boneCollider.GetOrAddComponent<BoxCollider2D>();
				box2D.size = new Vector2(Mathf.Max(size.x, 0.055F), Mathf.Max(size.y, 0.055F));
				box2D.center = new Vector2(l + r, u + d) / 2;
			}
		}
		
		void GeneratePolygonCollider2D(GameObject boneCollider, Vector2[] points) {
			float previousPointDistanceMultiplier = Random.Range(0.5F, 2F);
			float centerDistanceMultiplier = Random.Range(0.5F, 2F);
			float wrongDirectionMultiplier = Random.Range(0.5F, 2F);
			float minDistance = Random.Range(0.001F, 0.1F);
		
			List<Vector2> path = new List<Vector2>();
			List<Vector2> remainingPoints = new List<Vector2>(points);
		
			// Find center point
			Vector2 centerPoint = Vector2.zero;
		
			foreach (Vector2 point in points) {
				centerPoint += point;
			}
		
			centerPoint /= points.Length;
		
			// Find first point
			Vector2 firstPoint = new Vector2(float.MaxValue, 0);
		
			foreach (Vector2 point in remainingPoints) {
				if (point.x < firstPoint.x) {
					firstPoint = point;
				}
			}
		
			path.Add(remainingPoints.Pop(firstPoint));
		
			// Going up-left
			Vector2 bestPoint = -firstPoint;
		
			while (true) {
				float bestScore = float.MaxValue;
				bestPoint = firstPoint;
			
				for (int i = remainingPoints.Count - 1; i >= 0; i--) {
					Vector2 point = remainingPoints[i];
					float distanceFromPreviousPoint = Vector2.Distance(point, path.Last());
				
					if (distanceFromPreviousPoint < minDistance) {
						remainingPoints.Remove(point);
					}
					else if (point.y > path.Last().y) {
						float score = Vector2.Distance(point, path.Last()) * previousPointDistanceMultiplier;
						score -= Vector2.Distance(point, centerPoint) * centerDistanceMultiplier;
						score += point.x - path.Last().x * wrongDirectionMultiplier;
				
						if (score < bestScore) {
							bestScore = score;
							bestPoint = point;
						}
					}
				}
			
				if (bestPoint == firstPoint) {
					break;
				}
			
				path.Add(remainingPoints.Pop(bestPoint));
			}
		
			// Going right-up
			bestPoint = -firstPoint;
		
			while (true) {
				float bestScore = float.MaxValue;
				bestPoint = firstPoint;
			
				for (int i = remainingPoints.Count - 1; i >= 0; i--) {
					Vector2 point = remainingPoints[i];
					float distanceFromPreviousPoint = Vector2.Distance(point, path.Last());
				
					if (distanceFromPreviousPoint < minDistance) {
						remainingPoints.Remove(point);
					}
					else if (point.x > path.Last().x) {
						float score = Vector2.Distance(point, path.Last()) * previousPointDistanceMultiplier;
						score -= Vector2.Distance(point, centerPoint) * centerDistanceMultiplier;
						score += path.Last().y - point.y * wrongDirectionMultiplier;
				
						if (score < bestScore) {
							bestScore = score;
							bestPoint = point;
						}
					}
				}
			
				if (bestPoint == firstPoint) {
					break;
				}
			
				path.Add(remainingPoints.Pop(bestPoint));
			}
		
			// Going down-right
			bestPoint = -firstPoint;
		
			while (true) {
				float bestScore = float.MaxValue;
				bestPoint = firstPoint;
			
				for (int i = remainingPoints.Count - 1; i >= 0; i--) {
					Vector2 point = remainingPoints[i];
					float distanceFromPreviousPoint = Vector2.Distance(point, path.Last());
				
					if (distanceFromPreviousPoint < minDistance) {
						remainingPoints.Remove(point);
					}
					else if (point.y < path.Last().y) {
						float score = Vector2.Distance(point, path.Last()) * previousPointDistanceMultiplier;
						score -= Vector2.Distance(point, centerPoint) * centerDistanceMultiplier;
						score += path.Last().x - point.x * wrongDirectionMultiplier;
						if (score < bestScore) {
							bestScore = score;
							bestPoint = point;
						}
					}
				}
			
				if (bestPoint == firstPoint) {
					break;
				}
			
				path.Add(remainingPoints.Pop(bestPoint));
			}
		
			// Going left-down
			bestPoint = -firstPoint;
		
			while (true) {
				float bestScore = float.MaxValue;
				bestPoint = firstPoint;
			
				for (int i = remainingPoints.Count - 1; i >= 0; i--) {
					Vector2 point = remainingPoints[i];
					float distanceFromPreviousPoint = Vector2.Distance(point, path.Last());
				
					if (distanceFromPreviousPoint < minDistance) {
						remainingPoints.Remove(point);
					}
					else if (point.x < path.Last().x) {
						float score = distanceFromPreviousPoint * previousPointDistanceMultiplier;
						score -= Vector2.Distance(point, centerPoint) * centerDistanceMultiplier;
						score += point.y - path.Last().y * wrongDirectionMultiplier;
				
						if (score < bestScore) {
							bestScore = score;
							bestPoint = point;
						}
					}
				}
			
				if (bestPoint == firstPoint) {
					break;
				}
			
				path.Add(remainingPoints.Pop(bestPoint));
			}
		
			if (path.Count > 2) {
				PolygonCollider2D polygon2D = boneCollider.GetOrAddComponent<PolygonCollider2D>();
				polygon2D.SetPath(0, path.ToArray());
			}
			else {
				boneCollider.Remove();
			}
		}

		void DestroyBoneColliders(bool reset = false) {
			foreach (Collider child in GetComponentsInChildren<Collider>()) {
				if (reset) {
					child.gameObject.Remove();
				}
				else {
					child.Remove();
				}
			}
			
			foreach (Collider2D child in GetComponentsInChildren<Collider2D>()) {
				if (reset) {
					child.gameObject.Remove();
				}
				else {
					child.Remove();
				}
			}
		}
	}
}

