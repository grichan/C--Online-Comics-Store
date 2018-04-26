using Comics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Comics.Controllers
{
    public class UsersController : Controller
    {
        private MyDbContext _context;
        // enable-migrations (first time )
        // add-­migration SomeChangesName         // update-database


        public UsersController()
        {
            _context = new MyDbContext(); // new db conn
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [Route("Logout")] // LOGOUT
        public ActionResult Logout()
        {

            Session.Clear();
            return Redirect("/Index");
        }

        [HttpPost] // LOGIN  
        public JsonResult Login(Users Model) // receives a view model ( must be same name in post-ajax)
        {

            if (!string.IsNullOrEmpty(Model.username) && !string.IsNullOrEmpty(Model.password))
            {

                var user = _context.Users.FirstOrDefault(User => User.username == Model.username);
                if (user != null)
                {


                    var password_bytes = Encoding.UTF8.GetBytes(Model.password);
                    var salt = user.Salt;
                    var salted = GenerateSaltedHash(password_bytes, salt);

                    



                    if (user.username == Model.username && CompareByteArrays(user.Hash, salted))
                    {
                        Session["Status"] = true;
                        Session["User"] = user.email.ToString();
                        Session["Address"] = user.address.ToString();
                        Session["FirstName"] = user.first_name.ToString();
                        Session["LastName"] = user.last_name.ToString();
                        Session["Number"] = user.phone_number.ToString();

                        string str = "LOGGED IN!";
                        return Json(str);
                    }
                    else
                    {
                        return Json("Username or Password incorrect!");
                    }
                }
                else
                    return Json("Username or Password incorrect!");



            }
            else
            {
                return Json("Try Again");
            }



        }

        [HttpPost] // REGISTER
        public JsonResult Register(Users Model) // receives a view model (string) ( must be same name in post-ajax)
        {
            if (String.IsNullOrWhiteSpace(Model.password) || String.IsNullOrWhiteSpace(Model.first_name)
            || String.IsNullOrWhiteSpace(Model.last_name) || String.IsNullOrWhiteSpace(Model.address) || String.IsNullOrWhiteSpace(Model.email)
            || String.IsNullOrWhiteSpace(Model.phone_number))  // not empty check 
            {
                return Json("Missing Fields");
            }
            else
            {
                Regex rx = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}$");
                bool b = rx.IsMatch(Model.email);

                Regex rx1 = new Regex(@"\d{10}");
                bool n = rx1.IsMatch(Model.phone_number);

                // Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:
                Regex rx2 = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
                bool m = rx2.IsMatch(Model.password);


                Model.username = Model.email;

                if (n && b && m)
                {

                    var user = _context.Users.FirstOrDefault(User => User.username == Model.username);
                    if (user == null)
                    {
                       var password_bytes = Encoding.UTF8.GetBytes(Model.password);
                       var salt = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()); 
                       var salted = GenerateSaltedHash(password_bytes, salt);

                        Model.Hash = salted;
                        Model.Salt = salt;
                        _context.Users.Add(Model); // add User
                        _context.SaveChanges();
                        return Json("Created");

                    }
                    else
                    {
                        return Json("Already Exists");
                    }
                }
                else
                {
                    return Json("Please Fill All the fields Correctly");
                }

            }


        }


        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}