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

        public static class Update
        {
            public static void KeyDown(object sender, KeyEventArgs e)
            {
                if (Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
                {
                    HorizontalAxe.left = 1;
                }
                if (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right))
                {
                    HorizontalAxe.right = 1;
                }
            }

            public static void KeyUp(object sender, KeyEventArgs e)
            {
                if (Keyboard.IsKeyUp(Key.A) || Keyboard.IsKeyUp(Key.Left))
                {
                    HorizontalAxe.left = 0;
                }
                if (Keyboard.IsKeyUp(Key.D) || Keyboard.IsKeyUp(Key.Right))
                {
                    HorizontalAxe.right = 0;
                }
            }
        }

        public enum Axe { Horizontal }

        public static double GetAxe(Axe axe)
        {
            switch (axe)
            {
                case Axe.Horizontal:
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
