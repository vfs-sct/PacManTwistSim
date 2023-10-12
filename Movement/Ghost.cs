using System;
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

        public enum State
        {
            chasing,
            runningAway,
            movingInPath
        }
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left,
            Stop
        }
        //List<Direction> pathList = new List<Direction>();
        int defaultPositionX;
        int defaultPositionY;
        bool weakness;
        int[][] pathCoorArr;
        bool reachedPoint1;
        bool reachedPoint2;

        State state;
        List<Direction> chasePath;

        public Ghost(int x, int y, int[][] coorArr)
        {
            reachedPoint1=false;
            reachedPoint2 = false;
            state = State.movingInPath;
            defaultPositionX = x;
            defaultPositionY = y;
            X = x;
            Y = y;
            PathCoorArr = coorArr;
            chasePath = new List<Direction>();
            Type = "ghost";
            Symbol = '†';
            weakness = false;
        }
        public State GhostState
        {
            get { return state; }
            set { state = value; }
        }

        public int DefaultPositionX
        {
            get { return defaultPositionX; }
        }

        public int DefaultPositionY
        {
            get { return defaultPositionY; }
        }

        public int[][] PathCoorArr
        {
            get { return pathCoorArr; }
            set { pathCoorArr = value; }
        }

        public bool Weakness
        {
            get { return weakness; }
            set { weakness = value; }
        }

        public void Movement(Map levelMap)
        {
            if (!reachedPoint1 && !reachedPoint2)
            {
               GoTo(pathCoorArr[0][0], pathCoorArr[0][1], levelMap);
                if(X== pathCoorArr[0][0] && Y== pathCoorArr[0][1])
                    reachedPoint1 = true;
            }
            else if (reachedPoint1 && !reachedPoint2)
            {
                GoTo(pathCoorArr[1][0], pathCoorArr[1][1], levelMap);
                if (X == pathCoorArr[1][0] && Y == pathCoorArr[1][1])
                    reachedPoint2 = true;
            }
            else if(reachedPoint1 && reachedPoint2)
            {
                GoTo(pathCoorArr[0][0], pathCoorArr[0][1], levelMap);
                if (X == pathCoorArr[0][0] && Y == pathCoorArr[0][1])
                    reachedPoint1 = false;
            }
            else if(!reachedPoint1 && reachedPoint2)
            {
                GoTo(defaultPositionX, defaultPositionY, levelMap);
                if (X == pathCoorArr[1][0] && Y == pathCoorArr[1][1])
                    reachedPoint2 = false;
            }
        }

        public void GoTo(int targetX, int targetY, Map levelMap)
        {
            if (targetX >= X && targetY > Y)
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
            else if (targetX < X && targetY > Y)
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
            else if (targetX >= X && targetY <= Y)
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
            else if (targetX < X && targetY <= Y)
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




        }

        public void RunAway(int targetX, int targetY, Map levelMap)
        {
            if (targetX >= X && targetY > Y)
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
            else if (targetX < X && targetY > Y)
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
            else if (targetX >= X && targetY <= Y)
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
            else if (targetX < X && targetY <= Y)
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
                    levelMap.map[Y, X] = ' ';
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
                    levelMap.map[Y, X] = ' ';
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
                    levelMap.map[Y, X] = ' ';
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
                    levelMap.map[Y, X] = ' ';
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

    }
}
