using System.Net.Mail;
using System.Net;
using FlixStore.Data;
using FlixStore.Data.Static;
using FlixStore.Data.ViewModels;
using FlixStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google;

namespace FlixStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAdress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again ! ";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again ! ";
            return View(loginVM);
        }


        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAdress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAdress,
                UserName = registerVM.EmailAdress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegisterCompleted");
            }

            // Gestion des erreurs si la création échoue
            foreach (var error in newUserResponse.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        //// GET: /Account/ForgotPassword
        //[HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = await _userManager.FindByEmailAsync(model.EmailAdress);
        //    if (user == null)
        //    {
        //        // Ne pas révéler que l'utilisateur n'existe pas
        //        TempData["Message"] = "If an account with this email exists, you will receive a reset link.";
        //        return View();
        //    }

        //    // Générer token de reset
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var resetLink = Url.Action("ResetPassword", "Account",
        //        new { email = model.EmailAdress, token = token }, Request.Scheme);

        //    // Envoi du mail via OAuth 2.0
        //    var service = await GetGmailServiceAsync();
        //    await SendEmailAsync(service, model.EmailAdress, "Password Reset", $"Please reset your password by clicking here: <a href='{resetLink}'>link</a>");
        //    TempData["Message"] = "If an account with this email exists, you will receive a reset link.";
        //    return View();
        //}

        //// GET: /Account/ResetPassword
        //[HttpGet]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    if (token == null || email == null)
        //    {
        //        ModelState.AddModelError("", "Invalid password reset token");
        //    }
        //    var model = new ResetPasswordVM { Token = token, EmailAdress = email };
        //    return View(model);
        //}

        //// POST: /Account/ResetPassword
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = await _userManager.FindByEmailAsync(model.EmailAdress);
        //    if (user == null)
        //    {
        //        TempData["Error"] = "User not found";
        //        return RedirectToAction(nameof(ForgotPassword));
        //    }

        //    var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //    if (!resetPassResult.Succeeded)
        //    {
        //        foreach (var error in resetPassResult.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        return View(model);
        //    }

        //    TempData["Message"] = "Password has been reset successfully";
        //    return RedirectToAction(nameof(Login));
        //}

        //// --- METHODE POUR ENVOYER EMAIL AVEC GMAIL API et OAuth 2.0 ---
        //private async Task<GmailService> GetGmailServiceAsync()
        //{
        //    string[] Scopes = { GmailService.Scope.GmailSend };
        //    string ApplicationName = "FlixStoreApp";

        //    var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "credentials", "client_secret.json");

        //    using var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read);
        //    var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "credentials", "token.json");

        //    var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //        GoogleClientSecrets.Load(stream).Secrets,
        //        Scopes,
        //        "user",
        //        CancellationToken.None,
        //        new FileDataStore(tokenPath, true)
        //    );

        //    return new GmailService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = ApplicationName,
        //    });
        //}

        //private async Task SendEmailAsync(GmailService service, string toEmail, string subject, string body)
        //{
        //    try
        //    {
        //        var fromEmail = "flixstorepfe@gmail.com"; // Assure-toi que c’est bien l’email autorisé dans Google Cloud Console

        //        var mimeMessage = new MimeKit.MimeMessage();
        //        mimeMessage.From.Add(new MimeKit.MailboxAddress("", fromEmail));
        //        mimeMessage.To.Add(new MimeKit.MailboxAddress("", toEmail));
        //        mimeMessage.Subject = subject;
        //        mimeMessage.Body = new MimeKit.TextPart("html") { Text = body };

        //        using var ms = new MemoryStream();
        //        mimeMessage.WriteTo(ms);
        //        var rawMessage = Convert.ToBase64String(ms.ToArray())
        //            .Replace("+", "-")
        //            .Replace("/", "_")
        //            .Replace("=", ""); // ✅ Supprimer le padding

        //        var message = new Google.Apis.Gmail.v1.Data.Message
        //        {
        //            Raw = rawMessage
        //        };

        //        await service.Users.Messages.Send(message, "me").ExecuteAsync();
        //    }
        //    catch (GoogleApiException e)
        //    {
        //        var errorDetails = e.Error?.Message ?? e.Message;
        //        throw new Exception($"Erreur Gmail API : {errorDetails}", e);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Erreur d'envoi de l'email : {ex.Message}", ex);
        //    }
        //}

    }
}
