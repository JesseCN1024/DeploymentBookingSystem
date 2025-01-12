using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Presentation.Constants;
using DbsUsersManagementService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DbsUsersManagementService.Controllers
{
    [ApiController]
    [Route("image")]
    public class ImageUploadController : ControllerBase
    {
        public ImageUploadController()
        {
        }




        [HttpPost]
        public async Task<IActionResult> RequestInspectionImageUploadUrlBatch2([FromBody] RequestBatchInspectionImageUploadUrlModel requestModel)
        {
            throw new UserFriendlyException(ErrorCode.NotFound, "Invalid request", "Inner Exception");
        }


    }

}
