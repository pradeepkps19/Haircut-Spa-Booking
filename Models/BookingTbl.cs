namespace DatabaseFirstApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BookingTbl
    {
        public int BookingId { get; set; }
        public string ServiceName { get; set; }
        [Required(ErrorMessage = "Please Fill the Customer Name.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please Fill the Booking Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        [Required(ErrorMessage = "Please Fill the Booking Time")]
        public string BookingTime { get; set; }

        public int UserId { get; set; }
    }
}