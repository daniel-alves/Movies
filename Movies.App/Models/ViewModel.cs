namespace Movies.App.Models
{
    public abstract class ViewModel
    {
        public long Id { get; set; }

        public bool CanDelete { get; set; }
    }
}
