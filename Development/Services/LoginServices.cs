using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using ANCL_Marriage_MVC.Services.Interface;
using Dapper;
using System.Data;

namespace ANCL_Marriage_MVC.Services
{
    public class LoginServices: ILoginService
    {
        private readonly IConfiguration configuration;
        private readonly ILoginRepository loginRepository;

        public LoginServices(IConfiguration configuration, ILoginRepository loginRepository)
        {
            this.configuration = configuration;
            this.loginRepository = loginRepository;
        }

        public async Task<LoginResponse> Login(Login login)
        {

            var parameters = new DynamicParameters();

            parameters.Add("in_UserId", login.UserName);
            parameters.Add("in_password", login.Password);
            parameters.Add("in_macaddr", login.macaddr);
            parameters.Add("in_ipaddr", login.ipaddr);
            parameters.Add("in_hostname", login.hostname);
            parameters.Add("in_source", login.source);
            parameters.Add("in_deptid", login.deptid);




            parameters.Add("Out_UserName", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_userid", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_LastLogin", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_LastLogOut", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_Corporation", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_CorporationAddress", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_ReceiptOfficeName", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_ChalanOfficeName", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_PrabhagName", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_PrabhagID", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_DesigID", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_UserType", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_Collectioncenter", dbType: DbType.Int64, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_mobileno", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_OtpValidate", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("out_ErrorCode", dbType: DbType.Int64, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("Out_ErrorMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("out_OrgId", dbType: DbType.Int64, direction: ParameterDirection.Output, size: 6000);
            parameters.Add("out_forceFullPassChage", dbType: DbType.String, direction: ParameterDirection.Output, size: 6000);


            var response = await loginRepository.Login(parameters);

            return response;


        }
    }
}
