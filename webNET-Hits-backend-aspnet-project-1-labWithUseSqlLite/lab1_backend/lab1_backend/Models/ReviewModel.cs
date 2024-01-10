using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace lab1_backend.Models
{
    // как сделать так чтобы не создавалась таблица с автором?
    public class ReviewModel
    {

        public string Id { get; set; }

        public int Rating { get; set; }
        [MaybeNull]
        public string ReviewText { get; set; }

        public bool IsAnonymous { get; set; } = false;

        public DateTime createDateTime { get; set; } = DateTime.UtcNow;
        [MaybeNull]
        public UserShortModel? Author { get; set; }
    }
}
