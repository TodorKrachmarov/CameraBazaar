namespace CameraBazaar.Services.Contracts
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;

    public interface ICameraService 
    {
        void Create(CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed,
            int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResolution,
            IEnumerable<LightMeteringType> lightMeterings, string description, string imageURL, string userId);

        EditCameraServiceModel FindToEdit(int id);

        bool Edit(int id, CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed,
            int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResolution,
            IEnumerable<LightMeteringType> lightMeterings, string description, string imageURL);

        IEnumerable<AllCamerasServiceModel> AllCameras();

        CameraDetailsServiceModel Details(int id);

        bool Exist(int id, string userId);

        bool Delete(int id);
    }
}
