namespace CameraBazaar.Services.Implementations
{
    using Contracts;
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using CameraBazaar.Services.Models;
    using System;
    using Microsoft.EntityFrameworkCore;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public void Create
            (CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed,
            int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResolution,
            IEnumerable<LightMeteringType> lightMeterings, string description, string imageURL, string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMeteringType)lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageURL = imageURL,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }

        public EditCameraServiceModel FindToEdit(int id)
        {
            var cam = this.db.Cameras.Include(c => c.User).FirstOrDefault(c => c.Id == id);

            if (cam == null)
            {
                return null;
            }

            var lightToString = cam.LightMetering.ToString().Split(new[] { ", " }, StringSplitOptions.None);

            List<LightMeteringType> lightMeterings = new List<LightMeteringType>();

            foreach (var light in lightToString)
            {
                lightMeterings.Add((LightMeteringType)Enum.Parse(typeof(LightMeteringType), light));
            }

            return new EditCameraServiceModel
            {
                Make = cam.Make,
                Model = cam.Model,
                Price = cam.Price,
                Quantity = cam.Quantity,
                MinShutterSpeed = cam.MinShutterSpeed,
                MaxShutterSpeed = cam.MaxShutterSpeed,
                MinISO = cam.MinISO,
                MaxISO = cam.MaxISO,
                IsFullFrame = cam.IsFullFrame,
                VideoResolution = cam.VideoResolution,
                LightMeterings = lightMeterings,
                Description = cam.Description,
                ImageURL = cam.ImageURL
            };
        }

        public bool Edit(int id, CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed,
            int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResolution,
            IEnumerable<LightMeteringType> lightMeterings, string description, string imageURL)
        {
            var cam = this.db.Cameras.FirstOrDefault(c => c.Id == id);

            if (cam == null)
            {
                return false;
            }

            cam.Make = make;
            cam.Model = model;
            cam.Price = price;
            cam.Quantity = quantity;
            cam.MinShutterSpeed = minShutterSpeed;
            cam.MaxShutterSpeed = maxShutterSpeed;
            cam.MinISO = minISO;
            cam.MaxISO = maxISO;
            cam.IsFullFrame = isFullFrame;
            cam.VideoResolution = videoResolution;
            cam.LightMetering = (LightMeteringType)lightMeterings.Cast<int>().Sum();
            cam.Description = description;
            cam.ImageURL = imageURL;

            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<AllCamerasServiceModel> AllCameras()
        {
            return this.db
                    .Cameras
                    .OrderByDescending(c => c.Id)
                    .Select(c => new AllCamerasServiceModel
                    {
                        Id = c.Id,
                        Make = c.Make.ToString(),
                        Model = c.Model,
                        Price = c.Price.ToString("F2"),
                        Quantity = c.Quantity,
                        ImageURL = c.ImageURL,
                        Username = c.User.UserName
                    })
                    .ToList();
        }

        public CameraDetailsServiceModel Details(int id)
        {
            return this.db
                    .Cameras
                    .Where(c => c.Id == id)
                    .Select(c => new CameraDetailsServiceModel
                    {
                        Make = c.Make.ToString(),
                        Model = c.Model,
                        Price = c.Price.ToString("F2"),
                        Quantity = c.Quantity,
                        MinShutterSpeed = c.MinShutterSpeed,
                        MaxShutterSpeed = c.MaxShutterSpeed,
                        MinISO = (int)c.MinISO,
                        MaxISO = c.MaxShutterSpeed,
                        IsFullFrame = c.IsFullFrame,
                        VideoResolution = c.VideoResolution,
                        LightMetering = c.LightMetering.ToString().Split(new[] { ", " }, StringSplitOptions.None),
                        Description = c.Description,
                        ImageURL = c.ImageURL,
                        Username = c.User.UserName
                    })
                    .FirstOrDefault();
        }

        public bool Exist(int id, string userId)
                => this.db
                    .Cameras
                    .Any(c => c.Id == id && c.UserId == userId);

        public bool Delete(int id)
        {
            var camera = this.db.Cameras.FirstOrDefault(c => c.Id == id);

            if (camera == null)
            {
                return false;
            }

            this.db.Cameras.Remove(camera);
            this.db.SaveChanges();

            return true;
        }
    }
}
