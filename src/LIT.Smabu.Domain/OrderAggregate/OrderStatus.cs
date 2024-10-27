using System.ComponentModel.DataAnnotations;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public enum OrderStatus
    {
        [Display(Name = "Neu")]
        New,
        [Display(Name = "In Bearbeitung")]
        InProgress,
        [Display(Name = "Abgeschlossen")]
        Completed
    }
}