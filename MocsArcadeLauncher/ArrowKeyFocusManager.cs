using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataLayer;

namespace MocsArcadeLauncher
{
    public class ArrowKeySelectionManager
    {
        public List<GameTileUserControl> Tiles { get; set; }
        public int RowLength { get; set; }
        public int CurrentSelected { get; set; }
        public ArrowKeySelectionManager(List<GameTileUserControl> tiles, int rowLength)
        {
            Tiles = tiles;
            RowLength = rowLength;
            CurrentSelected = 0;
        }

        #region Changing The Selected Int

        public void ArrowDown()
        {
            var hold = RowLength + CurrentSelected;
            if (hold > Tiles.Count - 1)
            {
                hold = Tiles.Count - 1;
            }

            CurrentSelected = hold;
            //FocusManager.
        }
        public void ArrowUp()
        {
            var hold = CurrentSelected - RowLength;
            if (hold < 0)
            {
                hold = 0;
            }

            CurrentSelected = hold;
        }
        public void ArrowLeft()
        {
            var hold = CurrentSelected - 1;
            if (hold < 0)
            {
                hold = 0;
            }

            CurrentSelected = hold;
        }
        public void ArrowRight()
        {
            var hold = CurrentSelected + 1;
            if (hold > Tiles.Count - 1)
            {
                hold = Tiles.Count - 1;
            }

            CurrentSelected = hold;
        }

        #endregion

        #region ChangeFocus

        

        #endregion
    }
}
