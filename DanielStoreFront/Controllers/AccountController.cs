using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using DanielStoreFront.Models;

namespace DanielStoreFront.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private SendGridClient _sendGridClient;

        public AccountController(SignInManager<ApplicationUser> signInManager, SendGrid.SendGridClient sendGridClient)
        {
            this._signInManager = signInManager;
            this._sendGridClient = sendGridClient;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser oldUser = await _signInManager.UserManager.FindByNameAsync(username);
                if (oldUser != null)
                {
                   if (_signInManager.UserManager.CheckPasswordAsync(oldUser, password).Result)
                   {
                        await _signInManager.SignInAsync(oldUser, false);
                        return RedirectToAction("Index", "Home");
                   }
                    else
                    {
                        ModelState.AddModelError("username", "Username or password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("username", "Username or password is incorrect.");
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser { UserName = username };
                var userResult = await _signInManager.UserManager.CreateAsync(newUser);
                if (userResult.Succeeded)
                {
                    var passwordResult = await _signInManager.UserManager.AddPasswordAsync(newUser, password);
                    if (passwordResult.Succeeded)
                    {

                        //TODO: send new users an email.

                        SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
                        message.AddTo(username);
                        message.Subject = "Email Confirmation for RADZONE.com";
                        message.SetFrom("BigSauce@RADZONE.li");
                        message.AddContent("text/plain", "Thank you, " + username + " for signing up for RADZONE.com!");
                        await _sendGridClient.SendEmailAsync(message);


                        await _signInManager.SignInAsync(newUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        await _signInManager.UserManager.DeleteAsync(newUser);
                    }
                }
                else
                {
                    foreach (var error in userResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View();
        }
    }
}