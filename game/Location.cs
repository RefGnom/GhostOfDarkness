namespace game
{
    internal class Location
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public static Location GetStartLocation()
        {
            return new Location();
        }

        public static Location GetLocation()
        {
            return new Location();
        }
    }
}
