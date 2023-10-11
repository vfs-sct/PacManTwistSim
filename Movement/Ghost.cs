﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Timers;
using static Movement.Ghost;

namespace Movement
{

    internal class Ghost : Character
    {
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left,
            Stop
        }
        //List<Direction> pathList = new List<Direction>();
        int pathListIndex = 0;
        bool weakness;
        int[][] pathCoorArr;
        int i = 0;
        List<Direction> pathList;
        List<Direction> chasePath;

        public Ghost(int x, int y, int[][] coorArr)
        {
            X = x;
            Y = y;
            PathCoorArr = coorArr;
            PathList = new List<Direction>();
            chasePath = new List<Direction>();
            Type = "ghost";
            Symbol = '†';
            weakness = false;
        }

        public int[][] PathCoorArr
        {
            get { return pathCoorArr; }
            set { pathCoorArr = value; }
        }

        public List<Direction> PathList
        {
            get { return pathList; }
            set { pathList = value; }
        }

        public void SetRoad(Map levelMap)
        {
            int[] currentPos = { X, Y };
            Direction direction = Direction.Stop;

            //Console.WriteLine("StartPoint {0} EndPoint {1}", currentPos[0], coorArr[0][0]);
            //Console.WriteLine("StartPoint {0} EndPoint {1}", currentPos[1], coorArr[0][1]);

            foreach (var item in PathCoorArr)
            {
                while (currentPos[0] != item[0])
                {
                    if (item[0] - currentPos[0] > 0)
                    {
                        direction = Direction.Right;
                        currentPos[0] += 1;
                    }
                    else if (item[0] - currentPos[0] < 0)
                    {
                        direction = Direction.Left;
                        currentPos[0] -= 1;
                    }
                    PathList.Add(direction);
                }
                while (currentPos[1] != item[1])
                {
                    if (item[1] - currentPos[1] > 0)
                    {
                        direction = Direction.Down;
                        currentPos[1] += 1;
                    }
                    else if (item[1] - currentPos[1] < 0)
                    {
                        direction = Direction.Up;
                        currentPos[1] -= 1;
                    }
                    PathList.Add(direction);
                }
            }

        }

        public void Movement(Map levelMap, List<Direction> pathToMove)
        {
            if (Y == PathCoorArr[1][1] && X == PathCoorArr[1][0])
            {
                ReversePath();

            }
            if (i < pathToMove.Count)
            {
                if (pathToMove[i] == Direction.Left)
                {
                    if (levelMap.map[Y, X - 1] != '#' && levelMap.map[Y, X - 1] == '-')
                    {
                        levelMap.map[Y, X] = '-';
                        X--;
                    }
                    else
                    {
                        levelMap.map[Y, X] = ' ';
                        X--;
                    }
                    Spawn(levelMap);
                }
                else if (pathToMove[i] == Direction.Right)
                {
                    if (levelMap.map[Y, X + 1] != '#' && levelMap.map[Y, X + 1] == '-')
                    {
                        levelMap.map[Y, X] = '-';
                        X++;
                    }
                    else
                    {
                        levelMap.map[Y, X] = ' ';
                        X++;
                    }
                    Spawn(levelMap);
                }
                else if (pathToMove[i] == Direction.Up)
                {
                    if (levelMap.map[Y - 1, X] != '#' && levelMap.map[Y - 1, X] == '-')
                    {
                        levelMap.map[Y, X] = '-';
                        Y--;
                    }
                    else
                    {
                        levelMap.map[Y, X] = ' ';
                        Y--;
                    }
                    Spawn(levelMap);
                }
                else if (pathToMove[i] == Direction.Down)
                {
                    if (levelMap.map[Y + 1, X] != '#' && levelMap.map[Y + 1, X] == '-')
                    {
                        levelMap.map[Y, X] = '-';
                        Y++;
                    }
                    else
                    {
                        levelMap.map[Y, X] = ' ';
                        Y++;
                    }
                    Spawn(levelMap);
                }
                else
                {
                    Console.WriteLine("Err");
                }
                i++;
            }
        }

        public void ReversePath()
        {
            if (pathListIndex == PathList.Count)
            {
                PathList.Reverse();
                List<Direction> pathListReverse = new List<Direction>();
                int index = 0;
                foreach (var item in PathList)
                {
                    if (item == Direction.Left)
                    {
                        pathListReverse.Add(Direction.Right);
                    }
                    else if (item == Direction.Up)
                    {
                        pathListReverse.Add(Direction.Down);
                    }
                    else if (item == Direction.Down)
                    {
                        pathListReverse.Add(Direction.Up);
                    }
                    else if (item == Direction.Right)
                    {
                        pathListReverse.Add(Direction.Left);
                    }
                    index++;
                }
                PathList = pathListReverse;
                pathListIndex = 0;
            }
        }

