using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using TerrariaUI.Base;
using TerrariaUI.Base.Style;
using TerrariaUI.Widgets;

namespace TUITicTacToe
{
    public class TicTacToe : VisualObject
    {
        public TicTacToe()
            : base(0, 0, 0, 0, null, null, null)
        {
            SetupGrid(new ISize[] { new Relative(33), new Relative(33), new Relative(34) },
                new ISize[] { new Relative(33), new Relative(33), new Relative(34) });

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    int cellType = (i + j) % 2;
                    this[i, j] = new VisualContainer(0, 0, 0, 0, null, new ContainerStyle()
                    {
                        Wall = cellType == 0 ? WallID.AmberGemspark : WallID.AmethystGemspark,
                        WallColor = PaintID.White
                    }, callback: (self, touch) =>
                    {
                        int turn = (int)self.Parent["turn"];
                        self["value"] = turn;
                        Image image = self.Add(new Image(0, 0, $"worldedit\\schematic-{(turn == 0 ? "X" : "O")}.dat")) as Image;
                        image.Style.TileColor = turn == 0 ? PaintID.DeepBlue : PaintID.DeepRed;
                        self.Update().Apply().Draw();
                        self.Parent["turn"] = 1 - turn;

                        // Check if someone has already won the game
                    });
                }
        }
    }
}
