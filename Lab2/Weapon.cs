﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab2
{
    abstract class Weapon : Mover
    {
        private const int MOVE_INTERVAL = 10;
        public bool PickedUp { get; private set; }
        public Weapon(Game game, Point location) : base(game, location)
        {
            PickedUp = false;
        }
        public void PickUpWeapon() { PickedUp = true; }
        public abstract string Name { get; }
        public abstract void Attack(Direction direction, Random random);
        protected bool DamageEnemy(Direction direction, int radius,
        int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < radius; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    if (Nearby(enemy.Location, target, distance))
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }
                }
                target = Move(direction, target, game.Boundaries);
            }
            return false;
        }

        public bool Nearby(Point location, Point target, int distance)
        {
            if (Math.Abs(location.X - target.X) < distance &&
            (Math.Abs(location.Y - target.Y) < distance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Point Move(Direction direction, Point target, Rectangle boundaries)
        {
            Point newLocation = target;
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y - MOVE_INTERVAL >= boundaries.Top)
                        newLocation.Y -= MOVE_INTERVAL;
                    break;
                case Direction.Down:
                    if (newLocation.Y + MOVE_INTERVAL <= boundaries.Bottom)
                        newLocation.Y += MOVE_INTERVAL;
                    break;
                case Direction.Left:
                    if (newLocation.X - MOVE_INTERVAL >= boundaries.Left)
                        newLocation.X -= MOVE_INTERVAL;
                    break;
                case Direction.Right:
                    if (newLocation.X + MOVE_INTERVAL <= boundaries.Right)
                        newLocation.X += MOVE_INTERVAL;
                    break;
                default: break;
            }
            return newLocation;
        }

        public Direction CounterClockWiseDirection(Direction direction)
        {
            Direction counterClockWiseDirection;

            switch (direction)
            {
                case Direction.Up:
                    counterClockWiseDirection = Direction.Left;
                    break;
                case Direction.Down:
                    counterClockWiseDirection = Direction.Right;
                    break;
                case Direction.Left:
                    counterClockWiseDirection = Direction.Down;
                    break;
                case Direction.Right:
                    counterClockWiseDirection = Direction.Up;
                    break;
                default:
                    counterClockWiseDirection = direction;
                    break;
            }
            return counterClockWiseDirection;
        }

        public Direction ClockWiseDirection(Direction direction)
        {
            Direction clockWiseDirection;

            switch (direction)
            {
                case Direction.Up:
                    clockWiseDirection = Direction.Right;
                    break;
                case Direction.Down:
                    clockWiseDirection = Direction.Left;
                    break;
                case Direction.Left:
                    clockWiseDirection = Direction.Up;
                    break;
                case Direction.Right:
                    clockWiseDirection = Direction.Down;
                    break;
                default:
                    clockWiseDirection = direction;
                    break;
            }
            return clockWiseDirection;
        }
    }
}
