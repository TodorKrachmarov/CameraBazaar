namespace CameraBazaar.Web.Models.Cameras
{
    using Data.Models;
    using Infrastructure.Validations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCameraViewModel
    {
        public CameraMakeType Make { get; set; }

        [Required]
        [MaxLength(100)]
        [Model]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Display(Name = "Min Shutter Speed")]
        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinISOType MinISO { get; set; }

        [Display(Name = "Max ISO")]
        [Range(200, 409600)]
        [MaxISO]
        public int MaxISO { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Video Resolution")]
        [Required]
        [StringLength(15)]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        public IEnumerable<LightMeteringType> LightMeterings { get; set; }

        [Required]
        [StringLength(6000)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [StringLength(2000, MinimumLength = 10)]
        [ImageURL]
        public string ImageURL { get; set; }
    }
}
