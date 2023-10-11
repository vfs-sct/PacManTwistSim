using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Timers;

namespace Movement
{
    internal class Ghost:Character
    {
        //List<Direction> pathList = new List<Direction>();
        int pathListIndex = 0;
        bool weakness;

        public Ghost(int x,int y, int[][] coorArr)
        {
            X = x;
            Y = y;
            PathCoorArr = coorArr;
            PathList = new List<Direction>();
            Type = "ghost";
            Symbol = '†';
            weakness = false;
        }

        public void SetRoad(Map levelMap)
        {
            int[] currentPos = { X,Y };
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

        public void Movement(Map levelMap)
        {
            if(pathListIndex == PathList.Count)
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
            if (PathList[pathListIndex] == Direction.Left)
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
            else if (PathList[pathListIndex] == Direction.Right)
            {
                if (levelMap.map[Y, X + 1] != '#' && levelMap.map[Y, X + 1] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    X++;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    X--;
                }
                Spawn(levelMap);
            }
            else if (PathList[pathListIndex] == Direction.Up)
            {
                if (levelMap.map[Y - 1, X] != '#' && levelMap.map[Y - 1, X] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    Y--;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    X--;
                }
                Spawn(levelMap);
            }
            else if (PathList[pathListIndex] == Direction.Down)
            {
                if (levelMap.map[Y + 1, X] != '#' && levelMap.map[Y + 1, X] == '-')
                {
                    levelMap.map[Y, X] = '-';
                    Y++;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    X--;
                }
                Spawn(levelMap);
            }
            else
            {
                Console.WriteLine("Err");
            }

            if(pathListIndex != PathList.Count)
            {
                pathListIndex++;
            }

          
            
        }

        public bool Weakness
        {
            get { return weakness; }
            set { weakness = value; }
        }

    }
}
