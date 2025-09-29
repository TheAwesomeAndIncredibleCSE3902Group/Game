using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Tiles;

namespace Sprint0.Commands
{
    public class CommandSwitchBlockSprite : ICommand
    {

        private int Column;
        private int Row;
        private int Id; 
        private Tilemap Tilemap;

        public CommandSwitchBlockSprite(Tilemap tilemap, int tile_id, int column, int row)
        {
            Tilemap = tilemap;
            Column = column;
            Row = row;
            Id = tile_id;
        }

        public void Execute()
        {
            Tilemap.SetTile(Column, Row, Id);
        }

    }
}
