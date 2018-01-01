using System;
using System.Windows.Input;

namespace render
{
    public static class Input
    {
        private static class RotationHorizontalAxe
        {
            public static double left = 0, right = 0;

            public static double GetValue()
            {
                return right - left;
            }
        }
        private static class RotationVerticalAxe
        {
            public static double up = 0, down = 0;

            public static double GetValue()
            {
                return up - down;
            }
        }

        private static class MovementHorizontalAxe
        {
            public static double left = 0, right = 0;

            public static double GetValue()
            {
                return right - left;
            }
        }
        private static class MovementForwardAxe
        {
            public static double forward = 0, backward = 0;

            public static double GetValue()
            {
                return forward - backward;
            }
        }

        public static class Update
        {
            static Func<Key, bool> isDown = Keyboard.IsKeyDown;
            static Func<Key, bool> isUp = Keyboard.IsKeyUp;

            public static void KeyDown(object sender, KeyEventArgs e)
            {
                if (isDown(Key.Left))RotationHorizontalAxe.left = 1;
                if (isDown(Key.Right))RotationHorizontalAxe.right = 1;

                if (isDown(Key.Up))RotationVerticalAxe.up = 1;
                if (isDown(Key.Down))RotationVerticalAxe.down = 1;

                if (isDown(Key.W)) MovementForwardAxe.forward = 1;
                if (isDown(Key.S)) MovementForwardAxe.backward = 1;

                if (isDown(Key.A)) MovementHorizontalAxe.left = 1;
                if (isDown(Key.D)) MovementHorizontalAxe.right = 1;

            }

            public static void KeyUp(object sender, KeyEventArgs e)
            {
                if (isUp(Key.Left))RotationHorizontalAxe.left = 0;
                if (isUp(Key.Right))RotationHorizontalAxe.right = 0;

                if (isUp(Key.Up))RotationVerticalAxe.up = 0;
                if (isUp(Key.Down))RotationVerticalAxe.down = 0;

                if (isUp(Key.W)) MovementForwardAxe.forward = 0;
                if (isUp(Key.S)) MovementForwardAxe.backward = 0;

                if (isUp(Key.A)) MovementHorizontalAxe.left = 0;
                if (isUp(Key.D)) MovementHorizontalAxe.right = 0;
            }
        }

        public enum Axe { HorizontalRot, VerticalRot, ForwardMov, SideMov }

        public static double GetAxe(Axe axe)
        {
            switch (axe)
            {
                case Axe.HorizontalRot:
                    {
                        return RotationHorizontalAxe.GetValue();
                    }
                case Axe.VerticalRot:
                    {
                        return RotationVerticalAxe.GetValue();
                    }
                case Axe.ForwardMov:
                    {
                        return MovementForwardAxe.GetValue();
                    }
                case Axe.SideMov:
                    {
                        return MovementHorizontalAxe.GetValue();
                    }
                default:
                    {
                        throw new Exception("axe is undefined");
                    }
            }
        }
    }
}
