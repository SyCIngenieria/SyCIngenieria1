using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SyCIngenieria.Models.GestionContratos
{
    public class OrdenCambio
    {
        public int Id { get; set; }

        [ForeignKey("Contrato")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ContratoId { get; set; }
        public Contrato? Contrato { get; set; }

        [ForeignKey("AmpliacionContrato")]
        public int? AmpliacionContratoId { get; set; }
        public AmpliacionContrato? AmpliacionContrato { get; set; }

        public ICollection<ODS>? ODS { get; set; }
    }
}
