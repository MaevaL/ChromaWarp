using UnityEngine;

namespace RoguePlateformer {
	public class Vector2i {

		public int x;
		public int y;

		public Vector2i(int x, int z) {
			this.x = x;
			this.y = z;
		}

		public Vector2i(int v) : this(v, v) { }

		public Vector2i() : this(0, 0) { }

		public override int GetHashCode() {
			return x + y * 666;
		}

		public static Vector2i operator +(Vector2i v1, Vector2i v2) {
			return new Vector2i(v1.x + v2.x, v1.y + v2.x);
		}

		public static Vector2i operator /(Vector2i v1, Vector2i v2) {
			return new Vector2i(v1.x / v2.x, v1.y / v2.y);
		}

		public static Vector2i operator /(Vector2i v, float d) {
			return new Vector2i();
		}

		public static explicit operator Vector2(Vector2i v) {
			return new Vector2(v.x, v.y);
		}

	}
}
