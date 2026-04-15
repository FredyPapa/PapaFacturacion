using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Dto.Request;

namespace Papa.Facturacion.UI.Components.Shared
{
    public partial class Pager
    {
        private const int MaxPageButtons = 5;
        private int StartPageIndex => Result == null ? 0 : Math.Max(1, Result.CurrentPage - MaxPageButtons / 2);
        private int EndPageIndex => Result == null ? 0 : Math.Min(Result.TotalPages, StartPageIndex + MaxPageButtons - 1);

        [Parameter] public PagerRequest? Result { get; set; }

        [Parameter]
        public EventCallback OnPageChanged { get; set; }

        [Parameter]
        public EventCallback OnPageSizeChanged { get; set; }

        private async Task OnPreviousClicked()
        {
            if (Result?.CurrentPage > 1)
            {
                Result.CurrentPage--;
                await OnPageChanged.InvokeAsync();
            }
        }

        private async Task OnNextClicked()
        {
            if (Result?.CurrentPage < Result?.TotalPages)
            {
                Result.CurrentPage++;
                await OnPageChanged.InvokeAsync();
            }
        }

        private async Task OnPageClicked(int page)
        {
            if (Result is null) return;
            Result.CurrentPage = page;
            await OnPageChanged.InvokeAsync();
        }

        private void OnPageSizeClicked(int size)
        {
            if (Result is null) return;
            Result.RowsPerPage = size;
            OnPageSizeChanged.InvokeAsync();
        }

        private void PaginationSizeChanged(ChangeEventArgs e)
        {
            if (e.Value is null) return;
            var size = int.Parse(e.Value.ToString() ?? "5");
            OnPageSizeClicked(size);
        }

        private async Task OnPreviousGroupClicked()
        {
            if (Result.CurrentPage > MaxPageButtons)
            {
                Result.CurrentPage = StartPageIndex - 1;
            }
            else
            {
                Result.CurrentPage = 1;
            }
            await OnPageChanged.InvokeAsync();
        }

        private async Task OnNextGroupClicked()
        {
            if (EndPageIndex < Result.TotalPages)
            {
                Result.CurrentPage = EndPageIndex + 1;
            }
            else
            {
                Result.CurrentPage = Result.TotalPages;
            }
            await OnPageChanged.InvokeAsync();
        }
    }
}
