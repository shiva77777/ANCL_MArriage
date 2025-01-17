using ANCL_Marriage_MVC.Data;
using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using Dapper;
using System.Data;

namespace ANCL_Marriage_MVC.Repositories
{
    public class LoginRepository: ILoginRepository
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext applicationDbContext;

        public LoginRepository(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            this.configuration = configuration;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<LoginResponse> Login(DynamicParameters parameters)
        {

            using var connection = applicationDbContext.CreateConnection();
            await connection.ExecuteAsync("admins.aoma_login_fetch", parameters, commandType: CommandType.StoredProcedure);




            var userName = parameters.Get<string?>("Out_UserName");
            var userId = parameters.Get<string?>("Out_userid");
            var lastLogin = parameters.Get<string?>("Out_LastLogin");
            var lastLogOut = parameters.Get<string?>("Out_LastLogOut");
            var corporation = parameters.Get<string?>("Out_Corporation");
            var corporationAddress = parameters.Get<string?>("Out_CorporationAddress");
            var receiptOfficeName = parameters.Get<string?>("Out_ReceiptOfficeName");
            var chalanOfficeName = parameters.Get<string?>("Out_ChalanOfficeName");
            var prabhagName = parameters.Get<string?>("Out_PrabhagName");
            var prabhagID = parameters.Get<string?>("Out_PrabhagID");
            var desigID = parameters.Get<string?>("Out_DesigID");
            var userType = parameters.Get<string?>("Out_UserType");
            var collectioncenter = parameters.Get<Int64?>("Out_Collectioncenter");
            var mobileno = parameters.Get<string?>("Out_mobileno");
            var otpValidate = parameters.Get<string?>("Out_OtpValidate");
            var errorCode = parameters.Get<Int64?>("out_ErrorCode");
            var errorMsg = parameters.Get<string?>("Out_ErrorMsg");
            var OrgId = parameters.Get<Int64?>("out_OrgId");
            var forceFullPassChage = parameters.Get<string?>("out_forceFullPassChage");

            if (errorCode != 9999)
                return null;

            return new LoginResponse()
            {
                userName = userName,
                userId = userId,
                lastLogin = lastLogin,
                lastLogOut = lastLogOut,
                corporation = corporation,
                corporationAddress = corporationAddress,
                receiptOfficeName = receiptOfficeName,
                chalanOfficeName = chalanOfficeName,
                prabhagName = prabhagName,
                prabhagID = prabhagID,
                desigID = desigID,
                userType = userType,
                collectioncenter = collectioncenter,
                mobileno = mobileno,
                otpValidate = otpValidate,
                errorCode = errorCode,
                errorMsg = errorMsg,
                OrgId = OrgId,
                forceFullPassChage = forceFullPassChage

            };
        }

    }
}
