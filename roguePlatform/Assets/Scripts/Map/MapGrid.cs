using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoguePlateformer {
	public class MapGrid {

		private Vector2i middle;

		public MapChunk[,] chunks;
		private float realChunkSize;
		private float realChunkWidth;
		private float realChunkHeight;

		private int index;

		public MapGrid(int sizeX, int sizeZ, float realWidth, float realHeight) {
			if (sizeX % 2 == 0) {
				sizeX++;
			}
			if (sizeZ % 2 == 0) {
				sizeZ++;
			}
			middle = new Vector2i(sizeX / 2, sizeZ / 2);
			chunks = new MapChunk[sizeX, sizeZ];
			realChunkSize = realWidth;
			realChunkWidth = realWidth;
			realChunkHeight = realHeight;
		}

		public void SetChunk(Vector2i fakeGridPos, MapChunk chunk) {
			Vector2i gridPos = FakeToRealGridPos(fakeGridPos);
			chunk.FakePos = fakeGridPos;
			if (chunks[gridPos.x, gridPos.y] != null) {
				GameObject.Destroy(chunks[gridPos.x, gridPos.y].gameObject);
			}
			chunks[gridPos.x, gridPos.y] = chunk;
			chunk.transform.position = new Vector3(realChunkWidth * fakeGridPos.x, realChunkHeight * fakeGridPos.y, 0f);
			chunk.gameObject.SetActive(true);
			//chunk.name = "Chunk " + (index++);
			chunk.name = "C" + (index++) + "-";
			foreach (Orientation ori in chunk.GetAllOpenSidesOrientations()) {
				chunk.name += ori.ToString()[0];
			}
			//chunk.gameObject.SetActive(false);
			UpdateAdjacents(gridPos);
		}

		private void UpdateAdjacents(Vector2i gridPos) {
			if (chunks[gridPos.x, gridPos.y + 1] != null) {
				chunks[gridPos.x, gridPos.y + 1].Bottom.adjacentChunk = chunks[gridPos.x, gridPos.y];
				chunks[gridPos.x, gridPos.y].Top.adjacentChunk = chunks[gridPos.x, gridPos.y + 1];
			}
			if (chunks[gridPos.x + 1, gridPos.y] != null) {
				chunks[gridPos.x + 1, gridPos.y].Left.adjacentChunk = chunks[gridPos.x, gridPos.y];
				chunks[gridPos.x, gridPos.y].Right.adjacentChunk = chunks[gridPos.x + 1, gridPos.y];
			}
			if (chunks[gridPos.x, gridPos.y - 1] != null) {
				chunks[gridPos.x, gridPos.y - 1].Top.adjacentChunk = chunks[gridPos.x, gridPos.y];
				chunks[gridPos.x, gridPos.y].Bottom.adjacentChunk = chunks[gridPos.x, gridPos.y - 1];
			}
			if (chunks[gridPos.x - 1, gridPos.y] != null) {
				chunks[gridPos.x - 1, gridPos.y].Right.adjacentChunk = chunks[gridPos.x, gridPos.y];
				chunks[gridPos.x, gridPos.y].Left.adjacentChunk = chunks[gridPos.x - 1, gridPos.y];
			}
		}

		public Orientation[] GetAdjacentSidesOccupied(Vector2i fakeGridPos) {
			List<Orientation> occupied = new List<Orientation>(4);
			Vector2i gridPos = FakeToRealGridPos(fakeGridPos);
			if (chunks[gridPos.x, gridPos.y + 1] != null) { // TOP
				occupied.Add(Orientation.TOP);
			}
			if (chunks[gridPos.x + 1, gridPos.y] != null) { // RIGHT
				occupied.Add(Orientation.RIGHT);
			}
			if (chunks[gridPos.x, gridPos.y - 1] != null) { // BOTTOM
				occupied.Add(Orientation.BOTTOM);
			}
			if (chunks[gridPos.x - 1, gridPos.y] != null) { // LEFT
				occupied.Add(Orientation.LEFT);
			}
			return occupied.ToArray();
		}

		public Orientation[] GetAdjacentSidesOccupied(Vector2i fakeGridPos, Orientation ignore) {
			List<Orientation> occupied = new List<Orientation>(4);
			Vector2i gridPos = FakeToRealGridPos(fakeGridPos);
			if (ignore != Orientation.TOP && chunks[gridPos.x, gridPos.y + 1] != null) { // TOP
				occupied.Add(Orientation.TOP);
			}
			if (ignore != Orientation.RIGHT && chunks[gridPos.x + 1, gridPos.y] != null) { // RIGHT
				occupied.Add(Orientation.RIGHT);
			}
			if (ignore != Orientation.BOTTOM && chunks[gridPos.x, gridPos.y - 1] != null) { // BOTTOM
				occupied.Add(Orientation.BOTTOM);
			}
			if (ignore != Orientation.LEFT && chunks[gridPos.x - 1, gridPos.y] != null) { // LEFT
				occupied.Add(Orientation.LEFT);
			}
			return occupied.ToArray();
		}

		public Orientation[] GetAdjacentSidesLeadingToTile(Vector2i fakeGridPos, Orientation ignore) {
			List<Orientation> occupied = new List<Orientation>(4);
			Vector2i gridPos = FakeToRealGridPos(fakeGridPos);
			if (ignore != Orientation.TOP && chunks[gridPos.x, gridPos.y + 1] != null && chunks[gridPos.x, gridPos.y + 1].Bottom.Type != SideType.CLOSED) { // TOP
				occupied.Add(Orientation.TOP);
			}
			if (ignore != Orientation.RIGHT && chunks[gridPos.x + 1, gridPos.y] != null && chunks[gridPos.x + 1, gridPos.y].Left.Type != SideType.CLOSED) { // RIGHT
				occupied.Add(Orientation.RIGHT);
			}
			if (ignore != Orientation.BOTTOM && chunks[gridPos.x, gridPos.y - 1] != null && chunks[gridPos.x, gridPos.y - 1].Top.Type != SideType.CLOSED) { // BOTTOM
				occupied.Add(Orientation.BOTTOM);
			}
			if (ignore != Orientation.LEFT && chunks[gridPos.x - 1, gridPos.y] != null && chunks[gridPos.x - 1, gridPos.y].Right.Type != SideType.CLOSED) { // LEFT
				occupied.Add(Orientation.LEFT);
			}
			return occupied.ToArray();
		}

		public Vector2i FakeToRealGridPos(Vector2i fakePos) {
			return fakePos + middle;
		}

		public Vector2i WorldToFakePos(Vector2 worldPos) {
			Vector2 pos = new Vector2();
			pos.x = (worldPos.x - realChunkWidth / 2) / realChunkWidth;
			pos.y = (worldPos.y - realChunkHeight / 2) / realChunkHeight;
			if (pos.x >= 0) pos.x++;
			if (pos.y >= 0) pos.y++;
			return new Vector2i((int) pos.x, (int) pos.y);
		}

		public Vector2i WorldToRealPos(Vector2 worldPos) {
			Vector2i pos = WorldToFakePos(worldPos);
			return FakeToRealGridPos(pos);
		}

		public MapChunk GetChunkAtWorldPos(Vector3 worldPos) {
			Vector2 wPos = new Vector2(worldPos.x, worldPos.z);
			Vector2i gridPos = WorldToRealPos(wPos);
			return chunks[gridPos.x, gridPos.y];
		}

		public MapChunk this[int fakeX, int fakeZ] {
			get {
				return chunks[fakeX + middle.x, fakeZ + middle.y];
			}
		}

	}
}
