    using System;
    using BCrypt.Net;

    namespace HashPasswordApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                string password = "test65";

                // Hash the password once (store this hash in your database)
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                Console.WriteLine("Hashed Password (store this): " + hashedPassword);

                // When verifying a login attempt:
                string enteredPassword = "test65"; // Replace with user input

                // Verify using the stored hash
                bool verified = BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);

                if (verified)
                {
                    Console.WriteLine("Password is correct!");
                }
                else
                {
                    Console.WriteLine("Password is incorrect.");
                }
            }
        }
    }