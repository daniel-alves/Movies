using System.Collections.Generic;
using System.Linq;

namespace Movies.App.Models.Shared
{
    //paginação sem a necessidade de dar count na tabela
    //a paginação para quando a quantidade de registros na lista é menor que Limit
    //paginação artesanal rsrsrsrs
    public class PageViewModel<TViewModel>
        where TViewModel : ViewModel
    {
        public int Page { get; set; } = 1;

        public int PreviousPage { get => Page == 1 ? Page : Page - 1; }

        public int NextPage { get => Page + 1; }

        public int Limit { get; set; } = 20;

        public int Offset { get => (Page - 1) * Limit; }

        public bool HasNextPage { get => List.Count() == Limit; }

        public bool HasPreviousPage { get => Page > 1; }

        public List<TViewModel> List { get; set; }
    }
}
