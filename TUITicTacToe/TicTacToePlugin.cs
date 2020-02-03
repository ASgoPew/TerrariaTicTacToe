using FakeProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TerrariaUI;
using TerrariaUI.Base;
using TerrariaUI.Base.Style;
using TerrariaUI.Widgets;
using TShockAPI;
using TUIPlugin;

namespace TUITicTacToe
{
    [ApiVersion(2, 1)]
    public class TicTacToePlugin : TerrariaPlugin
    {
        public TicTacToePlugin(Main game) : base(game)
        {
        }

        public override string Name => base.Name;

        public override Version Version => base.Version;

        public override string Author => base.Author;

        public override string Description => base.Description;

        public override void Initialize()
        {
            // Determine the position and size of the interface.
            int x = 100, y = 100, w = 50, h = 30;
            // Pass an empty provider to the panel (the interface will be drawn on Main.tile).
            object provider = FakeProviderAPI.CreateReadonlyTileProvider("TicTacToe", x, y, w, h);
            // Although we can use as a provider, for example, FakeTileRectangle from FakeManager:
            //object provider = FakeManager.FakeManager.Common.Add("TestPanelProvider", x, y, w, h);

            // Create a panel with a wall of diamond gemspark wall with black paint.
            Panel root = TUI.Create(new Panel("TicTacToe", x, y, w, h, null,
                new ContainerStyle() { Wall = WallID.DiamondGemspark, WallColor = PaintID.White }, provider)) as Panel;

            root.SetupGrid(new ISize[] { new Absolute(20), new Relative(100) });

            var menu = root[0, 0] = new VisualContainer(new ContainerStyle() { WallColor = PaintID.Black });
            menu.SetupLayout(Alignment.Up, Direction.Down, Side.Center, null, 0);
            menu.AddToLayout(new Button(0, 0, 20, 4, "Start", null, new ButtonStyle()
            {
                Wall = WallID.AmberGemspark,
                WallColor = PaintID.DeepGreen
            }, (self, touch) =>
            {
                for (int i = 0; i < root[1, 0].ChildCount; i++)
                    root[1, 0].GetChild(i).RemoveAll();
                root[1, 0].Update().Apply().Draw();
            }));
            menu.AddToLayout(new Button(0, 0, 20, 4, "help", null, new ButtonStyle()
            {
                Wall = WallID.AmberGemspark,
                WallColor = PaintID.Gray
            }, (self, touch) => TSPlayer.All.SendInfoMessage("HELP")));

            var game = root[1, 0] = new TicTacToe();
            game["turn"] = 0;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
