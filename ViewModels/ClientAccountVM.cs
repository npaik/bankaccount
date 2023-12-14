using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class ClientAccountVM
    {
        public int ClientId { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

        private string _FirstName;
        public string FirstName { get; set; }

        private string _LastName;
        public string LastName { get; set; }

        private string _AccountType;
        public string AccountType { get; set; }

        private int _AccountNum;
        public int AccountNum
        {
            get { return _AccountNum; }
            set { _AccountNum = value; }
        }
    }
}