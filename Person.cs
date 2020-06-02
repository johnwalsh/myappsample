using System;
using System.Collections.Generic;
using System.Linq;
//some stuff

namespace WebFlix
{
    public enum SubsriptionType { Basic, Standard, Premium };
    public enum PictureDefintion { SD, HD, ULTRA_HD };

    /* --------------------------------------------------------
     *    SUBSCRIPTION CLASS
     * --------------------------------------------------------*/
    public class Subscription
    {
        //  4 is the max number of screen allowed so set it as const
        private const int MAX_SCREENS = 4;

        //1.a.i he subscription type (basic, standard, or premium)  
        public SubsriptionType SubscriptionType { get; set; }
        

        //1.a.ii The max no of screens  
        private int screens;
        public int Screens
        {
            get { return screens; }
            set
            {
                if (value >= 1 && value <= MAX_SCREENS)
                    this.screens = value;
                else
                    throw new ArgumentException("number of screens is invalid");
            }
        }

        //1.a.iii The picture definition (standard, high-definition, or ultra-high definition) for the video that is streamed 
        public PictureDefintion PictureDefinition { get; set; }

        //1.a.iv The price per month for the subscription 
        public double PricePerMonth { get; set; }


        //Override ToString() to return an appropriately formatted string containing full information about the Subscription 
        public override string ToString()
        {
            return "Subscription: " + SubscriptionType + " Defintion: " + PictureDefinition
                + " Screens: " + Screens + " euro: " + PricePerMonth;
        }
    }


    /* --------------------------------------------------------
    *    WebFlixSubscriptions
    * --------------------------------------------------------*/
    public class WebFlixSubscriptions
    {
        //2.a  Store the above data in a collection in a field in the class 
        //     i.e populate 3 subscriptions with data shown in table
        private List<Subscription> subscriptions = new List<Subscription>()
        {
            new Subscription()
            {
                SubscriptionType = SubsriptionType.Basic,
                Screens = 1,
                PictureDefinition = PictureDefintion.SD,               
                PricePerMonth = 7.99
            },
            new Subscription()
            {
                SubscriptionType = SubsriptionType.Standard,
                Screens = 2,
                PictureDefinition = PictureDefintion.HD,               
                PricePerMonth = 10.99
            },
            new Subscription()
            {
                SubscriptionType = SubsriptionType.Premium,
                Screens = 4,
                PictureDefinition = PictureDefintion.ULTRA_HD,               
                PricePerMonth = 15.99
            }
        };

        //2.b Provide an indexer to retrieve the subscription based on the subscription type
        //i.e. index into the collection based on the subscription type to find and return 
        //the full subscription object for that subscription type 
        public Subscription this[SubsriptionType type]
        {
            get
            {
                foreach (var sub in subscriptions)
                {
                    if (sub.SubscriptionType == type)
                        return sub;
                }
                return null;

                //You can do all the above in one line using the SingleOrDefault function in collections
                // return subscriptions.SingleOrDefault(s => s.SubscriptionType == type);
            }
        }
    }

//Prop (1) 8 chars(1), a digit(1), a Ucase(1)

    /* --------------------------------------------------------
    *    ACCOUNT CLASS
    * --------------------------------------------------------*/
    // a WebFlix account
    public class Account
    {
        //3.a read/Write Props
        //3.a.i  Email address of subscriber (no validation required) 
        public String Email { get; set; }

        //3.a.i Password for the account (min 8 chars including at least 1 digit and 1 uppercase letter), 
        //the password should be read-only outside the class 
        private String password;
        public String Password
        {
            private get
            {
                return password;
            }
            set
            {
                if (IsValidPassword(value))
                {
                    password = value;
                }
                else
                {
                    throw new ArgumentException("invalid password");
                }
            }
        }

        //3.a.i The subscription the user has signed up for (i.e. Subscription object) 
        public Subscription Subscription { get; set; }

        //3.b Add a constructor that initialises the 3 properties/fields to values passed in as parameters to the constructor. 
        public Account(String email, String password, Subscription subscription)
        {
            this.Email = email;
            this.Password = password;
            this.Subscription = subscription;
        }

        // check password strength, must be min 8 chars and contain a digit
        // and uppercase letter
        public static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            //*** check for at least 1 digit  and 1 uppercase*****
            
            //Solution 1 : loop over each char is string and look for didgits and numeber
            var DigitFound = false;
            var UpperFound = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    DigitFound = true;
                }
                if (char.IsUpper(c))
                {
                    UpperFound = true;
                }
            }
            
            if (DigitFound == false || UpperFound == false)
                return false;  //didnt find a digit or uppcase so validation fails


            //Solution 2 Could also do it using the string.Any function                 
            if (!password.Any(c => char.IsDigit(c)))
            {
                return false;
            }

            if (!password.Any(c => char.IsUpper(c)))
            {
                return false;
            }

            return true;
        }


        //Override ToString() to return an appropriately formatted string containing full information (except password)  about the Account 
        public override string ToString()
        {
            return "Subscription: " + Subscription + " Email: " + Email;
        }

    }


    //Develop a test class which tests the logic above
    /* --------------------------------------------------------
    *    TEST CLASS
    * --------------------------------------------------------*/
    public class WebFlixTest
    {
        public static void test()
        {
            try
            {
                WebFlixSubscriptions webflix = new WebFlixSubscriptions();
                Console.WriteLine(webflix[SubsriptionType.Standard]);

                Account gc = new Account(email: "gary@gmail.com", password: "passWord11", subscription: webflix[SubsriptionType.Standard]);
                Console.WriteLine(gc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}
