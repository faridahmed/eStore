using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineApp.Models;
using System.Collections.Generic;
using OnlineApp.ModelData;
using System.Web.Security;

namespace OnlineApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
  
        private LIVEEntities databaseManager = new LIVEEntities();
  
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                // Verification.    
                if (this.Request.IsAuthenticated)
                {
                    // Info.    
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info.    
            return this.View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model, string returnUrl)
        {
            try
            {  
                if (ModelState.IsValid)
                {  
                    var loginInfo = this.databaseManager.spUserLoginToApplication(model.UserID, model.UserPass, 0.ToString()).ToList();
                    
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        // Initialization.    
                        var logindetails = loginInfo.First();
                        // Login In.
                        // var sid = logindetails.UserName;
                        var sid = logindetails.userId;


                        this.SignInUser(sid.ToString(), false);
                        // Info.    
                        // return this.RedirectToLocal(returnUrl);

                        // for menu
                        bool isExist = false;
                        using (LIVEEntities dc = new LIVEEntities())
                        {
                            isExist = dc.sUsers.Where(x => x.UserID == model.UserID).Any();
                            if (isExist)
                            {
                                LoginData loginCredentials = dc.sUsers.Where(x => x.UserID == model.UserID).Select(x => new LoginData
                                {

                                    UserName = x.UserName,
                                    RoleId = x.RoleId,
                                    UserID = x.UserID
                                }).FirstOrDefault();
                                List<MenuData> menus = dc.sUserPages.Where(x => x.RoleID == loginCredentials.RoleId && x.UserID == loginCredentials.UserID).Select(x => new MenuData
                                {
                                    MenuID = x.sMenu.MenuID,
                                    MenuName = x.sMenu.MenuName,
                                    PageCode = x.PageCode,
                                    PageDescription = x.PageDescription,
                                    ContName = x.ContName,
                                    PageName = x.PageName,
                                    RoleId = x.RoleID,
                                    UserID = x.UserID,
                                    RoleName = x.sRole.RoleName
                                }).ToList();
                                FormsAuthentication.SetAuthCookie(loginCredentials.UserID.ToString(), false);
                                Session["LoginCredentials"] = loginCredentials;
                                Session["MenuMaster"] = menus;
                                Session["Name"] = loginCredentials.UserID;
                                return this.RedirectToLocal(returnUrl);
                            }
                            else
                            {
                                ViewBag.ErrorMsg = "Please enter the valid credentials!...";
                                return View("Index");
                            }
                        }


                    }
                    else
                    {
                        // Setting.    
                        ModelState.AddModelError(string.Empty, "Invalid UserName or Password..Please check");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            return this.View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult OLDLogin(LoginData model, string returnUrl)
        {
            try
            {
                // Verification.    
                if (ModelState.IsValid)
                {
                    // Initialization.    
                    var loginInfo = this.databaseManager.spUserLoginToApplication(model.UserID, model.UserPass,0.ToString()).ToList();
                    // Verification.    
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        // Initialization.    
                        var logindetails = loginInfo.First();
                        var sid = logindetails.userId;
                        // Login In.    
                        this.SignInUser(logindetails.userId.ToString(), false);
                        // Info.    
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        // Setting.    
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // If we got this far, something failed, redisplay form    
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                // Setting.    
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign Out.    
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Login", "Account");
        }
         private void SignInUser(string username, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }
 
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Home");
        }

    }
}
 
  