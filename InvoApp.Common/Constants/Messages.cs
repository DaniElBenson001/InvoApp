using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Common.Constants
{
    public static class Messages
    {
        public static class ErrorMessage
        {
            public const string BaseError = "Oops, An Error Occurred; It isn't your Fault!";

            public const string UserNotFound = "Oops, User Not Found";
            public const string ClientNotFound = "Oops, Client Not Found";
            public const string InvoiceNotFound = "Oops, Invoice Not Found";

            public const string IncorrectPassword = "Incorrect Password or Email, Try Again";

            public const string UserAlreadyExists = "Oops,  User Already Exists";
            public const string ClientAlreadyExists = "Oops, Client Already Exists";
            public const string ClientAlreadyDeleted = "Oops, Client Already Deleted";

        }

        public static class SuccessMessage
        {
            public const string BaseSuccess = "Successful! ";

            public const string ClientListSuccess = BaseSuccess + "Here is the List.";
            public const string ClientAddedSuccess = BaseSuccess + "New Client Added.";
            public const string ClientUpdatedSuccess = BaseSuccess + "Client Updated.";
            public const string ClientDeletedSuccess = "Successfully Deleted.";
        }

        public static class InfoMessage
        {

        }

        public static class WarningMessage
        {

        }
    }
}
