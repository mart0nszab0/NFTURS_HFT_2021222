using NFTURS_HFT_2021222.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NFTURS_HFT_2021222.Models
{
    public class Game
    {

        public enum GameMode { Singleplayer, Multiplayer } //for the Mode property

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [UserVisible]
        public int GameId { get; set; }

        [StringLength(240)]
        [UserVisible]
        public string Name { get; set; }
        
        [ForeignKey(nameof(Publisher))]
        public int PublisherID { get; set; }
        
        [ForeignKey(nameof(Genre))]
        public int GenreID { get; set; }

        
        public int Price { get; set; }

        
        public double GameRating { get; set; }
        public int NumberOfReviews { get; set; }

        public GameMode Mode { get; set; }

        //navigation property
        [UserVisible]
        public virtual Genre Genre { get; set; }

        [UserVisible]
        public virtual Publisher Publisher { get; set; }
    }


}
