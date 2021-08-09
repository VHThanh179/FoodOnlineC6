using System.ComponentModel.DataAnnotations;

namespace ASM.Share.Models.ViewModels
{
    public class ViewToken
    {
        //[Required]
        public string Token { get; set; }

        //[Required]
        public int KhachhangId { get; set; }

        //public string ReturnUrl { get; set; }
    }
}
