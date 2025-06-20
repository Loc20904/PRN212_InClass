namespace DE180158WPF.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public string TypeDescription { get; set; } = string.Empty;
        public string TypenNote { get; set; } = string.Empty;

        // Navigation Property
        public List<RoomInformation>? Rooms { get; set; } = null!;
    }
}