using VividClub.Data;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace VividClub.Web.Models.Messanger
{
    public class MessangerModel
    {
        public PaginatedList<MessageModel> Messages { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxMessageLength)]
        public string MessageText { get; set; }
    }
}