namespace ANCL_Marriage_MVC.Repositories
{
    public class Queries
    {
        internal const string vw_ulbs = "SELECT num_corporation_id, var_corporation_name FROM admins.aoma_corporation_mas ORDER BY num_corporation_id";


        internal const string vw_ulbsbyid = "SELECT num_corporation_id, var_corporation_name FROM admins.aoma_corporation_mas WHERE num_corporation_id = :id";

    }
}
