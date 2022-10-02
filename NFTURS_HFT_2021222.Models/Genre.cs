using NFTURS_HFT_2021222.Models.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NFTURS_HFT_2021222.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [UserVisible]
        public int GenreId { get; set; }

        [StringLength(240)]
        [UserVisible]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }

        public Genre()
        {
            Games = new HashSet<Game>();
        }
    }
}