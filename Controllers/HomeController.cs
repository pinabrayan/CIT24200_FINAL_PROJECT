using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
// BRAYAN PINA 
namespace PasswordGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<string> passwords = LoadPasswords();
            ViewBag.Passwords = passwords;
            return View();
        }

        [HttpPost]
        public IActionResult GeneratePassword(int length, bool includeNumbers, bool includeSymbols)
        {
            string password = GenerateRandomPassword(length, includeNumbers, includeSymbols);
            SavePassword(password);
            List<string> passwords = LoadPasswords();
            ViewBag.Passwords = passwords;
            ViewBag.Password = password;
            return View("Index");
        }

        [HttpPost]
        public IActionResult ClearPasswords()
        {
            ClearPasswordFile();
            List<string> passwords = LoadPasswords();
            ViewBag.Passwords = passwords;
            return View("Index");
        }

        private string GenerateRandomPassword(int length, bool includeNumbers, bool includeSymbols)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            if (includeNumbers)
            {
                characters += "0123456789";
            }
            if (includeSymbols)
            {
                characters += "!@#$%^&*()_+-=[]{}\\|;':\",./<>?";
            }

            Random random = new Random();
            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = characters[random.Next(characters.Length)];
            }

            return new string(password);
        }

        private void SavePassword(string password)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "passwords.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(password);
            }
        }

        private List<string> LoadPasswords()
        {
            List<string> passwords = new List<string>();
            string path = Path.Combine(Environment.CurrentDirectory, "passwords.txt");
            if (System.IO.File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        passwords.Add(line);
                    }
                }
            }
            return passwords;
        }

        private void ClearPasswordFile()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "passwords.txt");
            System.IO.File.WriteAllText(path, string.Empty);
        }

    }
}
// BRAYAN PINA

