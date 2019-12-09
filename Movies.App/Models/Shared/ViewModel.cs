namespace Movies.App.Models.Shared
{
    public abstract class ViewModel
    {
        public long Id { get; set; }

        public bool CanDelete { get; set; }
    }
}
