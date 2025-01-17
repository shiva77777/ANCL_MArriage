using ANCL_Marriage_MVC.Data;
using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using Dapper;
using System.Data;

namespace ANCL_Marriage_MVC.Repositories
{
    public class TransactionsRepositery : ITransactionsRepositery
    {

        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext applicationDbContext;

        public TransactionsRepositery(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            this.configuration = configuration;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<FormEntry> FormEntry(DynamicParameters parameters)
        {
            using var connection = applicationDbContext.CreateConnection();
            await connection.ExecuteAsync("aomm_formreceived_ins", parameters, commandType: CommandType.StoredProcedure);
      
            var errorCode = parameters.Get<Int64?>("out_ErrorCode");
            var errorMsg = parameters.Get<string?>("out_ErrorMsg");
            var FORMID = parameters.Get<Int64?>("out_FORMID");
           
           
            return new FormEntry()
            {
                ErrorCode= errorCode,
                ErrorMessage= errorMsg,
                OutFormId= FORMID
            };
        }


    }
}
