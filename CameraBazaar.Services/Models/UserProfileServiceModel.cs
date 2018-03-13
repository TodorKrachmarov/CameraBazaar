using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserProfileServiceModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<AllCamerasServiceModel> Cameras { get; set; }
    }
}
