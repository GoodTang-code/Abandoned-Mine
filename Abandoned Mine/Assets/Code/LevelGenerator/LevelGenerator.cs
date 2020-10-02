
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    //public Vector3Int tmpSize;

    public Texture2D map;
    public Tilemap tileMapDungeon;
    public ColorToPalette[] tileMappings;
    public ColorToPrefab[] prefabMappings;

    //public Vector3Int pos;

    //int width;
    //int height;

    void Start()
    {
        GenerateLevel();
    }
    // --------------------------------------------------------------------
    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenTile(x, y);
            }
        }
    }
    // --------------------------------------------------------------------
    void GenTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0) return; //Transparent

        //Generate Tile
        foreach (ColorToPalette mapping in tileMappings)
        {
            if (mapping.color.Equals(pixelColor))
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                //pos.x = x; // x + width / 2;
                //pos.y = y; //y + height / 2;
                //pos.z = 0;
                tileMapDungeon.SetTile(pos, mapping.ruleTile);
            }
        }

        //Generate Base and Camp
        foreach (ColorToPrefab mapping in prefabMappings)
        {
            if (mapping.color.Equals(pixelColor))
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                //pos.x = x; // x + width / 2;
                //pos.y = y; //y + height / 2;
                //pos.z = 0;
                Instantiate(mapping.prefab, pos, Quaternion.identity, transform);
            }
        }
    }
    // --------------------------------------------------------------------

    public void clearMap()
    {

        tileMapDungeon.ClearAllTiles();

    }
}
