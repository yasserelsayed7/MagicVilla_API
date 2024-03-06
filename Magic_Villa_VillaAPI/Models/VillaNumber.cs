using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_Villa_VillaAPI.Models
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] //not increament ony by one
        public int VillaNo { get; set; }

        //villa id => foreign key to the id in villa table
        [ForeignKey("Villa")] // we will need to create a navigation prop and provide its name to DataAnn
        public int villaId { get; set; }
        public Villa Villa { get; set; }



        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }




    }
}
