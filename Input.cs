using System;
using System.Windows.Input;

namespace render
{
    public static class Input
    {
        private static class HorizontalAxe
        {
            public static double left = 0, right = 0;

            public static double GetValue()
            {
                return right - left;
            }
        }
        private static class VerticalAxe
        {
            public static double up = 0, down = 0;

            public static double GetValue()
            {
                return up - down;
            }
        }

        public static class Update
        {
            static Func<Key, bool> isDown = Keyboard.IsKeyDown;
            static Func<Key, bool> isUp = Keyboard.IsKeyUp;

            public static void KeyDown(object sender, KeyEventArgs e)
            {
                if (isDown(Key.A) || isDown(Key.Left))HorizontalAxe.left = 1;
                if (isDown(Key.D) || isDown(Key.Right))HorizontalAxe.right = 1;

                if (isDown(Key.W) || isDown(Key.Up))VerticalAxe.up = 1;
                if (isDown(Key.S) || isDown(Key.Down))VerticalAxe.down = 1;
            }

            public static void KeyUp(object sender, KeyEventArgs e)
            {
                if (isUp(Key.A) && isUp(Key.Left))HorizontalAxe.left = 0;
                if (isUp(Key.D) && isUp(Key.Right))HorizontalAxe.right = 0;

                if (isUp(Key.W) && isUp(Key.Up))VerticalAxe.up = 0;
                if (isUp(Key.S) && isUp(Key.Down))VerticalAxe.down = 0;
            }
        }

        public enum Axe { Horizontal, Vertical }

        public static double GetAxe(Axe axe)
        {
            switch (axe)
            {
                case Axe.Horizontal:
                    {
                        return HorizontalAxe.GetValue();
                    }
                case Axe.Vertical:
                    {
                        return HorizontalAxe.GetValue();
                    }
                default:
                    {
                        throw new Exception("axe is undefined");
                    }
            }
        }
    }
}
