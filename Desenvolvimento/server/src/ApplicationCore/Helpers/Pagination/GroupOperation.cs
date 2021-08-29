using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Helpers.Pagination
{
    public enum GroupOperation
    {
        [Display(Name = "AND", Description = "AND")]
        AND,
        [Display(Name = "OR", Description = "OR")]
        OR
    }
}
