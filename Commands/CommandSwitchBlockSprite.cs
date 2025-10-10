using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AwesomeRPG.Map;

namespace AwesomeRPG.Commands
{
    public class CommandSwitchBlockSprite : ICommand
    {

        private int Column;
        private int Row;
        private int Direction;
        private readonly Tilemap Tilemap;

        public CommandSwitchBlockSprite(Tilemap tilemap, bool right, int column, int row)
        {
            Tilemap = tilemap;
            Column = column;
            Row = row;
            Direction = right ? -1 : 1;
            
        }

        public void Execute()
        {
            int tileId = Tilemap.GetTile(Column, Row).Id;
            int Size = Tilemap._tileset.Count;
            if (Direction + tileId >= Size)
            {
                Tilemap.SetTile(Column, Row, 0);
            }
            else if (Direction + tileId < 0)
            {
                Tilemap.SetTile(Column, Row, Size - 1);
            }
            else
            {
                Tilemap.SetTile(Column, Row, tileId + Direction);
            }
        }

    }
}
