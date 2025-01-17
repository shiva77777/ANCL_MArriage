using ANCL_Marriage_MVC.Models;

namespace ANCL_Marriage_MVC.Services.Interface
{
    public interface ITransactionsServices
    {
        Task<FormEntry> FormEntry(FormEntry login);
    }
}
