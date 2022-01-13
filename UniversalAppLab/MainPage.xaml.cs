using System.Text.RegularExpressions;
using Windows.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalAppLab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public static bool isValidFirstName = false;
        public static bool isValidLastName = false;
        public static bool isValidPassword = false;
        public static bool isValidAddress = false;
        public static bool isValidPhone = false;
        public static bool isValidAvatar = false;
        public static bool isValidEmail = false;
        public static bool isValidGender = false;
        public static bool isValidBirthday = false;
        public static bool isValidIntroduction = false;

        private void Handle_Submit(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            Account account = new Account();
            account.FirstName = txtFirstName.Text;
            account.LastName = txtLastName.Text;
            account.Password = txtPassword.Password;
            account.Address = txtAddress.Text;
            account.Phone = txtPhone.Text;
            account.Avatar = txtAvatar.Text;
            account.Email = txtEmail.Text;
            account.Birthday = txtBirthday.Date.DateTime;
            account.Introduction = txtIntroduction.Text;
            foreach (var child in txtGender.Children)
            {
                if ((child as RadioButton).IsChecked == true)
                {
                    account.Gender = Convert.ToString((child as RadioButton).Tag);
                }
            }
            
            msgFirstName.Text = ValidateFirstName(account.FirstName) ? "" : "First name is required.";
            msgLastName.Text = ValidateLastName(account.LastName) ? "" : "Last name is required.";
            msgPassword.Text = ValidatePassword(account.Password) ? "" : "Password must be contain at least 8 characters, least 1 number and 1 upper case letter.";
            msgAddress.Text = ValidateAddress(account.Address) ? "" : "Address is required.";
            msgPhone.Text = ValidatePhone(account.Phone) ? "" : "Invalid phone number.";
            msgAvatar.Text = ValidateAvatar(account.Avatar) ? "" : "Invalid avatar.";
            msgEmail.Text = ValidateEmail(account.Email) ? "" : "Last name is required.";
            msgGender.Text = ValidateGender(account.Gender) ? "" : "Gender is required.";
            msgBirthday.Text = ValidateBirthday(account.Birthday) ? "" : "Invalid birthday.";
            msgIntroduction.Text = ValidateIntroduction(account.Introduction) ? "" : "Introduction is required.";

            if(isValidFirstName && isValidLastName && isValidPassword && isValidAddress && isValidPhone && isValidAvatar && isValidEmail && isValidGender && isValidBirthday && isValidIntroduction)
            {
                string json = JsonConvert.SerializeObject(account);
                Debug.WriteLine(json);
            }
        }

        private void Handle_Reset(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        public static bool ValidateFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                isValidFirstName = false;
                return false;
            }
            isValidFirstName = true;
            return true;
        }

        public static bool ValidateLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                isValidLastName = false;
                return false;
            }
            isValidLastName = true;
            return true;
        }

        public static bool ValidatePassword(string password)
        {
            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            if (Regex.IsMatch(password, pattern))
            {
                isValidPassword = true;
                return true;
            }
            isValidPassword = false;
            return false;
        }

        public static bool ValidateAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                isValidAddress = false;
                return false;
            }
            isValidAddress = true;
            return true;
        }

        public static bool ValidatePhone(string phone)
        {
            string pattern = @"^(84|0[3|5|7|8|9])+([0-9]{8})$";
            if (Regex.IsMatch(phone, pattern))
            {
                isValidPhone = true;
                return true;
            }
            isValidPhone = false;
            return false;
        }

        public static bool ValidateAvatar(string avatar)
        {
            string pattern = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";
            if (Regex.IsMatch(avatar, pattern))
            {
                isValidAvatar = true;
                return true;
            }
            isValidAvatar = false;
            return false;
        }

        public static bool ValidateEmail(string email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (Regex.IsMatch(email, pattern))
            {
                isValidEmail = true;
                return true;
            }
            isValidEmail = false;
            return false;
        }

        public static bool ValidateGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                isValidGender = false;
                return false;
            }
            isValidGender = true;
            return true;
        }

        public static bool ValidateBirthday(DateTime birthday)
        {
            if(DateTime.Compare(birthday, DateTime.Now) >= 0)
            {
                isValidBirthday = false;
                return false;
            }
            isValidBirthday = true;
            return true;
        }
        public static bool ValidateIntroduction(string introduction)
        {
            if (string.IsNullOrEmpty(introduction))
            {
                isValidIntroduction = false;
                return false;
            }
            isValidIntroduction = true;
            return true;
        }

    }
}