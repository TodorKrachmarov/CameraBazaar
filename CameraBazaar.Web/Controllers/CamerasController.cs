namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Services.Models;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cameras;
    using Services.Contracts;

    public class CamerasController : BaseController
    {
        private readonly ICameraService cameraService;
        private readonly UserManager<User> userManager;

        public CamerasController(
            UserManager<User> userManager,
            ICameraService cameraService)
        {
            this.userManager = userManager;
            this.cameraService = cameraService;
        }

        public IActionResult All()
        {
            var cameras = this.cameraService.AllCameras();
            return this.View(cameras);
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            if (cameraModel.LightMeterings == null)
            {
                ModelState.AddModelError(nameof(cameraModel.LightMeterings), "Light Metering must have a value!");
                return this.View(cameraModel);
            }

            this.cameraService.Create(cameraModel.Make, cameraModel.Model, cameraModel.Price, cameraModel.Quantity,
                cameraModel.MinShutterSpeed, cameraModel.MaxShutterSpeed, cameraModel.MinISO, cameraModel.MaxISO,
                cameraModel.IsFullFrame, cameraModel.VideoResolution, cameraModel.LightMeterings, cameraModel.Description,
                cameraModel.ImageURL, this.userManager.GetUserId(User));

            return this.RedirectToAll(); ;
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var exist = this.cameraService.Exist(id, userId);

            if (!exist)
            {
                this.SetError("You can not edit This camera!");
                return this.RedirectToAll();
            }

            var camModel = this.cameraService.FindToEdit(id);

            if (camModel == null)
            {
                this.SetError("The camera you want to edit does not exist!");
                return this.RedirectToAll();
            }
            
            return this.View(camModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, EditCameraServiceModel cameraModel)
        {
            var userId = this.userManager.GetUserId(User);

            var exist = this.cameraService.Exist(id, userId);

            if (!exist)
            {
                this.SetError("You can not edit This camera!");
                return this.RedirectToAll();
            }

            if (!ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            var success = this.cameraService.Edit(id, cameraModel.Make, cameraModel.Model, cameraModel.Price, cameraModel.Quantity,
                cameraModel.MinShutterSpeed, cameraModel.MaxShutterSpeed, cameraModel.MinISO, cameraModel.MaxISO,
                cameraModel.IsFullFrame, cameraModel.VideoResolution, cameraModel.LightMeterings, cameraModel.Description,
                cameraModel.ImageURL);

            if (!success)
            {
                this.SetError("The camera you want to edit does not exist!");
                return this.RedirectToAll();
            }
            
            return RedirectToAction(nameof(UsersController.Profile), new { Username = this.User.Identity.Name });
        }

        public IActionResult Details(int id)
        {
            var camera = this.cameraService.Details(id);

            if (camera == null)
            {
                this.SetError("The camera does not exist!");
                return this.RedirectToAll();
            }

            return this.View(camera);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var exist = this.cameraService.Exist(id, userId);

            if (!exist)
            {
                this.SetError("The camera you want to delete does no exist!");
                return this.RedirectToAll();
            }

            return this.View(id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Destroy(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var exist = this.cameraService.Exist(id, userId);

            if (!exist)
            {
                this.SetError("The camera you want to delete does no exist!");
                return this.RedirectToAll();
            }

            var success = this.cameraService.Delete(id);

            if (!success)
            {
                this.SetError("The camera you want to delete does no exist!");
            }

            return RedirectToAction(nameof(UsersController.Profile), new { Username = this.User.Identity.Name });
        }

        private IActionResult RedirectToAll() => RedirectToAction(nameof(All));
    }
}