using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using ANCL_Marriage_MVC.Services.Interface;
using Dapper;
using System.Data;

namespace ANCL_Marriage_MVC.Services
{
    public class TransactionsServices: ITransactionsServices
    {
        private readonly IConfiguration configuration;
        private readonly ITransactionsRepositery transactionsRepositery;
        private readonly ILoginRepository loginRepository;

        public TransactionsServices(IConfiguration configuration,ITransactionsRepositery transactionsRepositery)
        {
            this.configuration = configuration;
            this.transactionsRepositery = transactionsRepositery;
           
        }

        public async Task<FormEntry> FormEntry(FormEntry formEntry)
        {

            var parameters = new DynamicParameters();

            parameters.Add("in_UserId", formEntry.UserId);
            parameters.Add("IN_CollcenterId", formEntry.CollCenterId);
            parameters.Add("IN_FirstName", formEntry.FirstName);
            parameters.Add("IN_MiddleName", formEntry.MiddleName);
            parameters.Add("IN_LastName", formEntry.LastName);
            parameters.Add("IN_PhoneNo", formEntry.PhoneNo);
            parameters.Add("IN_Address", formEntry.Adddress);
            parameters.Add("IN_UlbId", formEntry.UlbId);
            parameters.Add("IN_Source", formEntry.Source);


            parameters.Add("out_ErrorCode", dbType: DbType.Int64, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("out_ErrorMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("out_FORMID", dbType: DbType.Int64, direction: ParameterDirection.Output, size: 6000);
          
            var response = await transactionsRepositery.FormEntry(parameters);

            return response;


        }

        
    }
}
