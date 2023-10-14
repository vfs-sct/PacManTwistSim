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
        // ghost state, base on state to decide which type of movement should they do
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
        int defaultPositionX;
        int defaultPositionY;
        bool weakness;
        // set default path cooridate for ghost [X][Y]
        int[][] pathCoorArr;

        bool reachedPoint1;
        bool reachedPoint2;

        State state;
        List<Direction> chasePath;

        public Ghost(int x, int y, int[][] coorArr)
        {
            // Destination reach flag
            reachedPoint1 = false;
            reachedPoint2 = false;
            // default state move in the path
            state = State.movingInPath;
            //start point
            defaultPositionX = x;
            defaultPositionY = y;
            //current point
            X = x;
            Y = y;
            // array that record the coordinate we set for each ghost
            PathCoorArr = coorArr; 
            // record chase path
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
        //ghost Movement AI
        public void Movement(Map levelMap)
        {
            //if not reach point1 and2 yet, move to point one
            if (!reachedPoint1 && !reachedPoint2)
            {
                // move to point 1
                // pathCoorArr[FirstPoint][X] pathCoorArr[FirstPoint][Y]
                GoTo(pathCoorArr[0][0], pathCoorArr[0][1], levelMap);
                if(X== pathCoorArr[0][0] && Y== pathCoorArr[0][1])
                    reachedPoint1 = true;
            }
            // if reach point 1 but not 2,move to point 2
            else if (reachedPoint1 && !reachedPoint2)
            {
                // move to point 2
                // pathCoorArr[SecondPoint][X] pathCoorArr[FirstPoint][Y]
                GoTo(pathCoorArr[1][0], pathCoorArr[1][1], levelMap);
                if (X == pathCoorArr[1][0] && Y == pathCoorArr[1][1])
                    reachedPoint2 = true;
            }
            // if reach point 1 and 2,move back to point 1
            else if (reachedPoint1 && reachedPoint2)
            {
                GoTo(pathCoorArr[0][0], pathCoorArr[0][1], levelMap);
                if (X == pathCoorArr[0][0] && Y == pathCoorArr[0][1])
                    reachedPoint1 = false;
            }
            // Move back to Start point
            else if (!reachedPoint1 && reachedPoint2)
            {
                GoTo(defaultPositionX, defaultPositionY, levelMap);
                if (X == defaultPositionX && Y == defaultPositionY)
                    reachedPoint2 = false;
            }
        }

        // go to certain position
        public void GoTo(int targetX, int targetY, Map levelMap)
        {
            // if target is at right and up
            if (targetX > X && targetY > Y)
            {
                if (TryDown(levelMap)){
                    return; 
                }else if(TryRight(levelMap)) {
                    return; 
                }else if (TryUp(levelMap)){ 
                    return;
                }else if (TryLeft(levelMap)){
                    return;
                }else { 
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return;
                }
            }
            // if target is at left and up
            else if (targetX < X && targetY > Y)
            {
                if (TryLeft(levelMap)){
                    return;
                } else if (TryDown(levelMap)){ 
                    return; 
                }else if (TryRight(levelMap)){
                    return; 
                }else if (TryUp(levelMap)){ 
                    return; 
                }else { 
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return; 
                }
            }
            // if target is at right and down
            else if (targetX > X && targetY < Y)
            {
                if (TryUp(levelMap))
                { return; }
                else if (TryRight(levelMap))
                { return; }
                else if (TryDown(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else { Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; }
            }
            // if target is at left and down
            else if (targetX < X && targetY < Y)
            {
                if (TryRight(levelMap)){
                    return; 
                }else if (TryUp(levelMap)){ 
                    return;
                }else if (TryLeft(levelMap)){
                    return; 
                }else if (TryDown(levelMap)){ 
                    return; 
                }else { 
                    Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; 
                }
            }
            // if target is at down
            else if (targetX==X && targetY <= Y)
            {
                if (TryUp(levelMap)) { 
                    return; 
                }
                else if (TryRight(levelMap)){ 
                    return; 
                }
                else if (TryLeft(levelMap)){
                    return; 
                }
                else if (TryDown(levelMap)) {
                    return; 
                }else { 
                    Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; 
                }
            }
            // if target is at up
            else if (targetX == X && targetY >= Y)
            {
                if (TryDown(levelMap)){ 
                    return; 
                }else if (TryRight(levelMap)) { 
                    return; 
                }else if (TryLeft(levelMap)){ 
                    return;
                }else if (TryUp(levelMap)) {
                    return; 
                }else { 
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return;
                }
            }
            // if target is at left
            else if (targetX<=X && targetY == Y)
            {
                if (TryLeft(levelMap)){ 
                    return; 
                }
                else if (TryUp(levelMap)){ 
                    return; 
                }else if (TryDown(levelMap)){ 
                    return; 
                }else if (TryRight(levelMap)){ 
                    return; 
                }else { 
                    Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; 
                }
            }
            // if target is at right
            else if (targetX >= X && targetY == Y)
            {
                if (TryRight(levelMap)){
                    return; 
                } else if (TryUp(levelMap)){ 
                    return; 
                }else if (TryDown(levelMap)){ 
                    return; 
                }else if (TryLeft(levelMap)){ 
                    return; 
                } else {
                    Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return;
                }
            }
        }

        // Runaway from pacman
        public void RunAway(int targetX, int targetY, Map levelMap)
        {
            // if pacman is at left and down
            if (targetX < X && targetY < Y)
            {
                if (TryDown(levelMap))
                { return; }
                else if (TryRight(levelMap))
                { return; }
                else if (TryUp(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else
                {
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return;
                }
            }
            // if pacman is at right and down
            else if (targetX > X && targetY < Y)
            {
                if (TryLeft(levelMap))
                { return; }
                else if (TryDown(levelMap))
                { return; }
                else if (TryRight(levelMap))
                { return; }
                else if (TryUp(levelMap))
                { return; }
                else
                {
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return;
                }
            }
            // if pacman is at left and up
            else if (targetX < X && targetY > Y)
            {
                if (TryUp(levelMap))
                { return; }
                else if (TryRight(levelMap))
                { return; }
                else if (TryDown(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else
                {
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return;
                }
            }
            // if pacman is at right and up
            else if (targetX > X && targetY > Y)
            {
                if (TryRight(levelMap))
                { return; }
                else if (TryUp(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else if (TryDown(levelMap))
                { return; }
                else
                {
                    Console.WriteLine("Error ghost trapped");
                    InvertLastDirectionInList();
                    return;
                }
            }
            // if pacman is at up
            else if (targetX == X && targetY >= Y)
            {
                if (TryUp(levelMap)) { return; }
                else if (TryRight(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else if (TryDown(levelMap)) { return; }
                else { Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; }
            }
            // if pacman is at down
            else if (targetX == X && targetY <= Y)
            {
                if (TryDown(levelMap))
                { return; }
                else if (TryRight(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else if (TryUp(levelMap)) { return; }
                else { Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; }
            }
            // if pacman is at right
            else if (targetX >= X && targetY == Y)
            {
                if (TryLeft(levelMap))
                { return; }
                else if (TryUp(levelMap))
                { return; }
                else if (TryDown(levelMap))
                { return; }
                else if (TryRight(levelMap))
                { return; }
                else { Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; }
            }
            // if pacman is at left
            else if (targetX <= X && targetY == Y)
            {
                if (TryRight(levelMap))
                { return; }
                else if (TryUp(levelMap))
                { return; }
                else if (TryDown(levelMap))
                { return; }
                else if (TryLeft(levelMap))
                { return; }
                else { Console.WriteLine("Error ghost trapped"); 
                    InvertLastDirectionInList();
                    return; }
            }
        }

        // check the map if can go down
        bool TryDown(Map levelMap)
        {
            if (levelMap.map[Y + 1, X] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Up))
            {
                if (levelMap.map[Y + 1, X] == '·')
                {
                    levelMap.map[Y, X] = '·';
                    chasePath.Add(Direction.Down);
                    Y++;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    chasePath.Add(Direction.Down);
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

        // check the map if can go up
        bool TryUp(Map levelMap)
        {
            if (levelMap.map[Y - 1, X] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Down))
            {
                if (levelMap.map[Y - 1, X] == '·')
                {
                    levelMap.map[Y, X] = '·';
                    chasePath.Add(Direction.Up);
                    Y--;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    chasePath.Add(Direction.Up);
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

        // check the map if can go right
        bool TryRight(Map levelMap)
        {
            if (levelMap.map[Y, X + 1] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Left))
            {
                if (levelMap.map[Y, X + 1] == '·')
                {
                    levelMap.map[Y, X] = '·';
                    chasePath.Add(Direction.Right);
                    X++;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    chasePath.Add(Direction.Right);
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

        // check the map if can go left
        bool TryLeft(Map levelMap)
        {
            if (levelMap.map[Y, X - 1] != '#' && (chasePath.Count == 0 || chasePath.Last() != Direction.Right))
            {
                if (levelMap.map[Y, X - 1] == '·')
                {
                    levelMap.map[Y, X] = '·';
                    chasePath.Add(Direction.Left);
                    X--;
                    Spawn(levelMap);
                    return true;
                }
                else
                {
                    levelMap.map[Y, X] = ' ';
                    chasePath.Add(Direction.Left);
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

        //Inverst the pathList
        void InvertLastDirectionInList()
        {
            int lenght=chasePath.Count;
                if (chasePath.Last() == Direction.Left)
                    chasePath[lenght-1] = Direction.Right;
                else if (chasePath.Last() == Direction.Right)
                    chasePath[lenght-1] = Direction.Left;
                else if (chasePath.Last() == Direction.Up)
                    chasePath[lenght-1] = Direction.Down;
                else
                    chasePath[lenght-1] = Direction.Up;
        }
    }

}
