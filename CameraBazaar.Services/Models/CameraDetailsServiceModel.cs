namespace CameraBazaar.Services.Models
{
    using Data.Models;
    using System.Collections.Generic;

    public class CameraDetailsServiceModel
    {
        public string Make { get; set; }
        
        public string Model { get; set; }

        public string Price { get; set; }
        
        public int Quantity { get; set; }
        
        public int MinShutterSpeed { get; set; }
        
        public int MaxShutterSpeed { get; set; }

        public int MinISO { get; set; }
        
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }
        
        public string VideoResolution { get; set; }

        public IEnumerable<string> LightMetering { get; set; }
        
        public string Description { get; set; }
        
        public string ImageURL { get; set; }

        public string Username { get; set; }
    }
}
