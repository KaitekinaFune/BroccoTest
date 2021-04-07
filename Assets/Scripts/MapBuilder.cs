using UnityEngine;

public class MapBuilder : MonoBehaviour
{
	public TextAsset mapJSON;
	public GameObject mapTilePrefab;

	public static Vector2 min;
	public static Vector2 max;

	[System.Serializable]
	public class Tile
	{
		public string Id;
		public string Type;
		public float Width;
		public float Height;
		public float X;
		public float Y;
	}
	
	[System.Serializable]
	public class TileList
	{
		public Tile[] tile;
	}

	public TileList tileList = new TileList();

	private void Awake()
	{
		tileList = JsonUtility.FromJson<TileList>(mapJSON.text);
		LoadTiles();
	}

	private void LoadTiles()
	{
		Tile lastTile = new Tile();

		foreach (Tile tile in tileList.tile)
		{
			CheckForGaps(tile, lastTile);

			Vector3 pos = new Vector3(tile.X, 0, tile.Y);
			GameObject currentTile = Instantiate(mapTilePrefab, pos, Quaternion.Euler(Vector3.right * 90));
			currentTile.transform.localScale = new Vector3(tile.Width, tile.Height, 1);

			currentTile.name = tile.Id;

			Texture2D texture = Resources.Load<Texture2D>(tile.Id);
			currentTile.GetComponent<MeshRenderer>().material.mainTexture = texture;

			currentTile.transform.SetParent(transform);

			// Временно сохраняет текущую клетку для проверки на "дырки" в карте
			lastTile = tile;

			// Сохраняет минимальные и максимальные X и Y для ограничения движения камеры
			GetMinMax(tile);
		}
	}

	void CheckForGaps(Tile tile, Tile lastTile)
	{
		if (lastTile == null) return;

		if (tile.X - lastTile.X > tile.Width)
		{
			tile.X = (lastTile.X + lastTile.Width / 2) + tile.Width / 2;
		}

		if (tile.Y - lastTile.Y > tile.Height)
		{
			tile.Y = (lastTile.Y + lastTile.Height / 2) + tile.Height / 2;
		}
	}

	void GetMinMax(Tile tile)
	{
		if (tile.X - tile.Width / 2 < min.x)
			min.x = tile.X - tile.Width / 2;
		if (tile.Y - tile.Height < min.y)
			min.y = tile.Y - tile.Height / 2;

		if (tile.X + tile.Width / 2 > max.x)
			max.x = tile.X + tile.Width / 2;
		if (tile.Y + tile.Height / 2 > max.y)
			max.y = tile.Y + tile.Height / 2;
	}
}
