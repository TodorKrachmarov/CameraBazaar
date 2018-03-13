namespace CameraBazaar.Services.Models
{
    public class AllCamerasServiceModel
    {
        public int Id { get; set; }

        public string Make { get; set; }
        
        public string Model { get; set; }

        public string Price { get; set; }
        
        public int Quantity { get; set; }

        public string ImageURL { get; set; }

        public string Username { get; set; }
    }
}
