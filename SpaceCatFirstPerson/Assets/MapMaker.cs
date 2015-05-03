using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class MapMaker : MonoBehaviour {


    public enum TileTypes
    {
        Empty = 0,
        Blocked = 1
    }

    /// <summary>
    /// This class demonstrates a simple map builder for a roguelike game. For a detailed
    /// look at using it, go here http://www.evilscience.co.uk/?p=553
    /// </summary>
    private class csMapbuilder
    {

        public struct Point
        {
            // Private x and y coordinate fields.
            private int x, y;

            // -----------------------
            // Public Shared Members
            // -----------------------

            /// <summary>
            ///	Empty Shared Field
            /// </summary>
            ///
            /// <remarks>
            ///	An uninitialized Point Structure.
            /// </remarks>

            public static readonly Point Empty;



            /// <summary>
            ///	Addition Operator
            /// </summary>
            ///
            /// <remarks>
            ///	Translates a Point using the Width and Height
            ///	properties of the given <typeref>Size</typeref>.
            /// </remarks>

            public static Point operator +(Point pt, Size sz)
            {
                return new Point(pt.X + sz.Width, pt.Y + sz.Height);
            }

            /// <summary>
            ///	Equality Operator
            /// </summary>
            ///
            /// <remarks>
            ///	Compares two Point objects. The return value is
            ///	based on the equivalence of the X and Y properties 
            ///	of the two points.
            /// </remarks>

            public static bool operator ==(Point left, Point right)
            {
                return ((left.X == right.X) && (left.Y == right.Y));
            }

            /// <summary>
            ///	Inequality Operator
            /// </summary>
            ///
            /// <remarks>
            ///	Compares two Point objects. The return value is
            ///	based on the equivalence of the X and Y properties 
            ///	of the two points.
            /// </remarks>

            public static bool operator !=(Point left, Point right)
            {
                return ((left.X != right.X) || (left.Y != right.Y));
            }

            /// <summary>
            ///	Subtraction Operator
            /// </summary>
            ///
            /// <remarks>
            ///	Translates a Point using the negation of the Width 
            ///	and Height properties of the given Size.
            /// </remarks>

            public static Point operator -(Point pt, Size sz)
            {
                return new Point(pt.X - sz.Width, pt.Y - sz.Height);
            }

            /// <summary>
            ///	Point to Size Conversion
            /// </summary>
            ///
            /// <remarks>
            ///	Returns a Size based on the Coordinates of a given 
            ///	Point. Requires explicit cast.
            /// </remarks>

            public static explicit operator Size(Point p)
            {
                return new Size(p.X, p.Y);
            }



            // -----------------------
            // Public Constructors
            // -----------------------

            /// <summary>
            ///	Point Constructor
            /// </summary>
            ///
            /// <remarks>
            ///	Creates a Point from an integer which holds the Y
            ///	coordinate in the high order 16 bits and the X
            ///	coordinate in the low order 16 bits.
            /// </remarks>

            public Point(int dw)
            {
                y = dw >> 16;
                x = dw & 0xffff;
            }

            /// <summary>
            ///	Point Constructor
            /// </summary>
            ///
            /// <remarks>
            ///	Creates a Point from a Size value.
            /// </remarks>

            public Point(Size sz)
            {
                x = sz.Width;
                y = sz.Height;
            }

            /// <summary>
            ///	Point Constructor
            /// </summary>
            ///
            /// <remarks>
            ///	Creates a Point from a specified x,y coordinate pair.
            /// </remarks>

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            // -----------------------
            // Public Instance Members
            // -----------------------

            /// <summary>
            ///	IsEmpty Property
            /// </summary>
            ///
            /// <remarks>
            ///	Indicates if both X and Y are zero.
            /// </remarks>

            [Browsable(false)]
            public bool IsEmpty
            {
                get
                {
                    return ((x == 0) && (y == 0));
                }
            }

            /// <summary>
            ///	X Property
            /// </summary>
            ///
            /// <remarks>
            ///	The X coordinate of the Point.
            /// </remarks>

            public int X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }

            /// <summary>
            ///	Y Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Y coordinate of the Point.
            /// </remarks>

            public int Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }

            /// <summary>
            ///	Equals Method
            /// </summary>
            ///
            /// <remarks>
            ///	Checks equivalence of this Point and another object.
            /// </remarks>

            public override bool Equals(object obj)
            {
                if (!(obj is Point))
                    return false;

                return (this == (Point)obj);
            }

            /// <summary>
            ///	GetHashCode Method
            /// </summary>
            ///
            /// <remarks>
            ///	Calculates a hashing value.
            /// </remarks>

            public override int GetHashCode()
            {
                return x ^ y;
            }

            /// <summary>
            ///	Offset Method
            /// </summary>
            ///
            /// <remarks>
            ///	Moves the Point a specified distance.
            /// </remarks>

            public void Offset(int dx, int dy)
            {
                x += dx;
                y += dy;
            }

            public static Point Add(Point pt, Size sz)
            {
                return new Point(pt.X + sz.Width, pt.Y + sz.Height);
            }

            public void Offset(Point p)
            {
                Offset(p.X, p.Y);
            }

            public static Point Subtract(Point pt, Size sz)
            {
                return new Point(pt.X - sz.Width, pt.Y - sz.Height);
            }

        }
        public struct Rectangle
        {
            private int x, y, width, height;

            /// <summary>
            ///	Empty Shared Field
            /// </summary>
            ///
            /// <remarks>
            ///	An uninitialized Rectangle Structure.
            /// </remarks>

            public static readonly Rectangle Empty;

            /// <summary>
            ///	FromLTRB Shared Method
            /// </summary>
            ///
            /// <remarks>
            ///	Produces a Rectangle structure from left, top, right,
            ///	and bottom coordinates.
            /// </remarks>

            public static Rectangle FromLTRB(int left, int top,
                              int right, int bottom)
            {
                return new Rectangle(left, top, right - left,
                              bottom - top);
            }

            /// <summary>
            ///	Inflate Shared Method
            /// </summary>
            ///
            /// <remarks>
            ///	Produces a new Rectangle by inflating an existing 
            ///	Rectangle by the specified coordinate values.
            /// </remarks>

            public static Rectangle Inflate(Rectangle rect, int x, int y)
            {
                Rectangle r = new Rectangle(rect.Location, rect.Size);
                r.Inflate(x, y);
                return r;
            }

            /// <summary>
            ///	Inflate Method
            /// </summary>
            ///
            /// <remarks>
            ///	Inflates the Rectangle by a specified width and height.
            /// </remarks>

            public void Inflate(int width, int height)
            {
                Inflate(new Size(width, height));
            }

            /// <summary>
            ///	Inflate Method
            /// </summary>
            ///
            /// <remarks>
            ///	Inflates the Rectangle by a specified Size.
            /// </remarks>

            public void Inflate(Size size)
            {
                x -= size.Width;
                y -= size.Height;
                Width += size.Width * 2;
                Height += size.Height * 2;
            }

            /// <summary>
            ///	Intersect Shared Method
            /// </summary>
            ///
            /// <remarks>
            ///	Produces a new Rectangle by intersecting 2 existing 
            ///	Rectangles. Returns null if there is no	intersection.
            /// </remarks>

            public static Rectangle Intersect(Rectangle a, Rectangle b)
            {
                // MS.NET returns a non-empty rectangle if the two rectangles
                // touch each other
                if (!a.IntersectsWithInclusive(b))
                    return Empty;

                return Rectangle.FromLTRB(
                    Math.Max(a.Left, b.Left),
                    Math.Max(a.Top, b.Top),
                    Math.Min(a.Right, b.Right),
                    Math.Min(a.Bottom, b.Bottom));
            }

            /// <summary>
            ///	Intersect Method
            /// </summary>
            ///
            /// <remarks>
            ///	Replaces the Rectangle with the intersection of itself
            ///	and another Rectangle.
            /// </remarks>

            public void Intersect(Rectangle rect)
            {
                this = Rectangle.Intersect(this, rect);
            }


            /// <summary>
            ///	Union Shared Method
            /// </summary>
            ///
            /// <remarks>
            ///	Produces a new Rectangle from the union of 2 existing 
            ///	Rectangles.
            /// </remarks>

            public static Rectangle Union(Rectangle a, Rectangle b)
            {
                return FromLTRB(Math.Min(a.Left, b.Left),
                         Math.Min(a.Top, b.Top),
                         Math.Max(a.Right, b.Right),
                         Math.Max(a.Bottom, b.Bottom));
            }

            /// <summary>
            ///	Equality Operator
            /// </summary>
            ///
            /// <remarks>
            ///	Compares two Rectangle objects. The return value is
            ///	based on the equivalence of the Location and Size 
            ///	properties of the two Rectangles.
            /// </remarks>

            public static bool operator ==(Rectangle left, Rectangle right)
            {
                return ((left.Location == right.Location) &&
                    (left.Size == right.Size));
            }

            /// <summary>
            ///	Inequality Operator
            /// </summary>
            ///
            /// <remarks>
            ///	Compares two Rectangle objects. The return value is
            ///	based on the equivalence of the Location and Size 
            ///	properties of the two Rectangles.
            /// </remarks>

            public static bool operator !=(Rectangle left, Rectangle right)
            {
                return ((left.Location != right.Location) ||
                    (left.Size != right.Size));
            }


            // -----------------------
            // Public Constructors
            // -----------------------

            /// <summary>
            ///	Rectangle Constructor
            /// </summary>
            ///
            /// <remarks>
            ///	Creates a Rectangle from Point and Size values.
            /// </remarks>

            public Rectangle(Point location, Size size)
            {
                x = location.X;
                y = location.Y;
                width = size.Width;
                height = size.Height;
            }

            /// <summary>
            ///	Rectangle Constructor
            /// </summary>
            ///
            /// <remarks>
            ///	Creates a Rectangle from a specified x,y location and
            ///	width and height values.
            /// </remarks>

            public Rectangle(int x, int y, int width, int height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }



            /// <summary>
            ///	Bottom Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Y coordinate of the bottom edge of the Rectangle.
            ///	Read only.
            /// </remarks>

            [Browsable(false)]
            public int Bottom
            {
                get
                {
                    return y + height;
                }
            }

            /// <summary>
            ///	Height Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Height of the Rectangle.
            /// </remarks>

            public int Height
            {
                get
                {
                    return height;
                }
                set
                {
                    height = value;
                }
            }

            /// <summary>
            ///	IsEmpty Property
            /// </summary>
            ///
            /// <remarks>
            ///	Indicates if the width or height are zero. Read only.
            /// </remarks>		
            [Browsable(false)]
            public bool IsEmpty
            {
                get
                {
                    return ((x == 0) && (y == 0) && (width == 0) && (height == 0));
                }
            }

            /// <summary>
            ///	Left Property
            /// </summary>
            ///
            /// <remarks>
            ///	The X coordinate of the left edge of the Rectangle.
            ///	Read only.
            /// </remarks>

            [Browsable(false)]
            public int Left
            {
                get
                {
                    return X;
                }
            }

            /// <summary>
            ///	Location Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Location of the top-left corner of the Rectangle.
            /// </remarks>

            [Browsable(false)]
            public Point Location
            {
                get
                {
                    return new Point(x, y);
                }
                set
                {
                    x = value.X;
                    y = value.Y;
                }
            }

            /// <summary>
            ///	Right Property
            /// </summary>
            ///
            /// <remarks>
            ///	The X coordinate of the right edge of the Rectangle.
            ///	Read only.
            /// </remarks>

            [Browsable(false)]
            public int Right
            {
                get
                {
                    return X + Width;
                }
            }

            /// <summary>
            ///	Size Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Size of the Rectangle.
            /// </remarks>

            [Browsable(false)]
            public Size Size
            {
                get
                {
                    return new Size(Width, Height);
                }
                set
                {
                    Width = value.Width;
                    Height = value.Height;
                }
            }

            /// <summary>
            ///	Top Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Y coordinate of the top edge of the Rectangle.
            ///	Read only.
            /// </remarks>

            [Browsable(false)]
            public int Top
            {
                get
                {
                    return y;
                }
            }

            /// <summary>
            ///	Width Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Width of the Rectangle.
            /// </remarks>

            public int Width
            {
                get
                {
                    return width;
                }
                set
                {
                    width = value;
                }
            }

            /// <summary>
            ///	X Property
            /// </summary>
            ///
            /// <remarks>
            ///	The X coordinate of the Rectangle.
            /// </remarks>

            public int X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }

            /// <summary>
            ///	Y Property
            /// </summary>
            ///
            /// <remarks>
            ///	The Y coordinate of the Rectangle.
            /// </remarks>

            public int Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }

            /// <summary>
            ///	Contains Method
            /// </summary>
            ///
            /// <remarks>
            ///	Checks if an x,y coordinate lies within this Rectangle.
            /// </remarks>

            public bool Contains(int x, int y)
            {
                return ((x >= Left) && (x < Right) &&
                    (y >= Top) && (y < Bottom));
            }

            /// <summary>
            ///	Contains Method
            /// </summary>
            ///
            /// <remarks>
            ///	Checks if a Point lies within this Rectangle.
            /// </remarks>

            public bool Contains(Point pt)
            {
                return Contains(pt.X, pt.Y);
            }

            /// <summary>
            ///	Contains Method
            /// </summary>
            ///
            /// <remarks>
            ///	Checks if a Rectangle lies entirely within this 
            ///	Rectangle.
            /// </remarks>

            public bool Contains(Rectangle rect)
            {
                return (rect == Intersect(this, rect));
            }

            /// <summary>
            ///	Equals Method
            /// </summary>
            ///
            /// <remarks>
            ///	Checks equivalence of this Rectangle and another object.
            /// </remarks>

            public override bool Equals(object obj)
            {
                if (!(obj is Rectangle))
                    return false;

                return (this == (Rectangle)obj);
            }

            /// <summary>
            ///	GetHashCode Method
            /// </summary>
            ///
            /// <remarks>
            ///	Calculates a hashing value.
            /// </remarks>

            public override int GetHashCode()
            {
                return (height + width) ^ x + y;
            }

            /// <summary>
            ///	IntersectsWith Method
            /// </summary>
            ///
            /// <remarks>
            ///	Checks if a Rectangle intersects with this one.
            /// </remarks>

            public bool IntersectsWith(Rectangle rect)
            {
                return !((Left >= rect.Right) || (Right <= rect.Left) ||
                    (Top >= rect.Bottom) || (Bottom <= rect.Top));
            }

            private bool IntersectsWithInclusive(Rectangle r)
            {
                return !((Left > r.Right) || (Right < r.Left) ||
                    (Top > r.Bottom) || (Bottom < r.Top));
            }

            /// <summary>
            ///	Offset Method
            /// </summary>
            ///
            /// <remarks>
            ///	Moves the Rectangle a specified distance.
            /// </remarks>

            public void Offset(int x, int y)
            {
                this.x += x;
                this.y += y;
            }

            /// <summary>
            ///	Offset Method
            /// </summary>
            ///
            /// <remarks>
            ///	Moves the Rectangle a specified distance.
            /// </remarks>

            public void Offset(Point pos)
            {
                x += pos.X;
                y += pos.Y;
            }

            /// <summary>
            ///	ToString Method
            /// </summary>
            ///
            /// <remarks>
            ///	Formats the Rectangle as a string in (x,y,w,h) notation.
            /// </remarks>

            public override string ToString()
            {
                return String.Format("{{X={0},Y={1},Width={2},Height={3}}}",
                             x, y, width, height);
            }

        }

        public class Size
        {
            public int Width, Height;

            public Size(int w, int h)
            {
                Width = w;
                Height = h;
            }
        }

        public int[,] map;

        /// <summary>
        /// Built rooms stored here
        /// </summary>
        public List<Rectangle> rctBuiltRooms;

        /// <summary>
        /// Built corridors stored here
        /// </summary>
        public List<Point> lBuilltCorridors;

        public List<Point> corridorEdges;

        /// <summary>
        /// Corridor to be built stored here
        /// </summary>
        private List<Point> lPotentialCorridor;

        /// <summary>
        /// Room to be built stored here
        /// </summary>
        private Rectangle rctCurrentRoom;


        #region builder public properties

        //room properties
        [Category("Room"), Description("Minimum Size"), DisplayName("Minimum Size")]
        public Size Room_Min { get; set; }
        [Category("Room"), Description("Max Size"), DisplayName("Maximum Size")]
        public Size Room_Max { get; set; }
        [Category("Room"), Description("Total number"), DisplayName("Rooms to build")]
        public int MaxRooms { get; set; }
        [Category("Room"), Description("Minimum distance between rooms"), DisplayName("Distance from other rooms")]
        public int RoomDistance { get; set; }
        [Category("Room"), Description("Minimum distance of room from existing corridors"), DisplayName("Corridor Distance")]
        public int CorridorDistance { get; set; }

        //corridor properties
        [Category("Corridor"), Description("Minimum corridor length"), DisplayName("Minimum length")]
        public int Corridor_Min { get; set; }
        [Category("Corridor"), Description("Maximum corridor length"), DisplayName("Maximum length")]
        public int Corridor_Max { get; set; }
        [Category("Corridor"), Description("Maximum turns"), DisplayName("Maximum turns")]
        public int Corridor_MaxTurns { get; set; }
        [Category("Corridor"), Description("The distance a corridor has to be away from a closed cell for it to be built"), DisplayName("Corridor Spacing")]
        public int CorridorSpace { get; set; }


        [Category("Probability"), Description("Probability of building a corridor from a room or corridor. Greater than value = room"), DisplayName("Select room")]
        public int BuildProb { get; set; }

        [Category("Map"), DisplayName("Map Size")]
        public Size Map_Size { get; set; }
        [Category("Map"), DisplayName("Break Out"), Description("")]
        public int BreakOut { get; set; }



        #endregion

        /// <summary>
        /// describes the outcome of the corridor building operation
        /// </summary>
        enum CorridorItemHit
        {

            invalid //invalid point generated
            ,
            self  //corridor hit self
                ,
            existingcorridor //hit a built corridor
                ,
            originroom //corridor hit origin room 
                ,
            existingroom //corridor hit existing room
                ,
            completed //corridor built without problem    
                ,
            tooclose
                , OK //point OK
        }

        Point[] directions_straight = new Point[]{ 
                                            new Point(0, -1) //n
                                            , new Point(0, 1)//s
                                            , new Point(1, 0)//w
                                            , new Point(-1, 0)//e
                                        };

        private int filledcell = 1;
        private int emptycell = 0;

        System.Random rnd = new System.Random();

        public csMapbuilder(int x, int y)
        {
            Map_Size = new Size(x, y);
            map = new int[Map_Size.Width, Map_Size.Height];
            Corridor_MaxTurns = 5;
            Room_Min = new Size(3, 3);
            Room_Max = new Size(15, 15);
            Corridor_Min = 3;
            Corridor_Max = 15;
            MaxRooms = 15;
            Map_Size = new Size(150, 150);

            RoomDistance = 5;
            CorridorDistance = 2;
            CorridorSpace = 2;

            BuildProb = 50;
            BreakOut = 250;
        }

        /// <summary>
        /// Initialise everything
        /// </summary>
        private void Clear()
        {
            lPotentialCorridor = new List<Point>();
            rctBuiltRooms = new List<Rectangle>();
            lBuilltCorridors = new List<Point>();
            corridorEdges = new List<Point>();

            map = new int[Map_Size.Width, Map_Size.Height];
            for (int x = 0; x < Map_Size.Width; x++)
                for (int y = 0; y < Map_Size.Width; y++)
                    map[x, y] = filledcell;
        }

        #region build methods()

        /// <summary>
        /// Randomly choose a room and attempt to build a corridor terminated by
        /// a room off it, and repeat until MaxRooms has been reached. The map
        /// is started of by placing a room in approximately the centre of the map
        /// using the method PlaceStartRoom()
        /// </summary>
        /// <returns>Bool indicating if the map was built, i.e. the property BreakOut was not
        /// exceed</returns>
        public bool Build_OneStartRoom()
        {
            int loopctr = 0;

            CorridorItemHit CorBuildOutcome;
            Point Location = new Point();
            Point Direction = new Point();

            Clear();

            PlaceStartRoom();

            //attempt to build the required number of rooms
            while (rctBuiltRooms.Count() < MaxRooms)
            {

                if (loopctr++ > BreakOut)//bail out if this value is exceeded
                    return false;

                if (Corridor_GetStart(out Location, out Direction))
                {

                    CorBuildOutcome = CorridorMake_Straight(ref Location, ref Direction, rnd.Next(1, Corridor_MaxTurns)
                        , rnd.Next(0, 100) > 50 ? true : false);

                    switch (CorBuildOutcome)
                    {
                        case CorridorItemHit.existingroom:
                        case CorridorItemHit.existingcorridor:
                        case CorridorItemHit.self:
                            Corridor_Build();
                            break;

                        case CorridorItemHit.completed:
                            if (Room_AttemptBuildOnCorridor(Direction))
                            {
                                Corridor_Build();
                                Room_Build();
                            }
                            break;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Randomly choose a room and attempt to build a corridor terminated by
        /// a room off it, and repeat until MaxRooms has been reached. The map
        /// is started of by placing two rooms on opposite sides of the map and joins
        /// them with a long corridor, using the method PlaceStartRooms()
        /// </summary>
        /// <returns>Bool indicating if the map was built, i.e. the property BreakOut was not
        /// exceed</returns>
        public bool Build_ConnectedStartRooms()
        {
            int loopctr = 0;

            CorridorItemHit CorBuildOutcome;
            Point Location = new Point();
            Point Direction = new Point();

            Clear();

            PlaceStartRooms();

            //attempt to build the required number of rooms
            while (rctBuiltRooms.Count() < MaxRooms)
            {

                if (loopctr++ > BreakOut)//bail out if this value is exceeded
                    return false;

                if (Corridor_GetStart(out Location, out Direction))
                {

                    CorBuildOutcome = CorridorMake_Straight(ref Location, ref Direction, rnd.Next(1, Corridor_MaxTurns)
                        , rnd.Next(0, 100) > 50 ? true : false);

                    switch (CorBuildOutcome)
                    {
                        case CorridorItemHit.existingroom:
                        case CorridorItemHit.existingcorridor:
                        case CorridorItemHit.self:
                            Corridor_Build();
                            break;

                        case CorridorItemHit.completed:
                            if (Room_AttemptBuildOnCorridor(Direction))
                            {
                                Corridor_Build();
                                Room_Build();
                            }
                            break;
                    }
                }
            }

            return true;

        }

        #endregion


        #region room utilities

        /// <summary>
        /// Place a random sized room in the middle of the map
        /// </summary>
        private void PlaceStartRoom()
        {
            rctCurrentRoom = new Rectangle()
            {
                Width = rnd.Next(Room_Min.Width, Room_Max.Width)
                ,
                Height = rnd.Next(Room_Min.Height, Room_Max.Height)
            };
            rctCurrentRoom.X = Map_Size.Width / 2;
            rctCurrentRoom.Y = Map_Size.Height / 2;
            Room_Build();
        }


        /// <summary>
        /// Place a start room anywhere on the map
        /// </summary>
        private void PlaceStartRooms()
        {

            Point startdirection;
            bool connection = false;
            Point Location = new Point();
            Point Direction = new Point();
            CorridorItemHit CorBuildOutcome;

            while (!connection)
            {

                Clear();
                startdirection = Direction_Get(new Point());

                //place a room on the top and bottom
                if (startdirection.X == 0)
                {

                    //room at the top of the map
                    rctCurrentRoom = new Rectangle()
                    {
                        Width = rnd.Next(Room_Min.Width, Room_Max.Width)
                        ,
                        Height = rnd.Next(Room_Min.Height, Room_Max.Height)
                    };
                    rctCurrentRoom.X = rnd.Next(0, Map_Size.Width - rctCurrentRoom.Width);
                    rctCurrentRoom.Y = 1;
                    Room_Build();

                    //at the bottom of the map
                    rctCurrentRoom = new Rectangle();
                    rctCurrentRoom.Width = rnd.Next(Room_Min.Width, Room_Max.Width);
                    rctCurrentRoom.Height = rnd.Next(Room_Min.Height, Room_Max.Height);
                    rctCurrentRoom.X = rnd.Next(0, Map_Size.Width - rctCurrentRoom.Width);
                    rctCurrentRoom.Y = Map_Size.Height - rctCurrentRoom.Height - 1;
                    Room_Build();


                }
                else//place a room on the east and west side
                {
                    //west side of room
                    rctCurrentRoom = new Rectangle();
                    rctCurrentRoom.Width = rnd.Next(Room_Min.Width, Room_Max.Width);
                    rctCurrentRoom.Height = rnd.Next(Room_Min.Height, Room_Max.Height);
                    rctCurrentRoom.Y = rnd.Next(0, Map_Size.Height - rctCurrentRoom.Height);
                    rctCurrentRoom.X = 1;
                    Room_Build();

                    rctCurrentRoom = new Rectangle();
                    rctCurrentRoom.Width = rnd.Next(Room_Min.Width, Room_Max.Width);
                    rctCurrentRoom.Height = rnd.Next(Room_Min.Height, Room_Max.Height);
                    rctCurrentRoom.Y = rnd.Next(0, Map_Size.Height - rctCurrentRoom.Height);
                    rctCurrentRoom.X = Map_Size.Width - rctCurrentRoom.Width - 2;
                    Room_Build();

                }



                if (Corridor_GetStart(out Location, out Direction))
                {



                    CorBuildOutcome = CorridorMake_Straight(ref Location, ref Direction, 100, true);

                    switch (CorBuildOutcome)
                    {
                        case CorridorItemHit.existingroom:
                            Corridor_Build();
                            connection = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Make a room off the last point of Corridor, using
        /// CorridorDirection as an indicator of how to offset the room.
        /// The potential room is stored in Room.
        /// </summary>
        private bool Room_AttemptBuildOnCorridor(Point pDirection)
        {
            rctCurrentRoom = new Rectangle()
            {
                Width = rnd.Next(Room_Min.Width, Room_Max.Width)
                ,
                Height = rnd.Next(Room_Min.Height, Room_Max.Height)
            };

            //startbuilding room from this point
            Point lc = lPotentialCorridor.Last();

            if (pDirection.X == 0) //north/south direction
            {
                rctCurrentRoom.X = rnd.Next(lc.X - rctCurrentRoom.Width + 1, lc.X);

                if (pDirection.Y == 1)
                    rctCurrentRoom.Y = lc.Y + 1;//south
                else
                    rctCurrentRoom.Y = lc.Y - rctCurrentRoom.Height - 1;//north


            }
            else if (pDirection.Y == 0)//east / west direction
            {
                rctCurrentRoom.Y = rnd.Next(lc.Y - rctCurrentRoom.Height + 1, lc.Y);

                if (pDirection.X == -1)//west
                    rctCurrentRoom.X = lc.X - rctCurrentRoom.Width;
                else
                    rctCurrentRoom.X = lc.X + 1;//east
            }

            return Room_Verify();
        }


        /// <summary>
        /// Randomly get a point on the edge of a randomly selected room
        /// </summary>
        /// <param name="Location">Out: Location of point on room edge</param>
        /// <param name="Location">Out: Direction of point</param>
        /// <returns>If Location is legal</returns>
        private void Room_GetEdge(out Point pLocation, out Point pDirection)
        {

            rctCurrentRoom = rctBuiltRooms[rnd.Next(0, rctBuiltRooms.Count())];

            //pick a random point within a room
            //the +1 / -1 on the values are to stop a corner from being chosen
            pLocation = new Point(rnd.Next(rctCurrentRoom.Left + 1, rctCurrentRoom.Right - 1)
                                  , rnd.Next(rctCurrentRoom.Top + 1, rctCurrentRoom.Bottom - 1));


            //get a random direction
            pDirection = directions_straight[rnd.Next(0, directions_straight.GetLength(0))];

            do
            {
                //move in that direction
                pLocation.Offset(pDirection);

                if (!Point_Valid(pLocation.X, pLocation.Y))
                    return;

                //until we meet an empty cell
            } while (Point_Get(pLocation.X, pLocation.Y) != filledcell);

        }

        #endregion

        #region corridor utitlies

        /// <summary>
        /// Randomly get a point on an existing corridor
        /// </summary>
        /// <param name="Location">Out: location of point</param>
        /// <returns>Bool indicating success</returns>
        private void Corridor_GetEdge(out Point pLocation, out Point pDirection)
        {
            List<Point> validdirections = new List<Point>();

            do
            {
                //the modifiers below prevent the first of last point being chosen
                pLocation = lBuilltCorridors[rnd.Next(1, lBuilltCorridors.Count - 1)];

                //attempt to locate all the empy map points around the location
                //using the directions to offset the randomly chosen point
                foreach (Point p in directions_straight)
                    if (Point_Valid(pLocation.X + p.X, pLocation.Y + p.Y))
                        if (Point_Get(pLocation.X + p.X, pLocation.Y + p.Y) == filledcell)
                            validdirections.Add(p);


            } while (validdirections.Count == 0);

            pDirection = validdirections[rnd.Next(0, validdirections.Count)];
            pLocation.Offset(pDirection);

        }

        /// <summary>
        /// Build the contents of lPotentialCorridor, adding it's points to the builtCorridors
        /// list then empty
        /// </summary>
        private void Corridor_Build()
        {
            corridorEdges.Add(lPotentialCorridor[0]);
            corridorEdges.Add(lPotentialCorridor[lPotentialCorridor.Count - 1]);
            foreach (Point p in lPotentialCorridor)
            {
                Point_Set(p.X, p.Y, emptycell);
                lBuilltCorridors.Add(p);
            }

            lPotentialCorridor.Clear();
        }

        /// <summary>
        /// Get a starting point for a corridor, randomly choosing between a room and a corridor.
        /// </summary>
        /// <param name="Location">Out: pLocation of point</param>
        /// <param name="Location">Out: pDirection of point</param>
        /// <returns>Bool indicating if location found is OK</returns>
        private bool Corridor_GetStart(out Point pLocation, out Point pDirection)
        {
            rctCurrentRoom = new Rectangle();
            lPotentialCorridor = new List<Point>();

            if (lBuilltCorridors.Count > 0)
            {
                if (rnd.Next(0, 100) >= BuildProb)
                    Room_GetEdge(out pLocation, out pDirection);
                else
                    Corridor_GetEdge(out pLocation, out pDirection);
            }
            else//no corridors present, so build off a room
                Room_GetEdge(out pLocation, out pDirection);

            //finally check the point we've found
            return Corridor_PointTest(pLocation, pDirection) == CorridorItemHit.OK;

        }

        /// <summary>
        /// Attempt to make a corridor, storing it in the lPotentialCorridor list
        /// </summary>
        /// <param name="pStart">Start point of corridor</param>
        /// <param name="pTurns">Number of turns to make</param>
        private CorridorItemHit CorridorMake_Straight(ref Point pStart, ref Point pDirection, int pTurns, bool pPreventBackTracking)
        {

            lPotentialCorridor = new List<Point>();
            lPotentialCorridor.Add(pStart);

            int corridorlength;
            Point startdirection = new Point(pDirection.X, pDirection.Y);
            CorridorItemHit outcome;

            while (pTurns > 0)
            {
                pTurns--;

                corridorlength = rnd.Next(Corridor_Min, Corridor_Max);
                //build corridor
                while (corridorlength > 0)
                {
                    corridorlength--;

                    //make a point and offset it
                    pStart.Offset(pDirection);

                    outcome = Corridor_PointTest(pStart, pDirection);
                    if (outcome != CorridorItemHit.OK)
                        return outcome;
                    else
                        lPotentialCorridor.Add(pStart);
                }

                if (pTurns > 1)
                    if (!pPreventBackTracking)
                        pDirection = Direction_Get(pDirection);
                    else
                        pDirection = Direction_Get(pDirection, startdirection);
            }

            return CorridorItemHit.completed;
        }

        /// <summary>
        /// Test the provided point to see if it has empty cells on either side
        /// of it. This is to stop corridors being built adjacent to a room.
        /// </summary>
        /// <param name="pPoint">Point to test</param>
        /// <param name="pDirection">Direction it is moving in</param>
        /// <returns></returns>
        private CorridorItemHit Corridor_PointTest(Point pPoint, Point pDirection)
        {

            if (!Point_Valid(pPoint.X, pPoint.Y))//invalid point hit, exit
                return CorridorItemHit.invalid;
            else if (lBuilltCorridors.Contains(pPoint))//in an existing corridor
                return CorridorItemHit.existingcorridor;
            else if (lPotentialCorridor.Contains(pPoint))//hit self
                return CorridorItemHit.self;
            else if (rctCurrentRoom != null && rctCurrentRoom.Contains(pPoint))//the corridors origin room has been reached, exit
                return CorridorItemHit.originroom;
            else
            {
                //is point in a room
                foreach (Rectangle r in rctBuiltRooms)
                    if (r.Contains(pPoint))
                        return CorridorItemHit.existingroom;
            }


            //using the property corridor space, check that number of cells on
            //either side of the point are empty
            foreach (int r in Enumerable.Range(-CorridorSpace, 2 * CorridorSpace + 1).ToList())
            {
                if (pDirection.X == 0)//north or south
                {
                    if (Point_Valid(pPoint.X + r, pPoint.Y))
                        if (Point_Get(pPoint.X + r, pPoint.Y) != filledcell)
                            return CorridorItemHit.tooclose;
                }
                else if (pDirection.Y == 0)//east west
                {
                    if (Point_Valid(pPoint.X, pPoint.Y + r))
                        if (Point_Get(pPoint.X, pPoint.Y + r) != filledcell)
                            return CorridorItemHit.tooclose;
                }

            }

            return CorridorItemHit.OK;
        }


        #endregion

        #region direction methods

        /// <summary>
        /// Get a random direction, excluding the opposite of the provided direction to
        /// prevent a corridor going back on it's Build
        /// </summary>
        /// <param name="dir">Current direction</param>
        /// <returns></returns>
        private Point Direction_Get(Point pDir)
        {
            Point NewDir;
            do
            {
                NewDir = directions_straight[rnd.Next(0, directions_straight.GetLength(0))];
            } while (Direction_Reverse(NewDir) == pDir);

            return NewDir;
        }

        /// <summary>
        /// Get a random direction, excluding the provided directions and the opposite of 
        /// the provided direction to prevent a corridor going back on it's self.
        /// 
        /// The parameter pDirExclude is the first direction chosen for a corridor, and
        /// to prevent it from being used will prevent a corridor from going back on 
        /// it'self
        /// </summary>
        /// <param name="dir">Current direction</param>
        /// <param name="pDirectionList">Direction to exclude</param>
        /// <param name="pDirExclude">Direction to exclude</param>
        /// <returns></returns>
        private Point Direction_Get(Point pDir, Point pDirExclude)
        {
            Point NewDir;
            do
            {
                NewDir = directions_straight[rnd.Next(0, directions_straight.GetLength(0))];
            } while (
                        Direction_Reverse(NewDir) == pDir
                         | Direction_Reverse(NewDir) == pDirExclude
                    );


            return NewDir;
        }

        private Point Direction_Reverse(Point pDir)
        {
            return new Point(-pDir.X, -pDir.Y);
        }

        #endregion

        #region room test

        /// <summary>
        /// Check if rctCurrentRoom can be built
        /// </summary>
        /// <returns>Bool indicating success</returns>
        private bool Room_Verify()
        {
            //make it one bigger to ensure that testing gives it a border
            rctCurrentRoom.Inflate(RoomDistance, RoomDistance);

            //check it occupies legal, empty coordinates
            for (int x = rctCurrentRoom.Left; x <= rctCurrentRoom.Right; x++)
                for (int y = rctCurrentRoom.Top; y <= rctCurrentRoom.Bottom; y++)
                    if (!Point_Valid(x, y) || Point_Get(x, y) != filledcell)
                        return false;

            //check it doesn't encroach onto existing rooms
            foreach (Rectangle r in rctBuiltRooms)
                if (r.IntersectsWith(rctCurrentRoom))
                    return false;

            rctCurrentRoom.Inflate(-RoomDistance, -RoomDistance);

            //check the room is the specified distance away from corridors
            rctCurrentRoom.Inflate(CorridorDistance, CorridorDistance);

            foreach (Point p in lBuilltCorridors)
                if (rctCurrentRoom.Contains(p))
                    return false;

            rctCurrentRoom.Inflate(-CorridorDistance, -CorridorDistance);

            return true;
        }

        /// <summary>
        /// Add the global Room to the rooms collection and draw it on the map
        /// </summary>
        private void Room_Build()
        {
            rctBuiltRooms.Add(rctCurrentRoom);

            for (int x = rctCurrentRoom.Left; x <= rctCurrentRoom.Right; x++)
                for (int y = rctCurrentRoom.Top; y <= rctCurrentRoom.Bottom; y++)
                    map[x, y] = emptycell;

        }

        #endregion

        #region Map Utilities

        /// <summary>
        /// Check if the point falls within the map array range
        /// </summary>
        /// <param name="x">x to test</param>
        /// <param name="y">y to test</param>
        /// <returns>Is point with map array?</returns>
        private Boolean Point_Valid(int x, int y)
        {
            return x > 0 & x < map.GetLength(0)-1 & y > 0 & y < map.GetLength(1)-1;
        }

        /// <summary>
        /// Set array point to specified value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="val"></param>
        private void Point_Set(int x, int y, int val)
        {
            map[x, y] = val;
        }

        /// <summary>
        /// Get the value of the specified point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int Point_Get(int x, int y)
        {
            return map[x, y];
        }

        #endregion

        public delegate void moveDelegate();
        public event moveDelegate playerMoved;

    }

    public int width;
    public int height;

    public GameObject player;

    public Texture[] floorTextures;
    public GameObject doorPrefab;
    public GameObject[] wallPrefabs;
    public GameObject floorPrefab;
    public GameObject ceilingPrefab;

    public GameObject teleporterPrefab;

    public GameObject[] enemies;

    private MeshFilter filter;

    public int tile_count_y = 4;
    public int tile_count_x = 4;
    public int tileset_x = 0;
    public int tileset_y = 0;

    const float tilesize_x = 0.25f;
    const float tilesize_y = 0.25f;

    private static Vector3[] basePoints = {
		new Vector3(-0.5f, -0.5f, 0.5f),
		new Vector3(-0.5f, 0.5f,  0.5f),
		new Vector3(0.5f,  -0.5f, 0.5f),
		new Vector3(0.5f,   0.5f, 0.5f)
	};

    private static int[] baseTris = { 0, 2, 1, 2, 3, 1 };

    System.Random random;

	// Use this for initialization
	void Start () {
        random = new System.Random();
        createMap();
	}

    public void createMap()
    {
        height = 150;
        width = 150;

        MapMaker.csMapbuilder mapBuilder = new MapMaker.csMapbuilder(width, height);

        //Attempt to make a map.
        while (mapBuilder.Build_OneStartRoom() == false)
        {
            //Map generation failed
            Debug.Log("Map gen failed, raising map size and retrying...");
            width += 10;
            height += 10;
        }

        //Draw map.
        //StringBuilder str = new StringBuilder();
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                //str.Append("" + mapBuilder.map[w, h]);
                if (mapBuilder.map[h, w] == (int)TileTypes.Blocked)
                {
                    if (h > 0 && w > 0 && w < width - 1 && h < height - 1)
                    {
                        //Don't spawn walls if point is sorrounded by walls.
                        if (mapBuilder.map[h - 1, w] != (int)TileTypes.Blocked ||
                            mapBuilder.map[h + 1, w] != (int)TileTypes.Blocked ||
                            mapBuilder.map[h, w - 1] != (int)TileTypes.Blocked ||
                            mapBuilder.map[h, w + 1] != (int)TileTypes.Blocked)
                            createWall(w, h);
                    }
                    else
                        createWall(w, h);
                }
                else
                {
                    createFloor(w, h);
                    createCeiling(w, h);
                }
            }
            //str.Append("\n");
        }
        //Debug.Log(str);

        //Create doors for all corridors.
        foreach (csMapbuilder.Point corridor in mapBuilder.corridorEdges)
        {
            if (mapBuilder.map[corridor.X, corridor.Y + 1] != (int)TileTypes.Blocked
                && mapBuilder.map[corridor.X + 1, corridor.Y] == (int)TileTypes.Blocked)
                Instantiate(doorPrefab, new Vector3(corridor.Y, 0.0f, corridor.X), Quaternion.identity);
            else if (mapBuilder.map[corridor.X + 1, corridor.Y] != (int)TileTypes.Blocked
                && mapBuilder.map[corridor.X, corridor.Y + 1] == (int)TileTypes.Blocked)
            {
                GameObject door = (GameObject)Instantiate(doorPrefab, new Vector3(corridor.Y, 0.0f, corridor.X), Quaternion.identity);
                door.transform.Rotate(0, 90, 0);
            }
        }

        //Move player to starting room
        player.transform.position = new Vector3(mapBuilder.rctBuiltRooms[0].X, 0.5f, mapBuilder.rctBuiltRooms[0].Y);


        //Create enemies
        bool isStartingRoom = true;
        foreach (csMapbuilder.Rectangle room in mapBuilder.rctBuiltRooms)
        {
            //Skip first room
            if (isStartingRoom)
            {
                isStartingRoom = false;
                continue;
            }

            int numEnemies = random.Next(1, 4);
            for (int i = 0; i < numEnemies; i++)
            {
                GameObject enemy = (GameObject)Instantiate(enemies[0], new Vector3(room.Y + random.Next(0, room.Height), 0.5f, room.X + random.Next(0, room.Width)), Quaternion.identity);
            }
        }

        //Create level teleporter
        csMapbuilder.Rectangle finalRoom = mapBuilder.rctBuiltRooms[mapBuilder.rctBuiltRooms.Count - 1];
        Instantiate(teleporterPrefab, new Vector3(finalRoom.Y + random.Next(0, finalRoom.Height), 0.5f, finalRoom.X + random.Next(0, finalRoom.Width)), Quaternion.identity);
    }

    public void destroyMap()
    {
        foreach (GameObject o in FindObjectsOfType<GameObject>())
        {
            if (o.tag != "Player" && o.tag != "MainCamera" && o.tag != "Scene")
                Destroy(o);
        }
        createMap();
    }

    private void createFloor(int x, int y)
    {
        Instantiate(floorPrefab, new Vector3(x, -0.5f, y), Quaternion.identity);
    }

    private void createCeiling(int x, int y)
    {
        GameObject ceil = (GameObject)Instantiate(ceilingPrefab, new Vector3(x, 1.5f, y), Quaternion.identity);
    }

    /// <summary>
    /// Create a wall cube at given tile position.
    /// </summary>
    private void createWall(int x, int y)
    {
        GameObject wall = null;
        if (random.NextDouble() > 0.35)
            wall = (GameObject)Instantiate(wallPrefabs[0], new Vector3(x, 0.5f, y), Quaternion.identity);
        else
            wall = (GameObject)Instantiate(wallPrefabs[1], new Vector3(x, 0.5f, y), Quaternion.identity);

        
        MeshFilter meshFilter = wall.transform.GetComponent<MeshFilter>(); ;
        // Now store a local reference for the UVs
        Vector2[] theUVs = new Vector2[meshFilter.mesh.uv.Length];
        theUVs = meshFilter.mesh.uv;
 
        // set UV co-ordinates
        theUVs[10] = new Vector2(0f, 1.0f);
        theUVs[11] = new Vector2(1.0f, 1.0f);
        theUVs[6] = new Vector2(0f, 0f);
        theUVs[7] = new Vector2(1.0f, 0.0f);
 
        // Assign the mesh its new UVs
        meshFilter.mesh.uv = theUVs;
    }


    // Update is called once per frame
    void Update()
    {
	
	}

}