        public bool Weakness
        {
            get { return weakness; }
            set { weakness = value; }
        }

        public void Chase(PacMan pacMan, Map levelMap)
        {
            if (pacMan.X >= X && pacMan.Y > Y)
            {
                if (!TryDown(levelMap))
                {
                    if (!TryRight(levelMap))
                    {
                        if (!TryUp(levelMap))
                        {
                            if (!TryLeft(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else if (pacMan.X < X && pacMan.Y > Y)
            {
                if (!TryDown(levelMap))
                {
                    if (!TryLeft(levelMap))
                    {
                        if (!TryUp(levelMap))
                        {
                            if (!TryRight(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else if (pacMan.X >= X && pacMan.Y <= Y)
            {
                if (!TryUp(levelMap))
                {
                    if (!TryRight(levelMap))
                    {
                        if (!TryDown(levelMap))
                        {
                            if (!TryLeft(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else if (pacMan.X < X && pacMan.Y <= Y)
            {
                if (!TryUp(levelMap))
                {
                    if (!TryLeft(levelMap))
                    {
                        if (!TryDown(levelMap))
                        {
                            if (!TryRight(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.Write("We have a problem");
            }



        }

        public void RunAway(PacMan pacMan, Map levelMap)
        {
            if (pacMan.X >= X && pacMan.Y > Y)
            {
                if (!TryUp(levelMap))
                {
                    if (!TryLeft(levelMap))
                    {
                        if (!TryDown(levelMap))
                        {
                            if (!TryRight(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else if (pacMan.X < X && pacMan.Y > Y)
            {
                if (!TryUp(levelMap))
                {
                    if (!TryRight(levelMap))
                    {
                        if (!TryDown(levelMap))
                        {
                            if (!TryLeft(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else if (pacMan.X >= X && pacMan.Y <= Y)
            {
                if (!TryDown(levelMap))
                {
                    if (!TryLeft(levelMap))
                    {
                        if (!TryUp(levelMap))
                        {
                            if (!TryRight(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else if (pacMan.X < X && pacMan.Y <= Y)
            {
                if (!TryDown(levelMap))
                {
                    if (!TryRight(levelMap))
                    {
                        if (!TryUp(levelMap))
                        {
                            if (!TryLeft(levelMap))
                            {
                                Console.WriteLine("Error ghost trapped");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.Write("We have a problem");
            }



        }

        bool TryDown(Map levelMap)
        {
            if (levelMap.map[Y + 1, X] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Down))
            {
                if (levelMap.map[Y + 1, X] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    chasePath.Add(Direction.Up);
                    Y++;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    chasePath.Add(Direction.Up);
                    Y++;
                    Spawn(levelMap);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        bool TryUp(Map levelMap)
        {
            if (levelMap.map[Y - 1, X] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Up))
            {
                if (levelMap.map[Y - 1, X] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    chasePath.Add(Direction.Down);
                    Y--;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    chasePath.Add(Direction.Down);
                    Y--;
                    Spawn(levelMap);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        bool TryRight(Map levelMap)
        {
            if (levelMap.map[Y, X + 1] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Right))
            {
                if (levelMap.map[Y, X + 1] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    chasePath.Add(Direction.Left);
                    X++;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    chasePath.Add(Direction.Left);
                    X++;
                    Spawn(levelMap);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        bool TryLeft(Map levelMap)
        {
            if (levelMap.map[Y, X - 1] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Left))
            {
                if (levelMap.map[Y, X - 1] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    chasePath.Add(Direction.Right);
                    X--;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    chasePath.Add(Direction.Right);
                    X--;
                    Spawn(levelMap);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public void ReverseChase()
        {
            chasePath.Reverse();
            List<Direction> chasePathReverse = new List<Direction>();
            foreach (var item in chasePath)
            {
                if (item == Direction.Left)
                {
                    chasePathReverse.Add(Direction.Right);
                }
                else if (item == Direction.Up)
                {
                    chasePathReverse.Add(Direction.Down);
                }
                else if (item == Direction.Down)
                {
                    chasePathReverse.Add(Direction.Up);
                }
                else if (item == Direction.Right)
                {
                    chasePathReverse.Add(Direction.Left);
                }
            }
        }
    }
}
