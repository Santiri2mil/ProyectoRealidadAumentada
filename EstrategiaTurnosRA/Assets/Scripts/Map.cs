using System.Collections;
using System.Collections.Generic;
using System.IO;



public enum TileType
{
    WALL,
    GROUND,
}

public class Map
{
   
   public int width { get; protected set; }
    public int height { get; protected set; }

    private TileType[,] tilesData;
    
    public Map (TileType[,] tilesData)
    {
        this.tilesData = tilesData;
        this.width = this.tilesData.GetLength(0);//Ancho
        this.height = this.tilesData.GetLength(1);//Alto
    }
    public TileType GetTileType(int x, int y)//define el tipo de casilla
    {
        //define las paredes
        if(x<0||y<0)
        {
            return TileType.WALL;
        }

        if (x>=this.width||y>=this.height)
        {
            return TileType.WALL;

        }

        return this.tilesData[x, y];//nos regrersa la casilla


    }

    public static Map CreateWithStringData(string mapData) //crea el mapa en base a archivos de texto
    {
        StringReader reader = new StringReader(mapData);

        int mapWidth = 0;
        int mapHeight = 0;

        List<TileType> flatTilesData = new List<TileType>();


        while(true)
        {
            string line = reader.ReadLine();
            if(line==null)
            {
                break;
            }

            line = line.Trim();
            //linea vacia ignorar
            if(line.Length==0)
            {
                continue;
            }

            mapWidth = line.Length;
            mapHeight++;

            foreach(var letter in line)
            {
                switch(letter)
                {
                    case '#':
                        flatTilesData.Add(TileType.WALL);
                        break;
                    case '-':
                        flatTilesData.Add(TileType.GROUND);
                        break;

                }
            }


        }
        TileType[,] finalMapTiles = new TileType[mapWidth, mapHeight];
        for (int x=0;x<mapWidth;x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                finalMapTiles[x, y] = flatTilesData[y * mapWidth + x];
            }
        }


        return new Map(finalMapTiles);

    }



}
