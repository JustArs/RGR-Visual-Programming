using Avalonia;
using Avalonia.Controls;
using System.ComponentModel;

namespace LogicSimulator.Views.Shapes {
    public partial class Shifrator: GateBase, IGate, INotifyPropertyChanged {
        public override int TypeId => 4;

        public override int CountIns => 4;
        public override int CountOuts => 2;
        public override UserControl GetSelf() => this;
        protected override IGate GetSelfI => this;

        protected override void Init() {
            height = 30 * 4;
            InitializeComponent();
            DataContext = this;
        }

        /*
         * Обработка размеров внутренностей
         */

        public override Point[][] PinPoints { get {
            double X = EllipseSize - EllipseStrokeSize / 2;
            double X2 = base_size + width - EllipseStrokeSize / 2;
            double R = BodyRadius.TopLeft;
            double Y_s = R, Y_m = height / 2, Y_e = height - Y_s;
            double min = EllipseSize + BaseFraction * 2;
            // .1..2..3..4.
            double Y = Y_s + (Y_e - Y_s) / 8;
            double Y2 = Y_s + (Y_e - Y_s) / 8 * 3;
            double Y3 = Y_s + (Y_e - Y_s) / 8 * 5;
            double Y4 = Y_s + (Y_e - Y_s) / 8 * 7;
            if (Y2 - Y < min) { Y = Y_m - min / 2 * 3; Y2 = Y_m - min / 2; Y3 = Y_m + min / 2; Y4 = Y_m + min / 2 * 3; }
            double PinWidth = base_size - EllipseSize + PinStrokeSize;
            return new Point[][] {
                new Point[] { new(X, Y), new(X + PinWidth, Y) }, // Первый вход
                new Point[] { new(X, Y2), new(X + PinWidth, Y2) }, // Второй вход
                new Point[] { new(X, Y3), new(X + PinWidth, Y3) }, // Третий вход
                new Point[] { new(X, Y4), new(X + PinWidth, Y4) }, // Четвёртый вход
                new Point[] { new(X2, Y2), new(X2 + PinWidth, Y2) }, // Первый выход
                new Point[] { new(X2, Y3), new(X2 + PinWidth, Y3) }, // Второй выход
            };
        } }

        /*
         * Мозги
         */

        public void Brain(ref bool[] ins, ref bool[] outs) {
            bool a = ins[0], b = ins[1], c = ins[2], d = ins[3];
            if (d) {
                outs[0] = true;
                outs[1] = true;
            } else if (c) {
                outs[0] = false;
                outs[1] = true;
            } else if (b) {
                outs[0] = true;
                outs[1] = false;
            } else if (a) {
                outs[0] = false;
                outs[1] = false;
            }
        }
    }
}
