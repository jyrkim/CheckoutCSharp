using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    /// <summary>
    /// Sample PaymentViewModel just for demo purposes.
    /// </summary>
    public class PaymentViewModel
    {
        [Required]
        public string Amount { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Address { get; set; }

        public string Postcode { get; set; }

        public string PostOffice { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public string Language { get; set; }

        public string Device { get; set; }
    }
}