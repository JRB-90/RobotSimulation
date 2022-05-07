using Avalonia.Input;
using JSim.Core.Input;

namespace JSim.OpenTK.Input
{
    internal static class KeyConverter
    {
        public static bool TryConvertKey(
            Key avaloniaKey,
            out Keys jsimKey)
        {
            switch (avaloniaKey)
            {
                case Key.None:
                    jsimKey = Keys.None;
                    return true;
                case Key.Cancel:
                    jsimKey = Keys.Cancel;
                    return true;
                case Key.Back:
                    jsimKey = Keys.Back;
                    return true;
                case Key.Tab:
                    jsimKey = Keys.Tab;
                    return true;
                case Key.LineFeed:
                    jsimKey = Keys.LineFeed;
                    return true;
                case Key.Clear:
                    jsimKey = Keys.Clear;
                    return true;
                case Key.Return:
                    jsimKey = Keys.Return;
                    return true;
                case Key.Pause:
                    jsimKey = Keys.Pause;
                    return true;
                case Key.CapsLock:
                    jsimKey = Keys.CapsLock;
                    return true;
                case Key.Escape:
                    jsimKey = Keys.Escape;
                    return true;
                case Key.Space:
                    jsimKey = Keys.Space;
                    return true;
                case Key.PageUp:
                    jsimKey = Keys.PageUp;
                    return true;
                case Key.PageDown:
                    jsimKey = Keys.PageDown;
                    return true;
                case Key.End:
                    jsimKey = Keys.End;
                    return true;
                case Key.Home:
                    jsimKey = Keys.Home;
                    return true;
                case Key.Left:
                    jsimKey = Keys.Left;
                    return true;
                case Key.Up:
                    jsimKey = Keys.Up;
                    return true;
                case Key.Right:
                    jsimKey = Keys.Right;
                    return true;
                case Key.Down:
                    jsimKey = Keys.Down;
                    return true;
                case Key.Select:
                    jsimKey = Keys.Select;
                    return true;
                case Key.Print:
                    jsimKey = Keys.Print;
                    return true;
                case Key.Execute:
                    jsimKey = Keys.Execute;
                    return true;
                case Key.Snapshot:
                    jsimKey = Keys.Snapshot;
                    return true;
                case Key.Insert:
                    jsimKey = Keys.Insert;
                    return true;
                case Key.Delete:
                    jsimKey = Keys.Delete;
                    return true;
                case Key.Help:
                    jsimKey = Keys.Help;
                    return true;

                case Key.NumLock:
                    jsimKey = Keys.NumLock;
                    return true;
                case Key.Scroll:
                    jsimKey = Keys.Scroll;
                    return true;
                case Key.LeftShift:
                    jsimKey = Keys.LShiftKey;
                    return true;
                case Key.RightShift:
                    jsimKey = Keys.RShiftKey;
                    return true;
                case Key.LeftCtrl:
                    jsimKey = Keys.LControlKey;
                    return true;
                case Key.RightCtrl:
                    jsimKey = Keys.RControlKey;
                    return true;
                case Key.LeftAlt:
                    jsimKey = Keys.Alt;
                    return true;
                case Key.RightAlt:
                    jsimKey = Keys.Alt;
                    return true;

                case Key.F1:
                    jsimKey = Keys.F1;
                    return true;
                case Key.F2:
                    jsimKey = Keys.F2;
                    return true;
                case Key.F3:
                    jsimKey = Keys.F3;
                    return true;
                case Key.F4:
                    jsimKey = Keys.F4;
                    return true;
                case Key.F5:
                    jsimKey = Keys.F5;
                    return true;
                case Key.F6:
                    jsimKey = Keys.F6;
                    return true;
                case Key.F7:
                    jsimKey = Keys.F7;
                    return true;
                case Key.F8:
                    jsimKey = Keys.F8;
                    return true;
                case Key.F9:
                    jsimKey = Keys.F9;
                    return true;
                case Key.F10:
                    jsimKey = Keys.F10;
                    return true;
                case Key.F11:
                    jsimKey = Keys.F11;
                    return true;
                case Key.F12:
                    jsimKey = Keys.F12;
                    return true;
                case Key.F13:
                    jsimKey = Keys.F13;
                    return true;
                case Key.F14:
                    jsimKey = Keys.F14;
                    return true;
                case Key.F15:
                    jsimKey = Keys.F15;
                    return true;
                case Key.F16:
                    jsimKey = Keys.F16;
                    return true;
                case Key.F17:
                    jsimKey = Keys.F17;
                    return true;
                case Key.F18:
                    jsimKey = Keys.F18;
                    return true;
                case Key.F19:
                    jsimKey = Keys.F19;
                    return true;
                case Key.F20:
                    jsimKey = Keys.F20;
                    return true;
                case Key.F21:
                    jsimKey = Keys.F21;
                    return true;
                case Key.F22:
                    jsimKey = Keys.F22;
                    return true;
                case Key.F23:
                    jsimKey = Keys.F23;
                    return true;
                case Key.F24:
                    jsimKey = Keys.F24;
                    return true;

                case Key.D0:
                    jsimKey = Keys.D0;
                    return true;
                case Key.D1:
                    jsimKey = Keys.D1;
                    return true;
                case Key.D2:
                    jsimKey = Keys.D2;
                    return true;
                case Key.D3:
                    jsimKey = Keys.D3;
                    return true;
                case Key.D4:
                    jsimKey = Keys.D4;
                    return true;
                case Key.D5:
                    jsimKey = Keys.D5;
                    return true;
                case Key.D6:
                    jsimKey = Keys.D6;
                    return true;
                case Key.D7:
                    jsimKey = Keys.D7;
                    return true;
                case Key.D8:
                    jsimKey = Keys.D8;
                    return true;
                case Key.D9:
                    jsimKey = Keys.D9;
                    return true;

                case Key.A:
                    jsimKey = Keys.A;
                    return true;
                case Key.B:
                    jsimKey = Keys.B;
                    return true;
                case Key.C:
                    jsimKey = Keys.C;
                    return true;
                case Key.D:
                    jsimKey = Keys.D;
                    return true;
                case Key.E:
                    jsimKey = Keys.E;
                    return true;
                case Key.F:
                    jsimKey = Keys.F;
                    return true;
                case Key.G:
                    jsimKey = Keys.G;
                    return true;
                case Key.H:
                    jsimKey = Keys.H;
                    return true;
                case Key.I:
                    jsimKey = Keys.I;
                    return true;
                case Key.J:
                    jsimKey = Keys.J;
                    return true;
                case Key.K:
                    jsimKey = Keys.K;
                    return true;
                case Key.L:
                    jsimKey = Keys.L;
                    return true;
                case Key.M:
                    jsimKey = Keys.M;
                    return true;
                case Key.N:
                    jsimKey = Keys.N;
                    return true;
                case Key.O:
                    jsimKey = Keys.O;
                    return true;
                case Key.P:
                    jsimKey = Keys.P;
                    return true;
                case Key.Q:
                    jsimKey = Keys.Q;
                    return true;
                case Key.R:
                    jsimKey = Keys.R;
                    return true;
                case Key.S:
                    jsimKey = Keys.S;
                    return true;
                case Key.T:
                    jsimKey = Keys.T;
                    return true;
                case Key.U:
                    jsimKey = Keys.U;
                    return true;
                case Key.V:
                    jsimKey = Keys.V;
                    return true;
                case Key.W:
                    jsimKey = Keys.W;
                    return true;
                case Key.X:
                    jsimKey = Keys.X;
                    return true;
                case Key.Y:
                    jsimKey = Keys.Y;
                    return true;
                case Key.Z:
                    jsimKey = Keys.Z;
                    return true;

                case Key.NumPad0:
                    jsimKey = Keys.NumPad0;
                    return true;
                case Key.NumPad1:
                    jsimKey = Keys.NumPad1;
                    return true;
                case Key.NumPad2:
                    jsimKey = Keys.NumPad2;
                    return true;
                case Key.NumPad3:
                    jsimKey = Keys.NumPad3;
                    return true;
                case Key.NumPad4:
                    jsimKey = Keys.NumPad4;
                    return true;
                case Key.NumPad5:
                    jsimKey = Keys.NumPad5;
                    return true;
                case Key.NumPad6:
                    jsimKey = Keys.NumPad6;
                    return true;
                case Key.NumPad7:
                    jsimKey = Keys.NumPad7;
                    return true;
                case Key.NumPad8:
                    jsimKey = Keys.NumPad8;
                    return true;
                case Key.NumPad9:
                    jsimKey = Keys.NumPad9;
                    return true;

                case Key.Multiply:
                    jsimKey = Keys.Multiply;
                    return true;
                case Key.Add:
                    jsimKey = Keys.Add;
                    return true;
                case Key.Separator:
                    jsimKey = Keys.Separator;
                    return true;
                case Key.Subtract:
                    jsimKey = Keys.Subtract;
                    return true;
                case Key.Decimal:
                    jsimKey = Keys.Decimal;
                    return true;
                case Key.Divide:
                    jsimKey = Keys.Divide;
                    return true;

                default:
                    jsimKey = Keys.None;
                    return false;
            }
        }
    }
}
