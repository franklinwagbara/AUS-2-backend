﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.Helper.Notification;
using static AUS2.Core.Helper.Notification.NotificationModel;

namespace AUS2
{
    public class BaseController : ControllerBase
    {
        private readonly INotification _notification;

        protected BaseController(INotification notification)
        {
            _notification = notification;
        }

        private bool IsValidOperation() => !_notification.HasNotifications;

        protected new ActionResult Response(BaseResponse response)
        {
            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(response);
            }
            else
            {
                if (response == null)
                    response = new Response();

                response.Success = false;
                response.Errors = _notification.Notifications.Select(error => error);
                switch (_notification.Notifications.LastOrDefault().NotificationType)
                {
                    case ENotificationType.InternalServerError:
                        //TODO montar o log
                        //Log.Logger.BaseResponse(response);
                        return StatusCode((int)HttpStatusCode.InternalServerError, response);
                    case ENotificationType.BusinessRules:
                        return Conflict(response);
                    case ENotificationType.NotFound:
                        return NotFound(response);
                    default:
                        return BadRequest(response);
                }
            }
        }

        protected  new IActionResult Response(object response = null)
        {

            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(new
                {
                    success = true,
                    data = response
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.Notifications.Select(error => error)
            });
        }

        protected new IActionResult Response(WebApiResponse response)
        {

            if (IsValidOperation())
            {
                if (response.StatusCode == 200)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                if (response.StatusCode == 400)
                    return BadRequest(new
                    {
                        success = false,
                        data = response
                    });

                if (response.StatusCode == 404)
                    return NotFound(new
                    {
                        success = false,
                        data = response
                    });

                if (response.StatusCode == 409)
                    return Conflict(new
                    {
                        success = false,
                        data = response
                    });

                return StatusCode(StatusCodes.Status500InternalServerError, (new
                {
                    success = false,
                    data = response
                }));
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.Notifications.Select(error => error)
            });
        }

        protected new IActionResult Response(int? id = null, object response = null)
        {
            if (IsValidOperation())
            {
                if (id == null)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                return CreatedAtAction("Get", new { id },
                    new
                    {
                        success = true,
                        data = response ?? new object()
                    });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.Notifications.Select(error => error)
            });
        }

       
    }
    class Response : BaseResponse
    {
    }
}
