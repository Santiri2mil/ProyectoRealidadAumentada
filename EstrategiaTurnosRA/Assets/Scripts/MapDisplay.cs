using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public struct MapTilePair
{
    public TileType type;
    public Tile tile;
}

public class MapDisplay : MonoBehaviour
{
    public MapTilePair[] mapTilePairs;
    public Tilemap targetTileMap;

    private Map map;

    public Camera gameCamara;
    public Transform cursor;

    private Creature selectedCreature;
    private List<Creature> creatures;

    private void Start()
    {
        this.creatures = new List<Creature>();
    }
    public void RenderMapData(Map madpdata)
    {
        this.map = madpdata;
        for (int x=0;x<this.map.width;x++)
        {
            for (int y = 0; y < this.map.height; y++)
            {
                TileType type = this.map.GetTileType(x,y);

                Tile tile = this.GetTileForType(type);
                
                this.targetTileMap.SetTile(new Vector3Int(x, -y, 0), tile);
            }
        }
    }
    public void EmplaceCreature(Creature creature, Vector2Int localPosition)
    {
        if(this.creatures==null)
        {
            this.creatures = new List<Creature>();
        }

        Vector3 worldPosition = this.LocalToWorld(localPosition);

        creature.localPosition = localPosition;
        creature.transform.position = worldPosition;

        this.creatures.Add(creature);

    }
    private Tile GetTileForType(TileType type)
    {
        foreach(var pair in this.mapTilePairs)
        {
            if(pair.type== type)
            {
                return pair.tile;
            }
        }
        Debug.LogError("NO hay tiles para " + type);
        return null;

    }


    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Vector3 world = this.gameCamara.ScreenToWorldPoint(Input.mousePosition);
            var local = this.WorldToLocal(world);

            if (this.map.GetTileType(local.x, local.y) == TileType.GROUND)
            {
                this.cursor.gameObject.SetActive(true);
                this.cursor.position = this.LocalToWorld(local);
                Debug.Log("Es tierra");
            }
            else
            {
                this.cursor.gameObject.SetActive(false);
                
            }
        }
        if(Input.GetButtonDown("Fire1"))//AQUI SE MODIFICA PARA QUE FUNCIONE CON REALIDAD AUMENTADA
        {
            Vector3 world = this.gameCamara.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int local = this.WorldToLocal(world);

            if (this.selectedCreature!=null)
            {
                this.selectedCreature.SetSelectionStatus(false);
            }
            this.selectedCreature = this.GetCreatureAtPosition(local);

            if(this.selectedCreature!=null)
            {
                this.selectedCreature.SetSelectionStatus(true);
            }

        }
    }

    private Creature GetCreatureAtPosition(Vector2Int localPosition)
    {
        foreach(var creature in this.creatures)
        {
            Vector2Int pos = creature.localPosition;
            if(pos.x==localPosition.x&&pos.y==localPosition.y)
            {
                return creature;
            }
        }

        return null;
    }

    private Vector2Int WorldToLocal(Vector3 world)
    {
        Vector3 local = world - this.transform.position;

        int mapX = Mathf.FloorToInt(local.x);
        int mapY = Mathf.FloorToInt(local.z);

        return new Vector2Int(mapX, -mapY);

    }

    private Vector3 LocalToWorld(Vector2Int local)
    {
        Vector3 localF = new Vector3(local.x, 0, -local.y);
        return this.transform.position + localF + (Vector3.one * 0.5f);
    }


}
